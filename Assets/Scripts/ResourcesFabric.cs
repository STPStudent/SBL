using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesFabric : HealthControl
{
    [SerializeField] private Text text;
    [SerializeField] private FabricResourceType type;
    private bool overflowed;
    [SerializeField] private int secondsWait = 5;
    [SerializeField] private int countAddAfterWait = 3;
    private float lastTime;
    private float deltaTime;
    public int resourcesCount = 0;

    void Start()
    {
        this.SetHealth();
        lastTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        if(PauseMenu.GameIsPause)
        {
            deltaTime = Time.realtimeSinceStartup - lastTime;
            return;
        }
        var timeNow = Time.realtimeSinceStartup;
        if(timeNow - lastTime - deltaTime > secondsWait)
        {
            lastTime = timeNow;
            resourcesCount ++;
        }
        if(type == FabricResourceType.fork)
            text.text = resourcesCount.ToString();
    }
}