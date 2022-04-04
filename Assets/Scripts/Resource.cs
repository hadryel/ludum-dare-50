using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ResourceHandler ResourceHandler;
    public ResourceType Type;
    public float FoodCostPerStep;
    public float FoodValue;

    public Sprite CaravanGraphic;

    void Start()
    {

    }

    void Update()
    {

    }

    public void RandomizePosition()
    {
        Vector2 position = ResourceHandler.RandomFreeCoordinateInsideBoard(false);
        transform.position = new Vector3(position.x, position.y, 0);
    }


    public void Spawn()
    {
        RandomizePosition();
        gameObject.SetActive(true);
    }
}

public enum ResourceType
{
    Food, Wood, Stone, People, Water, Fish
}