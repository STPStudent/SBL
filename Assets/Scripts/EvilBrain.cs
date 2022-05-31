using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvilBrain : MonoBehaviour
{
    [SerializeField] private MainBuilding player;
    [SerializeField] private MainBuilding bot;
    [SerializeField] private TowerScript Tower;
    [SerializeField] private ResourcesFabric Fabric;
    public static List<EvilSpawner> Spawners;
    [SerializeField] private int spawnerCost;
    [SerializeField] private int towerCost;
    [SerializeField] private int fabricCost;
    private UnitComponent playerUnits;
    public static UnitComponent units;
    public static int unitCount = 0;
    public int resourcesCount = 0;
    private int fabricCount = 0;
    private int bildingCount = 0;
    private int spawnerCount = 0;
    private int towerCount = 0;

    public static void AddUnit(UnitComponent comp)
    {
        //Добавляет юнита бота в список
        if (units != null)
            units.nextComponent = comp;
        comp.previousComponent = units;
        units = comp;
        unitCount++;
    }

    public static void DeleteSpawner(EvilSpawner spawner)
        => Spawners.Remove(spawner);

    void Awake()
    {
        units = null;
        Spawners = new List<EvilSpawner>();
    }

    private Vector3 SetPoint(string goal, GameObject[] k)
    {
        var found = Vector3.zero;
        foreach (var player in k)
            if (player.gameObject.name.Contains(goal)
                && player.transform.position.magnitude < 50)
                found = player.gameObject.transform.position;
        return found;
    }

    private void DoAttackUnit()
    {
        foreach (var comp in playerUnits)
        {
            foreach (var unit in units)
                if (unit != null && comp != null &&
                    (unit.transform.position - comp.transform.position).magnitude < 10)
                    unit.finishPosition = comp.transform.position;
        }
    }

    private void TransformPositions(Vector3 goal)
    {
        foreach (var unit in units)
            unit.finishPosition = goal;
    }

    private void DoAttackBuilding()
    {
        if (unitCount > 10)
        {
            var playerBuilding = GameObject.FindGameObjectsWithTag("Player");
            var pointMain = SetPoint("PlayerMainBuild", playerBuilding);
            var pointFabric = SetPoint("Fabric", playerBuilding);
            var pointTower = SetPoint("Tower", playerBuilding);
            if (pointTower != Vector3.zero)
                TransformPositions(pointTower);
            else if (pointFabric != Vector3.zero)
                TransformPositions(pointFabric);
            else if (pointMain != Vector3.zero)
                TransformPositions(pointMain);
        }
    }

    private int SpawnDefasePosition(Vector3 unitAttack)
    {
        var indexNear = 0;
        var lenToUnit = Vector3.zero;
        for (var i = 0; i < Spawners.Count(); i++)
        {
            if (lenToUnit == Vector3.zero
                || lenToUnit.magnitude >
                (Spawners[i].transform.position - unitAttack).magnitude)
            {
                indexNear = i;
                lenToUnit = Spawners[i].transform.position - unitAttack;
            }
        }

        return indexNear;
    }

    private void DefenseBuilding()
    {
        var attackCount = 0;
        var defenseCount = 0;
        var nearPlayer = Vector3.zero;
        foreach (var comp in playerUnits)
        {
            if (comp == null)
                continue;
            var len = (comp.transform.position - transform.position).magnitude;
            if (len < 20 && (nearPlayer == Vector3.zero || (nearPlayer - transform.position).magnitude < len))
            {
                nearPlayer = comp.transform.position;
                attackCount++;
            }
        }

        if (nearPlayer == Vector3.zero)
            return;

        foreach (var unit in units)
            if (unit != null
                && (unit.transform.position - nearPlayer).magnitude < 20)
            {
                unit.finishPosition = nearPlayer;
                defenseCount++;
            }

        var indexNear = SpawnDefasePosition(nearPlayer);
        if (defenseCount < attackCount)
        {
            Spawners[indexNear].Spawn();
        }
    }

    private void ControlArmy()
    {
        if (unitCount < 5
            || unitCount * 3 < UnitControl.unitCount * 2)
        {
            var k = Random.Range(0, Spawners.Count);
            while (Spawners[k].transform.position.magnitude > 50)
                k = Random.Range(0, Spawners.Count);
            Spawners[k].Spawn();
        }

        if (units == null)
            return;
        DoAttackBuilding();
        DoAttackUnit();
        DefenseBuilding();
    }

    private int CreateBilding(
        int cost,
        GameObject building
    )
    {
        if (resourcesCount > cost)
        {
            var x = Random.Range(-10.0f, 5);
            var right = Mathf.Sqrt(10.0f * 10.0f - x * x);
            var y = Random.Range(-right, 5);
            var allBildings = GameObject
                .FindGameObjectsWithTag(gameObject.tag);
            var buildingPlace = allBildings[Random.Range(0, allBildings.Length)]
                .transform.position;
            var newBuildPlace = new Vector3(x, y, 0)
                                + buildingPlace;
            if ((new Vector3(-x, -y, 0)).magnitude < 7
                || !MainCamera.IsBounds(newBuildPlace))
                return 0;
            foreach (var obj in allBildings)
                if ((obj.transform.position - newBuildPlace).magnitude < 7
                    && obj.gameObject.name.Contains("Unit"))
                    return 0;
            Instantiate(building,
                newBuildPlace,
                Quaternion.identity);
            bot.resourcesCount -= cost;
            return 1;
        }

        return 0;
    }

    void Update()
    {
        //Задаем направление каждому юниту
        resourcesCount = bot.resourcesCount;
        playerUnits = UnitControl.units;
        if (Spawners.Count > 2)
            ControlArmy();
        if (CreateBilding(fabricCost, Fabric.gameObject) == 1)
        {
            fabricCost *= 2;
            fabricCount++;
        }

        if (fabricCount == 0)
            return;
        var spawner = Spawners[Random.Range(0, 2)];
        if ((spawnerCount + towerCount) % 3 == 1)
            towerCount += CreateBilding(towerCost, Tower.gameObject);
        else
            spawnerCount += CreateBilding(spawnerCost, spawner.gameObject);
    }
}