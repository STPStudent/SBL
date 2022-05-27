using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCollider : MonoBehaviour
{
    private float spawnTime;

    void Start()
    {
        spawnTime = Time.time;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name.Contains("Build")
            || !other.name.Contains("Unit")
            && !other.name.Contains("Bomb")
            && other.gameObject.GetComponent<InCollider>() != null
            && other.gameObject.GetComponent<InCollider>().spawnTime < spawnTime)
            transform.position += Vector3.left;
    }
}