using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        sceneName = "AnimTesting";
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

    public void Level1()
    {
        sceneName = "Level1";
        Invoke("ChangeScene", 1);
    }

    public void Level2()
    {
        sceneName = "Level2";
        Invoke("ChangeScene", 1);
    }

    public void Level3()
    {
        sceneName = "Level3";
        Invoke("ChangeScene", 1);
    }
    /*
        public void Level4() {
            sceneName = "Level4";
            Invoke("ChangeScene", 1);
        }

        public void Level5() {
            sceneName = "Level5";
            Invoke("ChangeScene", 1);
        }

        public void Level6() {
            sceneName = "Level6";
            Invoke("ChangeScene", 1);
        }

        public void Level7() {
            sceneName = "Level7";
            Invoke("ChangeScene", 1);
        }

        public void Level8() {
            sceneName = "Level8";
            Invoke("ChangeScene", 1);
        }

        public void Level9() {
            sceneName = "Level9";
            Invoke("ChangeScene", 1);
        }

        public void Level10() {
            sceneName = "Level10";
            Invoke("ChangeScene", 1);
        }

        public void Level11() {
            sceneName = "Level11";
            Invoke("ChangeScene", 1);
        }

        public void Level12() {
            sceneName = "Level12";
            Invoke("ChangeScene", 1);
        }

        public void Level13() {
            sceneName = "Level13";
            Invoke("ChangeScene", 1);
        }

        public void Level14() {
            sceneName = "Level14";
            Invoke("ChangeScene", 1);
        }

        public void Level15() {
            sceneName = "Level15";
            Invoke("ChangeScene", 1);
        }

        public void Level16() {
            sceneName = "Level16";
            Invoke("ChangeScene", 1);
        }

        public void Level17() {
            sceneName = "Level17";
            Invoke("ChangeScene", 1);
        }

        public void Level18() {
            sceneName = "Level18";
            Invoke("ChangeScene", 1);
        }

        public void Level19() {
            sceneName = "Level19";
            Invoke("ChangeScene", 1);
        }

        public void Level20() {
            sceneName = "Level20";
            Invoke("ChangeScene", 1);
        }
    */
    public void QuitGame()
    {
        Application.Quit();
    }
}