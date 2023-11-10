using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public bool Paused = false;
    public GameObject pauseUI;
    AudioSource sfx;
    [SerializeField]
    AudioClip restartSound;

    void Start()
    {
        pauseUI.SetActive(false);
        sfx = gameObject.GetComponent<AudioSource>();
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
            if (Paused) 
            { 
                Resume();
            } 
            else
            {
                sfx.PlayOneShot(restartSound);
                Invoke("Restart", 0.5f);
            }
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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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



