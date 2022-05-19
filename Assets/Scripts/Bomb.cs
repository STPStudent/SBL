using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private float lifeStart;
    private int bombSpeed;
    private Rigidbody2D rigidBodyComponent;

    public Vector3 Direction { get; set; } = Vector3.zero;

    void Start()
    {
        lifeStart = Time.time;
        bombSpeed = 50;
        rigidBodyComponent = GetComponent<Rigidbody2D>();
        //Задаем направление bomb
        rigidBodyComponent.velocity =
            (Direction - transform.position).normalized * bombSpeed;
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
        if (!gameObject.CompareTag(other.gameObject.tag)
            && !other.gameObject.CompareTag("Untagged"))
        {
            Destroy(gameObject);
        }
    }
}