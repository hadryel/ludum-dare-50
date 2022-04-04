using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayPanel : MonoBehaviour
{
    public Caravan Caravan;
    public TextMeshProUGUI Text;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDays()
    {
        Text.text = "Day " + Caravan.Days;
    }
}
