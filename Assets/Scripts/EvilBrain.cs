using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvilBrain : HealthControl
{
    [SerializeField] private MainBilding player;
    public static List<EvilSpawner> Spawners;
    private UnitComponent playerUnits;
    public static UnitComponent units;
    public static int unitCount = 0;
    public int resourcesCount = 0;
    public static void AddUnit(UnitComponent comp)
	{
        //Добавляет юнита бота в список
		units.nextComponent = comp;
		comp.previousComponent = units;
		units = comp;
        unitCount++;
	}

    void Awake()
    {
        units = new UnitComponent();
        Spawners = new List<EvilSpawner>();
    }
    
    void Start()
    {
        this.SetHealth();
    }

    private void SetPoint(string goal, GameObject player)
    {
        if(player.gameObject.name.Contains(goal)
        && player.transform.position.magnitude < 50)
            foreach(var unit in units)
            {
                unit.finishPosition = player.gameObject.transform.position;
                Debug.Log(player.gameObject);
            }
    }

    private void ControlArmy()
    {
        if(unitCount < 5
        || unitCount * 3 < UnitControl.unitCount * 2)
        {
            var k = Random.Range(0, Spawners.Count);
            while(Spawners[k].transform.position.magnitude > 50)
                k = Random.Range(0, Spawners.Count);
            Spawners[k].Spawn();
        }

        foreach(var unit in units)
            unit.finishPosition =  new Vector2(15, -5);

        foreach(var unit in units)
            foreach(var comp in playerUnits)
                if(unit != null && comp != null &&
                (unit.transform.position - comp.transform.position).magnitude < 7)
                    unit.finishPosition = comp.transform.position;
        
        if(unitCount > 10)
        {
            var k = GameObject.FindGameObjectsWithTag("Player");
            foreach(var v in k)
            {
                SetPoint("Fabric", v);
                SetPoint("Recourse", v);
            }
        }
    }

    private void CreateBilding()
    {
        resourcesCount = Spawners[0].resources.resourcesCount;
        if(resourcesCount > 15)
        {
            var x = Random.Range(0.0f, 15.0f);
            var y = Random.Range(0.0f, Mathf.Sqrt(225 - x*x));
            Instantiate(Spawners[Random.Range(0,1)], 
                new Vector3(-x, -y, 0.0f) + transform.position, 
                Quaternion.identity);
            Spawners[0].resources.resourcesCount -= 15;
        }
    }

    async void Update()
    {
        //Задаем направление каждому юниту
        playerUnits = UnitControl.units;
        ControlArmy();
        CreateBilding();
    }
}
