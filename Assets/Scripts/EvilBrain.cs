using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBrain : MonoBehaviour
{
    [SerializeField] private MainBilding player;
    [SerializeField] private EvilSpawner spawner;
    public static UnitComponent units;
    // Start is called before the first frame update
    public static void AddUnit(UnitComponent comp)
	{
		units.nextComponent = comp;
		comp.previousComponent = units;
		units = comp;
	}

    void Awake()
    {
        units = new UnitComponent();
    }

    // Update is called once per frame
    void Update()
    {
        if(spawner.unitCost <= spawner.resources.resourcesCount
        && spawner.unitCount > 0)
        {
            spawner.resources.resourcesCount -= spawner.unitCost;
            Instantiate(spawner.unit, spawner.transform.position + Vector3.left, Quaternion.identity);
        }
        foreach(var comp in units)
            comp.finishPosition = player.transform.position;
        
    }
}
