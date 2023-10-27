using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    static string sceneName = "";
    int buildIndexNumber = -1;

    float delay = 0.2f;

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("LevelTransition")) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                buildIndexNumber = PlayerPrefs.GetInt("buildIndex");
                Invoke("ChangeScene", delay);
            }
        }
    }

    private void ChangeScene()
    {
        if (sceneName == "")
        {
            SceneManager.LoadScene(buildIndexNumber);
            buildIndexNumber = -1;
        }
        else if (buildIndexNumber == -1)
        {
            SceneManager.LoadScene(sceneName);
            sceneName = "";
        }
    }

    public void PlayGame()
    {
        sceneName = "Level1";
        Invoke("ChangeScene", 0.8f);
    }

    public void LevelSelect()
    {
        sceneName = "LevelSelect";
        Invoke("ChangeScene", delay);
    }

    public void ReturnMainMenu()
    {
        sceneName = "MainMenu";
        Invoke("ChangeScene", delay);
    }

    public void Level(Button b)
    {
        string txt = b.GetComponentInChildren<TMP_Text>().text;
        sceneName = "Level" + txt;
        Invoke("ChangeScene", delay);
    }

    public void LevelTransition()
    {
        buildIndexNumber = PlayerPrefs.GetInt("buildIndex");
        Invoke("ChangeScene", delay);
    }

    public void LevelRetry()
    {
        buildIndexNumber = PlayerPrefs.GetInt("buildIndex") - 1;
        Invoke("ChangeScene", delay);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}