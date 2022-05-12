using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvilBrain : MonoBehaviour
{
    [SerializeField] private MainBilding player;
    [SerializeField] private MainBilding bot;
    public static List<EvilSpawner> Spawners;
    private UnitComponent playerUnits;
    public static UnitComponent units;
    public static int unitCount = 0;
    public int resourcesCount = 0;
    private int attackCount = 0;
    private int defenseCount = 0;
    private int bildingCount = 0;
    public static void AddUnit(UnitComponent comp)
	{
        //Добавляет юнита бота в список
		units.nextComponent = comp;
		comp.previousComponent = units;
		units = comp;
        unitCount++;
	}

    public static void DeleteSpawner(EvilSpawner spawner) 
        => Spawners.Remove(spawner);

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

        foreach(var comp in playerUnits)
        {
            foreach(var unit in units)
                if(unit != null && comp != null &&
                (unit.transform.position - comp.transform.position).magnitude < 7)
                    unit.finishPosition = comp.transform.position;
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
                    && len < (unit.finishPosition 
                        - new Vector2(transform.position.x, transform.position.y)).magnitude)
                    {
                        unit.finishPosition = comp.transform.position;
                        defenseCount++;
                    }
                Debug.Log(defenseCount);
                Debug.Log(attackCount);
                if(defenseCount < attackCount)
                    Spawners[k].Spawn();
            }
        }
    }

    private void CreateBilding<T>(
        int coust, 
        GameObject building
    ) where T : Component
    {
        var anotherBuilding = GetComponents<T>();
        if(resourcesCount > coust
        && Spawners.Count < 4
        || resourcesCount > coust * (anotherBuilding.Length + 1) * 6)
        {
            var x = Random.Range(-10.0f, 10.0f);
            var right = Mathf.Sqrt(10.0f *10.0f - x*x);
            var y = Random.Range(-right, right);
            var allBildings = GameObject
            .FindGameObjectsWithTag(this.gameObject.tag);
            var newBuildPlace = new Vector3(-x,-y,0) 
            + allBildings[Random.Range(0, allBildings.Length)]
                .transform.position;
            if((new Vector3(-x,-y,0)).magnitude < 7
            || !MainCamera.IsBounds(newBuildPlace))
                return;
            foreach(var obj in allBildings)
                if((obj.transform.position - newBuildPlace).magnitude < 7)
                    return;
            Instantiate(building, 
                newBuildPlace, 
                Quaternion.identity);
            bot.resourcesCount -= coust;
            bildingCount ++;
        }
    }

    async void Update()
    {
        //Задаем направление каждому юниту
        resourcesCount = bot.resourcesCount;
        playerUnits = UnitControl.units;
        if(Spawners.Count > 2)
            ControlArmy();
        var spawner = Spawners[Random.Range(0,2)];
        CreateBilding<EvilSpawner>(15, 
            spawner.gameObject);
    }
}
