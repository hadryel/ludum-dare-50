using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildRecipe : Recipe
{
    public override void Build(Caravan caravan)
    {
        var buildingGO = Instantiate(Result.gameObject, caravan.CaravanElements[caravan.CaravanElements.Count - 1].transform.position, Quaternion.identity);
        buildingGO.SetActive(true);

        ResourceHandler.ActiveBuildings.Add(buildingGO.GetComponent<Building>());

        UpdateCaravanElements(caravan);
        ResourceHandler.ValidateRecipes();

        ResourceHandler.AvailableRecipes.Remove(this);
        gameObject.SetActive(false);
    }
}
