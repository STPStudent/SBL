using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : HealthControl
{
    [SerializeField] public int unitCost = 5;
    [SerializeField] public UnitComponent unit;
    [SerializeField] public MainBuilding mainBuilding;
    [SerializeField] private float deltaTime;
    public UnitPanel unitPanel;
    private float lastTime;
    private int spawnCount;

    void Start()
    {
        SetHealth();
        if(gameObject.tag == "Bot")
        {
            EvilBrain.Spawners.Add(this);
            spawnCount = 5;
        }
        else
            UnitPanel.Spawners.Add(this);
        lastTime = Time.time;
    }

    void Update()
    {
        if (transform.position.y < -70)
            return;
        
        if(spawnCount > 0 && Spawn())
            spawnCount --;
    }

    public override void DestroyObject()
    {
        EvilBrain.DeleteSpawner(this);
        Destroy(gameObject);
    }
    
    public bool Spawn()
    {
        //Если выполняются условие делает спавн юнита бота
        var time = Time.time;
        if(unitCost <= mainBuilding.resourcesCount
        && time - lastTime > deltaTime)
        {
            lastTime = time;
            spawnCount--;
            mainBuilding.resourcesCount -= unitCost;
            Instantiate(unit, transform.position + Vector3.left, Quaternion.identity);
            return true;
        }
        return false;
    }
}