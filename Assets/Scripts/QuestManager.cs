using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Caravan Caravan;
    public ResourceHandler ResourceHandler;

    public List<Quest> AvailableQuests;
    public List<Quest> ActiveQuests; // Actually completed

    public QuestDelivery QuestDelivery;

    public Recipe FarmRecipe;
    public Recipe FishingPondRecipe;

    public QuestListPanel QuestListPanel;

    void Start()
    {
        QuestListPanel.gameObject.SetActive(true);

        foreach (var quest in AvailableQuests)
        {
            SetupQuest(quest);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Cycle()
    {
        UpdateAvailableQuests();
    }

    void UpdateAvailableQuests()
    {
        var questsToRemoveFromAvailable = new List<Quest>();
        foreach (var quest in AvailableQuests)
        {
            if (quest.AvailableCondition())
            {
                ActiveQuests.Add(quest);
                QuestListPanel.ActivateQuest(quest);
                questsToRemoveFromAvailable.Add(quest);
            }
        }

        for (int i = 0; i < questsToRemoveFromAvailable.Count; i++)
        {
            AvailableQuests.Remove(questsToRemoveFromAvailable[i]);
        }
    }

    public void UpdateActiveQuests()
    {
        var questsToRemoveFromActive = new List<Quest>();

        foreach (var quest in ActiveQuests)
        {
            if (quest.ValidateCompletion() && !quest.QuestDelivery.gameObject.activeInHierarchy)
            {
                quest.QuestDelivery.Spawn();
                QuestListPanel.CompleteRequirements(quest, true);
            }
            else
            {
                quest.QuestDelivery.Despawn();
                QuestListPanel.CompleteRequirements(quest, false);
            }
        }
    }

    public void SetupQuest(Quest quest)
    {
        switch (quest.Type)
        {
            case QuestType.GrowFromTheLand:
                quest.AvailableCondition = () => { return ResourceHandler.ActiveBuildings.Count(b => b.Type == BuildingType.House) >= 3; };
                quest.Complete = () => { ResourceHandler.AvailableRecipes.Add(FarmRecipe); };
                break;
            case QuestType.GoFishing:
                quest.AvailableCondition = () => { return ResourceHandler.ActiveBuildings.Count(b => b.Type == BuildingType.Farm) >= 1; };
                quest.Complete = () => { ResourceHandler.AvailableRecipes.Add(FishingPondRecipe); };
                break;
        }

        quest.QuestDelivery = Instantiate(QuestDelivery.gameObject, transform.position, Quaternion.identity).GetComponent<QuestDelivery>();
        quest.QuestDelivery.Quest = quest;
    }

    public void CompleteQuest(Quest quest)
    {
        quest.Complete();
        quest.QuestDelivery.gameObject.SetActive(false);
        QuestListPanel.CompleteQuest(quest);
        ActiveQuests.Remove(quest);
        UpdateActiveQuests();
    }
}
