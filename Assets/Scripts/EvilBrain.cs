using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBrain : MonoBehaviour
{
    [SerializeField] private MainBilding player;
    [SerializeField] private EvilSpawner spawner;
    private UnitControl playerUnits;
    public static UnitComponent units;
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

    void Update()
    {
        //Задаем направление каждому юниту
        //playerUnits = UnitControl.units;
        foreach(var comp in units)
        {
            comp.finishPosition = player.transform.position;
            /*foreach(var unit in playerUnits){
                if((comp.transform.position - unit.transform.position).magnitude < 10)
                    comp.finishPosition = unit.transform.position;
            }*/
        }
    }
}
