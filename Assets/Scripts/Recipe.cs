using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public ResourceHandler ResourceHandler;

    public Building Result;

    public List<ResourceType> Ingredients;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Build(Caravan caravan)
    {
        var buildingGO = Instantiate(Result.gameObject, caravan.CaravanElements[caravan.CaravanElements.Count - 1].transform.position, Quaternion.identity);
        buildingGO.SetActive(true);

        ResourceHandler.ActiveBuildings.Add(buildingGO.GetComponent<Building>());
        
        if(Result.Type == BuildingType.Farm)
        {
            caravan.FoodCapacity += 500;
            caravan.FoodAmount += 1000;
        }

        UpdateCaravanElements(caravan);
        ResourceHandler.ValidateRecipes();
        RandomizePosition();

    }

    protected void UpdateCaravanElements(Caravan caravan)
    {
        var recipeElements = new List<ResourceType>(Ingredients);

        for (int i = caravan.CaravanElements.Count - 1; i > 0; i--)
        {
            var resource = caravan.CaravanElements[i].GetComponent<Resource>();

            if (recipeElements.Contains(resource.Type))
            {
                recipeElements.Remove(resource.Type);
                var removedElement = caravan.CaravanElements[i];
                caravan.AddResource(resource, -1);
                caravan.CaravanElements.Remove(caravan.CaravanElements[i]);
                Destroy(removedElement);
            }
        }
    }

    public void RandomizePosition()
    {
        Vector2 position = ResourceHandler.RandomFreeCoordinateInsideBoard(true);
        transform.position = new Vector3(position.x, position.y, 0);
    }

    public bool IsReadyToBuild()
    {
        foreach (ResourceType resourceType in Ingredients.Distinct())
        {
            if (ResourceHandler.Caravan.Resources[resourceType] < Ingredients.Count(r => r == resourceType))
            {
                return false;
            }
        }

        return true;
    }
}
