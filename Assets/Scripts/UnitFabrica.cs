using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UnitFabrica : HealthControl
{
    [SerializeField] private int unitCost = 5;
    [SerializeField] private FabricUnitType type;
    [SerializeField] private UnitComponent unit;
    [SerializeField] private MainBilding mainBilding;
    [SerializeField] private float deltaTime;
    private int spawnCount;
    private bool a;
    private Collision2D alsmd;
    private float lastTime;
    void Start()
    {
        this.SetHealth();
        lastTime = Time.time;
    }

    void Spawn()
    {
        var time = Time.time;
        if(spawnCount == 0
        || time - lastTime < deltaTime)
            return;
        spawnCount --;
        Instantiate(unit, transform.position + Vector3.right, Quaternion.identity);
        lastTime = time;
    }
    
    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0)
        && unitCost <= mainBilding.resourcesCount)
        {
            mainBilding.resourcesCount -= unitCost;
            spawnCount ++;
        }
        Spawn();
    }
}
