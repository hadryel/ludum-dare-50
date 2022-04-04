using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Should be named SpawnManager
public class ResourceHandler : MonoBehaviour
{
    public Caravan Caravan;
    public Resource Wood;
    public Resource Food;
    public Resource People;
    public Resource Stone;

    public List<Recipe> AvailableRecipes = new List<Recipe>();
    public List<Building> ActiveBuildings = new List<Building>();

    public Transform UpWall;
    public Transform DownWall;
    public Transform LeftWall;
    public Transform RightWall;

    public DayPanel DayPanel;

    void Start()
    {
        ExecuteCycle();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Caravan.Reset();
    }

    float TotalSpawnedWoods = 1f;
    float TotalSpawnedStones = 5f;

    void DefaultResources()
    {
        if (!Food.gameObject.activeInHierarchy)
            Food.Spawn();

        if (UnityEngine.Random.Range(0f, 1f) >= (TotalSpawnedWoods / (TotalSpawnedWoods + TotalSpawnedStones)))
        {
            Wood.Spawn();
            TotalSpawnedWoods++;
        }
        else
        {
            Stone.Spawn();
            TotalSpawnedStones++;
        }
    }

    bool DefaultReadyToCycle()
    {
        return !Wood.gameObject.activeInHierarchy && !Stone.gameObject.activeInHierarchy;
    }

    void ExecuteCycle()
    {
        Caravan.Days += 1;
        DayPanel.UpdateDays();

        DefaultResources();

        foreach (var building in ActiveBuildings)
        {
            building.Cycle();
        }
    }

    public void Collect(Resource resource)
    {
        AddResourceToCaravan(resource);
        ValidateResources();
    }

    void AddResourceToCaravan(Resource resource)
    {
        resource.gameObject.SetActive(false);

        if (resource.Type == ResourceType.Food)
        {
            Caravan.FoodAmount = Mathf.Clamp(Caravan.FoodAmount + resource.FoodValue, 0, Caravan.FoodCapacity);
        }
        else
        {
            var resourceGO = Instantiate(resource.gameObject, Caravan.CaravanElements[Caravan.CaravanElements.Count - 1].transform.position, Quaternion.identity);
            resourceGO.tag = "Player";

            if (resourceGO.GetComponent<Resource>().CaravanGraphic != null)
                resourceGO.GetComponentInChildren<SpriteRenderer>().sprite = resourceGO.GetComponent<Resource>().CaravanGraphic;

            resourceGO.SetActive(true);
            Caravan.CaravanElements.Add(resourceGO);

            Caravan.AddResource(resourceGO.GetComponent<Resource>(), 1);
        }
    }

    void ValidateResources()
    {
        if (DefaultReadyToCycle() && !ActiveBuildings.Any(b => !b.ReadyToCycle()))
        {
            ExecuteCycle();
            ValidateAndRandomizeRecipes();
        }
    }

    void ValidateAndRandomizeRecipes()
    {
        ValidateRecipes();

        foreach (var recipe in AvailableRecipes.Where(r => r.gameObject.activeInHierarchy))
        {
            recipe.RandomizePosition();
        }
    }

    public void ValidateRecipes()
    {
        foreach (var recipe in AvailableRecipes)
        {
            if (recipe.IsReadyToBuild())
                recipe.gameObject.SetActive(true);
            else
                recipe.gameObject.SetActive(false);
        }
    }

    public Vector2 RandomFreeCoordinateInsideBoard(bool singleSquare)
    {
        float x = Mathf.Round(UnityEngine.Random.Range(LeftWall.position.x + 1, RightWall.position.x - 1));
        float y = Mathf.Round(UnityEngine.Random.Range(DownWall.position.y + 1, UpWall.position.y - 1));

        var result = new Vector2(x, y);

        result = GetNextFreeCoordinateFrom(result, singleSquare);

        return result;
    }

    public Vector2 GetNextFreeCoordinateFrom(Vector2 position, bool singleSquare)
    {
        float minX = LeftWall.position.x + 1;
        float maxX = RightWall.position.x - 1;
        float minY = DownWall.position.y + 1;
        float maxY = UpWall.position.y - 1;

        Vector2 checkSize = (singleSquare) ? new Vector2(0.75f, 0.75f) : new Vector2(1.5f, 1.5f);

        RaycastHit2D hit = Physics2D.BoxCast(position, checkSize, 0, Vector2.zero);

        Vector2 newPosition = position;

        while (hit.collider != null)
        {
            newPosition += Vector2.right;

            if (newPosition.x >= maxX - 1)
            {
                newPosition = new Vector2(minX + 1, newPosition.y);
                newPosition += Vector2.up;

                if (newPosition.y >= maxY - 1)
                {
                    newPosition = new Vector2(newPosition.x, minY + 1);
                }
            }
            Debug.Log("BOXCAST HIT!! " + newPosition);
            hit = Physics2D.BoxCast(newPosition, checkSize, 0, Vector2.zero);
        }

        Debug.Log("FINAL POSITION SELECTED: " + newPosition);
        return newPosition;
    }
}