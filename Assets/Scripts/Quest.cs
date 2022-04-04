using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public QuestManager QuestManager;

    public string Name;
    public QuestType Type;

    public Func<bool> AvailableCondition;
    public Action Complete;

    public List<Resource> ResourcesToComplete;

    public QuestDelivery QuestDelivery;

    public Color QuestPanelBackgroundColor;

    public bool ValidateCompletion()
    {
        var caravanElementsCount = QuestManager.Caravan.CaravanElements.Count;
        var resourcesToCompleteCount = ResourcesToComplete.Count;

        for (int i = 1; i < caravanElementsCount; i++)
        {
            if (i + resourcesToCompleteCount  > caravanElementsCount)
                break;

            if (QuestManager.Caravan.CaravanElements[i].GetComponent<Resource>().Type == ResourcesToComplete[0].Type)
            {
                for(int j = 0; j < resourcesToCompleteCount; j++)
                {
                    if (QuestManager.Caravan.CaravanElements[i + j].GetComponent<Resource>().Type != ResourcesToComplete[j].Type)
                        break;

                    if (resourcesToCompleteCount - 1 == j)
                        return true;
                }
            }
        }

        return false;
    }
}

public enum QuestType
{
    GrowFromTheLand, GoFishing
}