using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerScript : HealthControl
{
    [SerializeField] private Bomb _bomb;
    [SerializeField] private int Radius;
    [SerializeField] private float deltaTime = 0;
    private float lastShot;

    public static UnitComponent units;
    
    void Start()
    {
        SetHealth();
        lastShot = Time.time;
    }
	void Update()
    {
        if (Time.time - lastShot > deltaTime)
        {
            lastShot = Time.time;
            if (gameObject.tag == "Player")
                units = EvilBrain.units;
            else units = UnitControl.units;
            if (units != null)
            {
			    Vector3 position = transform.position;
                position.y += 4.5F; //чтоб из вершины башни
                foreach (var unit in units)
                {
                    if((position - unit.transform.position).magnitude > Radius)
                        continue;
                    Bomb newBomb = Instantiate(_bomb, position, Quaternion.identity) as Bomb;
                    newBomb.Direction = unit.transform.position;
                    break;
                }
            }
		}
    }
}