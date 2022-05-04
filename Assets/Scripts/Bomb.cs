using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float speed = 3;
    private Vector3 direction = Vector3.zero;
    [SerializeField] private float lifeTime = 0;
    private float lifeStart;
    [SerializeField] private int BombSpeed = 1; 
    private Rigidbody2D rigidBodyComponent;

    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }
    
    void Start()
    {
        lifeStart = Time.time;
        rigidBodyComponent = GetComponent<Rigidbody2D>();
        //Задаем направление bomb
        rigidBodyComponent.velocity = 
        (Direction - transform.position).normalized * BombSpeed;
    }

    private void Update()
    {
        if (Time.time - lifeStart >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D other) 
    {
        //уничтожение бомбы при столкновении с любым предметом
        if(gameObject.tag != other.gameObject.tag
        && other.gameObject.tag != "Untagged")
        {
            Destroy(gameObject);
        }
    }
}