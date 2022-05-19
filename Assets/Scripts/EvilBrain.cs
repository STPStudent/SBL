using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvilBrain : MonoBehaviour
{
    [SerializeField] private MainBuilding player;
    [SerializeField] private MainBuilding bot;
    [SerializeField] private TowerScript Tower;
    public static List<EvilSpawner> Spawners;
    private UnitComponent playerUnits;
    public static UnitComponent units;
    public static int unitCount = 0;
    public int resourcesCount = 0;
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
        units = gameObject.AddComponent<UnitComponent>();
        Spawners = new List<EvilSpawner>();
    }
    
    private Vector3 SetPoint(string goal, GameObject[] k)
    {
        var found = Vector3.zero;
        foreach(var player in k)
            if(player.gameObject.name.Contains(goal)
            && player.transform.position.magnitude < 50)
                found = player.gameObject.transform.position;
        return found;
    }

    private void DoAttackUnit()
    {
        foreach(var comp in playerUnits)
        {
            foreach (var unit in units)
                if (unit != null && comp != null &&
                    (unit.transform.position - comp.transform.position).magnitude < 7)
                    unit.finishPosition = comp.transform.position;
        }
    }

    private void TransformPositions(Vector3 goal)
    {
        foreach(var unit in units)
            unit.finishPosition = goal;
    }

    private void DoAttackBuilding()
    {
        if(unitCount > 10)
        {
            var playerBuilding = GameObject.FindGameObjectsWithTag("Player");
            var pointMain = SetPoint("PlayerMainBuild", playerBuilding);
            var pointFabric = SetPoint("Fabric", playerBuilding);
            var pointTower = SetPoint("Tower", playerBuilding);
            if(pointTower != Vector3.zero)
                TransformPositions(pointTower);
            else if(pointFabric != Vector3.zero)
                TransformPositions(pointFabric);
            else if(pointMain != Vector3.zero)
                TransformPositions(pointMain);
        }
    }

    private int SpawnDefasePosition(Vector3 unitAttack)
    {
        var indexNear = 0;
        var lenToUnit = Vector3.zero;
        for(var i = 0; i < Spawners.Count(); i++)
        {
            if(lenToUnit == Vector3.zero
            || lenToUnit.magnitude > 
                (Spawners[i].transform.position - unitAttack).magnitude)
            {
                indexNear = i;
                lenToUnit = Spawners[i].transform.position - unitAttack;
            }
        }
        return indexNear;
    }

    private void DefanseBuilding()
    {
        var attackCount = 0;
        var defenseCount = 0;
        var nearPlayer = Vector3.zero;
        foreach(var comp in playerUnits)
        {
            if (comp == null)
                continue;
            var len = (comp.transform.position - transform.position).magnitude;
            if(len < 20
                && (nearPlayer == Vector3.zero 
                    || (nearPlayer - transform.position).magnitude < len))
            {
                nearPlayer = comp.transform.position;
                attackCount++;
            }
        }

        if(nearPlayer == Vector3.zero)
            return;

        foreach(var unit in units)
            if(unit != null 
                && (unit.transform.position - nearPlayer).magnitude < 20)
            {
                unit.finishPosition = nearPlayer;
                defenseCount++;
            }

        var indexNear = SpawnDefasePosition(nearPlayer);
        if(defenseCount < attackCount)
        {
            Spawners[indexNear].Spawn();
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

        DoAttackBuilding();
        DoAttackUnit();
        DefanseBuilding();
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
            .FindGameObjectsWithTag(gameObject.tag);
            var newBuildPlace = new Vector3(-x,-y,0) 
            + allBildings[Random.Range(0, allBildings.Length)]
                .transform.position;
            if((new Vector3(-x,-y,0)).magnitude < 7
            || !MainCamera.IsBounds(newBuildPlace))
                return;
            foreach(var obj in allBildings)
                if((obj.transform.position - newBuildPlace).magnitude < 7
                && obj.gameObject.name.Contains("Unit"))
                    return;
            Instantiate(building, 
                newBuildPlace, 
                Quaternion.identity);
            bot.resourcesCount -= coust;
            bildingCount ++;
        }
    }

    void Update()
    {
        //Задаем направление каждому юниту
        resourcesCount = bot.resourcesCount;
        playerUnits = UnitControl.units;
        if (Spawners.Count > 2)
            ControlArmy();
        var spawner = Spawners[Random.Range(0,2)];
        CreateBilding<EvilSpawner>(15, 
            spawner.gameObject);
        CreateBilding<TowerScript>(20, 
            Tower.gameObject);
    }
}