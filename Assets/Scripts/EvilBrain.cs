using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvilBrain : HealthControl
{
    [SerializeField] private MainBilding player;
    [SerializeField] private EvilSpawner spawner;
    private UnitComponent playerUnits;
    public static UnitComponent units;
    public static int unitCount = 0;
    public static void AddUnit(UnitComponent comp)
	{
        //Добавляет юнита бота в список
		units.nextComponent = comp;
		comp.previousComponent = units;
		units = comp;
	}

    void Awake()
    {
        units = new UnitComponent();
    }
    
    void Start()
    {
        this.SetHealth();
    }

    void Update()
    {
        //Задаем направление каждому юниту
        playerUnits = UnitControl.units;
        if(UnitControl.unitCount < 6)
        {
            
        }
    }
}
