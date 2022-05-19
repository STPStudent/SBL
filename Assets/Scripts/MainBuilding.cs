using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuilding : HealthControl
{
    [SerializeField] private GameObject result;
    public int resourcesCount = 0;
    
    void Start()
    {
        SetHealth();
    }

    public override void DestroyObject()
    {
        PauseMenu.GameIsPause = true;
        Time.timeScale = 0f;
        result.SetActive(true);
    }
}
