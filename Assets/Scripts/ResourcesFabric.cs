using UnityEngine;
using UnityEngine.UI;

public class ResourcesFabric : HealthControl
{
    [SerializeField] private Text text;
    private float secondsWait = 0.85f;
    private float countAddAfterWait = 1;
    private float lastTime;
    private float deltaTime;
    public int resourcesCount;

    void Start()
    {
        SetHealth();
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
            resourcesCount += (int)countAddAfterWait;
        }
        if(text != null)
            text.text = resourcesCount.ToString();
    }
}