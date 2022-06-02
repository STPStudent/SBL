using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private int bombSpeed;
    private Rigidbody2D rigidBodyComponent;
    public UnitComponent unit;

    void Start()
        => rigidBodyComponent = GetComponent<Rigidbody2D>();

    //Задаем направление bomb
    private void Update()
    {
        if(unit == null)
            Destroy(gameObject);
            
        rigidBodyComponent.velocity =
            (unit.transform.position - transform.position).normalized * bombSpeed;
    }
}