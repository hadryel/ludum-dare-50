using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDelivery : MonoBehaviour
{
    public ResourceHandler ResourceHandler;
    public Quest Quest;

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

    public void Despawn()
    {
        gameObject.SetActive(false);
    }
}
