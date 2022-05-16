using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause;
    public static bool IsSettings;
    public GameObject pauseMenuUI;
    public GameObject resourses;
    public GameObject settings;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsSettings)
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
        resourses.SetActive(false);
        Time.timeScale = 0f;
        GameIsPause = true;  

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        resourses.SetActive(true);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    public void ActiveSettings()
    {
        pauseMenuUI.SetActive(false);
        IsSettings = true;
        settings.SetActive(IsSettings);
    }

    public void DisableSettings()
    {
        pauseMenuUI.SetActive(true);
        IsSettings = false;
        settings.SetActive(IsSettings);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
