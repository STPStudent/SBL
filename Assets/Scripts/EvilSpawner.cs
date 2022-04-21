using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSpawner : MonoBehaviour
{
    [SerializeField] public int unitCount = 16;
    [SerializeField] public int unitCost = 5;
    [SerializeField] public FabricUnitType type;
    [SerializeField] public UnitComponent unit;
    [SerializeField] public ResourcesFabric resources;
    
    void Update()
    {
        //Если выполняются условие делает спавн юнита бота
        if(unitCost <= resources.resourcesCount
        && unitCount > 0)
        {
            resources.resourcesCount -= unitCost;
            Instantiate(unit, transform.position + Vector3.left, Quaternion.identity);
        }
    }
}