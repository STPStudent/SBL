using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSpawner : HealthControl
{
    [SerializeField] public int unitCount = 16;
    [SerializeField] public int unitCost = 5;
    [SerializeField] public FabricUnitType type;
    [SerializeField] public UnitComponent unit;
    [SerializeField] public ResourcesFabric resources;

    void Start()
    {
        SetHealth();
    }
    
    void Update()
    {
        //Если выполняются условие делает спавн юнита бота
        if(unitCost <= resources.resourcesCount
        && unitCount > 0)
        {
            resources.resourcesCount -= unitCost;
            var name = unit.gameObject.name;
            var newUnit = Instantiate(unit, transform.position + Vector3.left, Quaternion.identity);
            newUnit.gameObject.name = name + Guid.NewGuid().ToString();
        }
    }
}
