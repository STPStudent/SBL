using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerScript : HealthControl
{
    [SerializeField] private Bomb bomb;
    private int radius;
    [SerializeField] private float deltaTime;
    private float lastShot;
    private static UnitComponent units;

    void Start()
    {
        SetHealth();
        lastShot = Time.time;
        radius = 20;
    }

    void Update()
    {
        if (Time.time - lastShot > deltaTime)
        {
            lastShot = Time.time;
            units = gameObject.CompareTag("Player") ? EvilBrain.units : UnitControl.units;
            if (units != null)
            {
                var position = transform.position;
                position.y += 4.5F; //чтоб из вершины башни
                foreach (var unit in units)
                {
                    if ((position - unit.transform.position).magnitude > radius)
                        continue;
                    var newBomb = Instantiate(bomb, position, Quaternion.identity) as Bomb;
                    newBomb.Direction = unit.transform.position;
                    break;
                }
            }
        }
    }
}