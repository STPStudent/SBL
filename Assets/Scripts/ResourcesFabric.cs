using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesFabric : HealthControl
{
    [SerializeField] private Text text;
    [SerializeField] private FabricResourceType type;
    [SerializeField] private int secondsWait = 5;
    [SerializeField] private int countAddAfterWait = 1;
    [SerializeField] private MainBilding mainBilding;
    private float lastTime;

    void Start()
    {
        this.SetHealth();
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
            mainBilding.resourcesCount += countAddAfterWait;
        }
        if(text != null)
            text.text = mainBilding.resourcesCount.ToString();
    }
}