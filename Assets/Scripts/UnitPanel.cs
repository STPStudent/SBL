using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitPanel : MonoBehaviour, IPointerDownHandler
{
    public GameObject Fabric;
    [SerializeField] public Image circle;
    [SerializeField] public Image stop;
    public int Cost;
    [SerializeField] private MainBuilding mainBuilding;
    [SerializeField] private Text text;
    public List<UnitSpawner> Spawners;
    private float lastTime = 0;
    private int unitToSpawn = 0;
    private bool enabled = false;

    void Start(){
        Spawners = new List<UnitSpawner>();
    }

    public void Update()
    {
        if(Spawners.Count > 0)
            stop.fillAmount = 0;
        else
            stop.fillAmount = 1;
        var deltaTime = Time.time - lastTime;
        lastTime = Time.time;
        if (circle.fillAmount - deltaTime / 15 < 0)
        {
            if(unitToSpawn == 0)
                circle.fillAmount = 0;
            else
            {
                unitToSpawn --;
                text.text = unitToSpawn.ToString();
                circle.fillAmount = 1f;
                Spawners[0].Spawn();
            }
        }
        else
            circle.fillAmount -= deltaTime / 15;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (mainBuilding.resourcesCount >= Cost && Spawners.Count != 0)
        {
            unitToSpawn ++;
            text.text = unitToSpawn.ToString();
            mainBuilding.resourcesCount -= Cost;
        }
    }
}