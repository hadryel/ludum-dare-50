using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestListPanel : MonoBehaviour
{
    public QuestManager QuestManager;

    public GameObject QuestPanel;

    public Dictionary<Quest, GameObject> QuestPanels = new Dictionary<Quest, GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateQuest(Quest quest)
    {
        var newQuestPanel = GameObject.Instantiate(QuestPanel, transform);
        newQuestPanel.GetComponent<QuestPanel>().Setup(quest);
        newQuestPanel.SetActive(true);
        QuestPanels.Add(quest, newQuestPanel);
    }

    public void CompleteRequirements(Quest quest, bool completed)
    {
        if (completed)
            QuestPanels[quest].GetComponent<Image>().color = QuestPanels[quest].GetComponent<QuestPanel>().CompletedColor;
        else
            QuestPanels[quest].GetComponent<Image>().color = quest.QuestPanelBackgroundColor;

    }

    public void CompleteQuest(Quest quest)
    {
        var panel = QuestPanels[quest];

        QuestPanels.Remove(quest);
        Destroy(panel);
    }
}
