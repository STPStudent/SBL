using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvilBrain : MonoBehaviour
{
    [SerializeField] private MainBilding player;
    public static List<EvilSpawner> Spawners;
    private UnitComponent playerUnits;
    public static UnitComponent units;
    public static int unitCount = 0;
    public int resourcesCount = 0;
    private int attackCount = 0;
    private int defenseCount = 0;
    public static void AddUnit(UnitComponent comp)
	{
        //Добавляет юнита бота в список
		units.nextComponent = comp;
		comp.previousComponent = units;
		units = comp;
        unitCount++;
        Debug.Log(unitCount);
	}

    void Awake()
    {
        units = new UnitComponent();
        Spawners = new List<EvilSpawner>();
    }
    
    private void SetPoint(string goal, GameObject[] k)
    {
        foreach(var player in k)
            if(player.gameObject.name.Contains(goal)
            && player.transform.position.magnitude < 50)
                foreach(var unit in units)
                {
                    unit.finishPosition = player.gameObject.transform.position;
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
            unit.finishPosition =  new Vector2(42, 6);

        foreach(var comp in playerUnits)
        {
            foreach(var unit in units)
                if(unit != null && comp != null &&
                (unit.transform.position - comp.transform.position).magnitude < 7)
                    unit.finishPosition = comp.transform.position;
        }

        if(unitCount > 10)
        {
            var k = GameObject.FindGameObjectsWithTag("Player");
            SetPoint("PlayerMainBuild", k);
            SetPoint("Fabric", k);
            SetPoint("Tower", k);
            //SetPoint("Recourse", k);
        }

        attackCount = 0;
        foreach(var comp in playerUnits)
        {
            if(comp == null)
                continue;
            var len = (comp.transform.position - transform.position).magnitude;
            if(len < 20)
            {
                attackCount++;
                var k = Random.Range(0, Spawners.Count);
                while(Spawners[k].transform.position.magnitude > 50)
                    k = Random.Range(0, Spawners.Count);
                defenseCount = 0;
                foreach(var unit in units)
                    if(unit != null 
                    && (unit.transform.position - transform.position).magnitude < 20
                    && len < (unit.finishPosition - new Vector2(transform.position.x, transform.position.y)).magnitude)
                    {
                        unit.finishPosition = comp.transform.position;
                        defenseCount++;
                    }
                Debug.Log(attackCount);
                if(defenseCount < attackCount)
                    Spawners[k].Spawn();
            }
        }
    }

    private void CreateBilding()
    {
        resourcesCount = Spawners[0].resources.resourcesCount;
        if(resourcesCount > 15
        && Spawners.Count < 4)
        {
            var x = Random.Range(0.0f, 15.0f);
            var y = Random.Range(0.0f, Mathf.Sqrt(225 - x*x));
            if(x*x + y*y < 81
            || (x/y > Mathf.Tan(1)))
                return;
            Instantiate(Spawners[Spawners.Count % 2], 
                new Vector3(-x, -y, 0.0f) + transform.position, 
                Quaternion.identity);
            Spawners[0].resources.resourcesCount -= 15;
        }
    }

    async void Update()
    {
        //Задаем направление каждому юниту
        playerUnits = UnitControl.units;
        if(Spawners.Count > 2)
            ControlArmy();
        CreateBilding();
    }
}
