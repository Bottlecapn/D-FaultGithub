using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelectBehavior : MonoBehaviour
{
    static string sceneName = "";
    int buildIndexNumber = -1;

    float delay = 0.2f;

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

}