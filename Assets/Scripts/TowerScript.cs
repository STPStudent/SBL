using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerScript : HealthControl
{
    [SerializeField] private Bomb _bomb;
    private int frameCount = 0;
    public int interval;

    public static UnitComponent units;
    
    void Start()
    {
        SetHealth();
    }
	void Update()
    {
        frameCount++;
        if (frameCount % interval == 0)
        {
            if (gameObject.tag == "Player")
                units = EvilBrain.units;
            else units = UnitControl.units;
            frameCount = 0;
            if (units != null)
            {
			    Vector3 position = transform.position;
                position.y += 4.5F; //чтоб из вершины башни
                Bomb newBomb = Instantiate(_bomb, position, Quaternion.identity) as Bomb;
                foreach (var unit in units)
                {
                    newBomb.Direction = unit.transform.position;
                    break;
                }
            }
		}
    }
}