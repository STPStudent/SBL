using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UnitFabrica : MonoBehaviour
{
    [SerializeField] private int unitCount = 16;
    [SerializeField] private int unitCost = 5;
    [SerializeField] private FabricUnitType type;
    [SerializeField] private string name;
    private bool a;
    private Collision2D alsmd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0)
        && unitCost <= ResourcesFabric.resourcesCount
        && unitCount > 0)
        {
            unitCount--;
            ResourcesFabric.resourcesCount -= unitCost;
            var x = GameObject.Find(name);
            Instantiate(x, transform.position + Vector3.right, Quaternion.identity);
        }
    }
}
