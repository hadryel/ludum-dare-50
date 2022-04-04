using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    public GameObject QuestRequirementsPanel;
    public GameObject QuestRequirement;

    public Color CompletedColor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Quest quest)
    {
        GetComponent<Image>().color = quest.QuestPanelBackgroundColor;
        GetComponentInChildren<TextMeshProUGUI>().text = quest.Name;

        int index = 0;

        foreach(var requirement in quest.ResourcesToComplete)
        {
            var qrGO = GameObject.Instantiate(QuestRequirement, QuestRequirementsPanel.transform);
            qrGO.GetComponent<Image>().sprite = quest.ResourcesToComplete[index].GetComponentInChildren<SpriteRenderer>().sprite;
            index++;
        }

        Destroy(QuestRequirement);
    }
}
