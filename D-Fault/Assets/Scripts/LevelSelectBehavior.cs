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

    private void Start()
    {
        GameObject gameProgress = GameObject.FindGameObjectWithTag("GameProgress");
        GameProgress gp = null;
        if (gameProgress != null)
        {
            gp = gameProgress.GetComponent<GameProgress>();
        }
        if (gp != null)
        {
            List<bool> levelUnlocked = gp.GetLevelUnlocked();
            for (int i = 0; i < levelUnlocked.Count; i++)
            {
                Button b = gameObject.transform.GetChild(i).GetComponent<Button>();
                b.interactable = levelUnlocked[i];
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