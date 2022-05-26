using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSpawner : HealthControl
{
    [SerializeField] public int unitCount = 16;
    [SerializeField] public int unitCost = 5;
    [SerializeField] public UnitComponent unit;
    [SerializeField] public MainBuilding mainBuilding;
    [SerializeField] private float deltaTime;
    private float lastTime;
    private int spawnCount;

    void Start()
    {
        SetHealth();
        EvilBrain.Spawners.Add(this);
        lastTime = Time.time;
        spawnCount = 5;
    }

    void Update()
    {
        if (transform.position.y < -70)
            return;
        DoSpawn();
    }

    public override void DestroyObject()
    {
        EvilBrain.DeleteSpawner(this);
        Destroy(gameObject);
    }

    public void Spawn()
        => spawnCount++;

    public void DoSpawn()
    {
        //Если выполняются условие делает спавн юнита бота
        var time = Time.time;
        if (unitCost <= mainBuilding.resourcesCount
            && spawnCount > 0
            && time - lastTime > deltaTime)
        {
            lastTime = time;
            spawnCount--;
            mainBuilding.resourcesCount -= unitCost;
            Instantiate(unit, transform.position + Vector3.left, Quaternion.identity);
        }
    }
}