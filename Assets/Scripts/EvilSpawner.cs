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
    public bool AreTriger = false;
    void Start()
    {
        SetHealth();
        EvilBrain.Spawners.Add(this);
        spawnTime = Time.time;
        lastTime = Time.time;
    }

    public override void DestroyObject()
    {
        EvilBrain.DeleteSpawner(this);
        Destroy(this.gameObject);
    }
    
    public async void Spawn()
    {
        //Если выполняются условие делает спавн юнита бота
        var time = Time.time;
        if(unitCost <= mainBuilding.resourcesCount
        && unitCount > 0
        && time - lastTime > deltaTime)
        {
            lastTime = time;
            mainBuilding.resourcesCount -= unitCost;
            Instantiate(unit, transform.position + Vector3.left, Quaternion.identity);
        }
    }
}
