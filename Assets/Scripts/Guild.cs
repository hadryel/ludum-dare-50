using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guild : Building
{
    public QuestManager QuestManager;

    private void Start()
    {
        QuestManager.gameObject.SetActive(true);
    }

    public override bool ReadyToCycle()
    {
        return true;
    }

    public override void Cycle()
    {
        QuestManager.Cycle();
    }
}
