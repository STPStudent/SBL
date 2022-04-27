using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Update()
    {
        if (gameObject.GetComponent<HealthControl>().CurrentHealth <= 0)
            GameObject.Destroy(gameObject);
    }
}
