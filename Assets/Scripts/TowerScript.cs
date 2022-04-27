using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;

public class TowerScript : HealthControl
{
    [SerializeField] private Bomb _bomb;

    void Start()
    {
        SetHealth();
    }
    
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag != gameObject.tag)
        {
            Vector3 position = transform.position;
            position.y += 3.0F; //чтоб из вершины башни
            Bomb newBomb = Instantiate(_bomb, position, Quaternion.identity) as Bomb;
            newBomb.Direction = other.gameObject.transform.position;
        }
    }

    
    
    
}