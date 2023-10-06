using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    static string sceneName;
    private void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
        sceneName = "";
    }

    public void PlayGame()
    {
        sceneName = "Level1";
        Invoke("ChangeScene", 1);
    }

    public void LevelSelect()
    {
        sceneName = "LevelSelect";
        Invoke("ChangeScene", 1);
    }

    public void ReturnMainMenu()
    {
        sceneName = "MainMenu";
        Invoke("ChangeScene", 1);
    }

    public void Level(Button b)
    {
        string txt = b.GetComponentInChildren<TMP_Text>().text;
        sceneName = "Level" + txt;
        Invoke("ChangeScene", 1);
    }

    public void LevelTransition()
    {
        int buildIndex = PlayerPrefs.GetInt("buildIndex");
        SceneManager.LoadScene(buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}