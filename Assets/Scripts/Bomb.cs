using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Bomb : HealthControl
{
    private float speed = 3;
    private Vector3 direction;
    private Vector3 dir;
    private SpriteRenderer sprite;

    private void Start()
    {
        SetHealth();
    }

    public Vector3 Direction
    {
        set => direction = value;
    }
    

    private void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Bot").Length > 6)
        {
            direction = GameObject.FindGameObjectsWithTag("Bot")[6].transform.position;
            transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        }
        //Задаем направление bomb
    }
}