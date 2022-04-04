using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBar : MonoBehaviour
{
    public Caravan Caravan;
    [SerializeField]
    Slider FoodBarSlider;

    public Image FoodBarFill;

    public Color Safe;
    public Color Warning;
    public Color Danger;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float foodPercentage = Caravan.FoodAmount / Caravan.FoodCapacity;
        FoodBarSlider.value = foodPercentage;

        if (foodPercentage >= 0.75f)
        {
            FoodBarFill.color = Safe;
        }
        else if (foodPercentage >= 0.25f)
            FoodBarFill.color = Warning;
        else
            FoodBarFill.color = Danger;


    }
}
