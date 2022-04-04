using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingType Type;
    public Resource Resource;

    private void Start()
    {
        Resource = Instantiate(Resource.gameObject).GetComponent<Resource>();
    }

    public virtual bool ReadyToCycle()
    {
        if (Type == BuildingType.Farm)
            return true;

        return !Resource.gameObject.activeInHierarchy;
    }

    public virtual void Cycle()
    {
        Resource.Spawn();
    }
}

public enum BuildingType
{
    House, StoneMine, Lumberyard, Farm, Guild, WaterWell, FishingPond, Outpost
}
