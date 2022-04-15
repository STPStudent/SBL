using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBrain : MonoBehaviour
{
    [SerializeField] private MainBilding player;
    public static UnitComponent units;
    // Start is called before the first frame update
    public static void AddUnit(UnitComponent comp)
	{
		units.nextComponent = comp;
		comp.previousComponent = units;
		units = comp;
	}

    void Awake()
    {
        units = new UnitComponent();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var comp in units)
            comp.finishPosition = player.transform.position;
    }
}
