using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCollider : MonoBehaviour
{
    private float spawnTime;
    private new SpriteRenderer renderer;
    private bool isTrigered;

    void Start()
    {
        isTrigered = true;
        spawnTime = Time.time;
        renderer = GetComponent<SpriteRenderer>();
        renderer.sortingOrder = -2;
    }

    void Update()
    {
        if(!isTrigered)
            renderer.sortingOrder = 1;
        isTrigered = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.name.Contains("Build")
        ||!other.name.Contains("Unit")
        &&!other.name.Contains("Bomb")
        && other.gameObject.GetComponent<InCollider>()!=null
        && other.gameObject.GetComponent<InCollider>().spawnTime < spawnTime)
        {
            transform.position = transform.position + Vector3.left;
            isTrigered = true;
        }
    }
}
