using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private UnitComponent unit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*var scale = transform.localScale;
        var helthLen = unit.CurrentHealth / unit.MaxHealth;
        transform.localScale = new Vector3(1 - helthLen, 1, 0);
        transform.localPosition = new Vector3(helthLen / 2, 0, 0);*/
    }
}
