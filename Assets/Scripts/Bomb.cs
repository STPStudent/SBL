using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float speed = 3;
    private Vector3 direction = Vector3.zero;
    private int frameCount = 0;
    public int bombLifeLongevity;

    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    
    private void Update()
    {
        frameCount++;
        if (frameCount == bombLifeLongevity)
        {
            frameCount = 0;
            Destroy(gameObject);
        }

        //Задаем направление bomb
        transform.position = Vector3.MoveTowards(transform.position, Direction, speed * Time.deltaTime);
    }
    
    void OnCollisionEnter2D(Collision2D other) 
    {
        //уничтожение бомбы при столкновении с любым предметом
        Destroy(gameObject);
    }
}