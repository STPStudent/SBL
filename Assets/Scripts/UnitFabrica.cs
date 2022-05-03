using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UnitFabrica : HealthControl
{
    [SerializeField] private int unitCount = 16;
    [SerializeField] private int unitCost = 5;
    [SerializeField] private FabricUnitType type;
    [SerializeField] private UnitComponent unit;
    [SerializeField] private ResourcesFabric resources;
    [SerializeField] private float deltaTime;
    private bool a;
    private Collision2D alsmd;
    private float lastTime;
    void Start()
    {
        this.SetHealth();
        lastTime = Time.time;
    }
    
    void OnMouseOver()
    {
        var time = Time.time;
        if(Input.GetMouseButtonDown(0)
        && unitCost <= resources.resourcesCount
        && unitCount > 0
        && time - lastTime > deltaTime)
        {
            lastTime = time;
            unitCount--;
            resources.resourcesCount -= unitCost;
            Instantiate(unit, transform.position + Vector3.right, Quaternion.identity);
        }
    }
}
