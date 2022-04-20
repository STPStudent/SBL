using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBrain : MonoBehaviour
{
    [SerializeField] private MainBilding player;
    [SerializeField] private EvilSpawner spawner;
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
        foreach(var comp in units)
            comp.finishPosition = player.transform.position;
        
    }
}
