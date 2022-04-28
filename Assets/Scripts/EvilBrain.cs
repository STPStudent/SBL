using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvilBrain : HealthControl
{
    [SerializeField] private MainBilding player;
    public static List<EvilSpawner> Spawners;
    private UnitComponent playerUnits;
    public static UnitComponent units;
    public static int unitCount = 0;
    public static void AddUnit(UnitComponent comp)
	{
        //Добавляет юнита бота в список
		units.nextComponent = comp;
		comp.previousComponent = units;
		units = comp;
        unitCount++;
        Debug.Log(unitCount);
	}

    void Awake()
    {
        units = new UnitComponent();
        Spawners = new List<EvilSpawner>();
    }
    
    void Start()
    {
        this.SetHealth();
    }

    private void ControlArmy()
    {
        if(unitCount < 3 * Spawners.Count
        || unitCount * 3 < UnitControl.unitCount * 2)
        {
            for(var i = 0; i < Mathf.Max(3 * Spawners.Count, UnitControl.unitCount * 2 / 3) - unitCount; i++)
                Spawners[i % Spawners.Count].Spawn();
        }

        foreach(var unit in units)
            unit.finishPosition =  new Vector2(15, -5);

        foreach(var unit in units)
            foreach(var comp in playerUnits)
                if(unit != null && comp != null &&
                (unit.transform.position - comp.transform.position).magnitude < 7)
                    unit.finishPosition = comp.transform.position;
        
        if(unitCount > 10)
        {
            var k = GameObject.FindGameObjectsWithTag("Player");
            foreach(var v in k)
            {
                if(v.gameObject.name.Contains("Recourse"))
                {
                    foreach(var unit in units)
                        unit.finishPosition = v.gameObject.transform.position;
                }
            }
        }
    } 

    async void Update()
    {
        //Задаем направление каждому юниту
        playerUnits = UnitControl.units;
        ControlArmy();
    }
}
