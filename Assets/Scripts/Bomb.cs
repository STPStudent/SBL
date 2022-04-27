using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : HealthControl
{
    private float speed = 3;
    private Vector3 direction;
    
    
    private int dir = 1;
    
    
    
    public Vector3 Direction
    {
        set { direction = value; }
    }
    private SpriteRenderer sprite;

    private void Update()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, -direction * 2, speed * Time.deltaTime);
        dir = -dir;
        if(this.CurrentHealth < 0.1)
            DestroyObject();
        //Задаем направление bomb

    }
    
}