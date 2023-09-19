using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        Invoke("PlayGameInvoke", 2);
    }
    private void PlayGameInvoke()
    {
        SceneManager.LoadScene("GridWork");
    }

    public void LevelSelect()
    {
        Invoke("LevelSelectInvoke", 1);
    }

    private void LevelSelectInvoke()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    public void OptionsMenu()
    {
        Invoke("OptionsMenuInvoke", 1);
    }

    private void OptionsMenuInvoke()
    {
        SceneManager.LoadScene("Options");
    }


    /*
    Go back to main menu
    public void MainMenu()
    {
        Invoke("MainMenuInvoke", 1);
    }
    public void MainMenuInvoke()
    {
        SceneManager.LoadScene("MainMenu");
    }*/

    public void QuitGame()
    {
        Application.Quit();
    }
}
