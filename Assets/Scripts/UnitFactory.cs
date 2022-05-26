using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UnitFactory : HealthControl
{
    [SerializeField] private int unitCost = 5;
    [SerializeField] private UnitComponent unit;
    [SerializeField] private MainBuilding mainBuilding;
    [SerializeField] private float deltaTime;
    private int spawnCount;
    private float lastTime;

    void Start()
    {
        SetHealth();
        lastTime = Time.time;
    }

    void Spawn()
    {
        var time = Time.time;
        if (spawnCount == 0
            || time - lastTime < deltaTime)
            return;
        spawnCount--;
        Instantiate(unit, transform.position + Vector3.right, Quaternion.identity);
        lastTime = time;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)
            && unitCost <= mainBuilding.resourcesCount)
        {
            mainBuilding.resourcesCount -= unitCost;
            spawnCount++;
        }

        Spawn();
    }
}