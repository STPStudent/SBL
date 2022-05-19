using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause;
    private static bool isSettings;
    public GameObject pauseMenuUI;
    public GameObject resources;
    public GameObject settings;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isSettings)
                DisableSettings();
            else
            {
                if (GameIsPause)
                    Resume();
                else
                    Pause();
            }
        }
           
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        resources.SetActive(false);
        Time.timeScale = 0f;
        GameIsPause = true;  

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        resources.SetActive(true);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    public void ActiveSettings()
    {
        pauseMenuUI.SetActive(false);
        isSettings = true;
        settings.SetActive(isSettings);
    }

    public void DisableSettings()
    {
        pauseMenuUI.SetActive(true);
        isSettings = false;
        settings.SetActive(isSettings);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
