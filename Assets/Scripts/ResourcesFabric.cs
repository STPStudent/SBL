using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesFabric : HealthControl
{
    [SerializeField] private Text text;
    [SerializeField] private int secondsWait = 5;
    [SerializeField] private int countAddAfterWait = 1;
    [SerializeField] private MainBuilding mainBuilding;
    private float lastTime;

    void Start()
    {
        SetHealth();
        lastTime = Time.time;
    }

    void Update()
    {
        if(PauseMenu.GameIsPause)
            return;
        
        var timeNow = Time.time;
        if(timeNow - lastTime > secondsWait)
        {
            lastTime = timeNow;
            mainBuilding.resourcesCount += countAddAfterWait;
        }
        if(text != null)
            text.text = mainBuilding.resourcesCount.ToString();
    }
}