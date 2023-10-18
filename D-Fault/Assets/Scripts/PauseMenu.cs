using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{


    public static bool Paused = false;


    public GameObject pauseUI;


    void Start()
    {
        pauseUI.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Resume();
        }
    }


    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }


    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }


    public void LevelSelect()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        SceneManager.LoadScene("LevelSelect");
    }


    public void MainMenu()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        SceneManager.LoadScene("MainMenu");
    }
}



