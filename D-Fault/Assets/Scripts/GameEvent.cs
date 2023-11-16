using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvent : MonoBehaviour
{
    public List<DieBehavior> mDice;
    private bool mSelectionDisabled = false;
    public List<HoleBehavior> mHoles;
    private bool mLevelCompleted = false;
    // Restart screen pops up 1 second later after fail
    private float restartScreenTimer = 0.0f;
    private const float RESTART_SCREEN_TIME = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        // Reset telemetry data variables
        GameObject TelemetryManager = GameObject.FindGameObjectWithTag("Telemetry");
        Telemetry tele = null;
        if (TelemetryManager != null)
        {
            tele = TelemetryManager.GetComponent<Telemetry>();
        }
        // if current scene is different from previous scene, reset Telemetry varaibles
        if (tele != null && SceneManager.GetActiveScene().buildIndex != tele.GetPreviousLevel())
        {
            tele.SetPreviousLevel(SceneManager.GetActiveScene().buildIndex);
            tele.ResetRestartTimes();
            tele.ResetTimeEachLevel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Disable selection if in pause menu OR if any OBJECT IS MOVING
        ToggleSelectionDisabled();

        // update mCanSelect of all dice every frame
        // If selection is disabled, set all dice unselectable, vice versa
        foreach (var dice in mDice)
        {
            DieBehavior db = dice.GetComponent<DieBehavior>();
            if (db != null)
            {
                db.SetCanSelect(!mSelectionDisabled);
            }
        }

        // every frame, check if every hole is completed
        mLevelCompleted = mHoles[0].GetComponent<HoleBehavior>().GetCompleted();
        foreach (var hole in mHoles)
        {
            HoleBehavior hb = hole.GetComponent<HoleBehavior>();
            if (hb != null)
            {
                mLevelCompleted &= hb.GetCompleted();
            }
        }

        // if this level is completed, load transition scene
        if (mLevelCompleted)
        {
            OutputTelemetryData();
            EnableLevelSelectButton();

            PlayerPrefs.SetInt("buildIndex", SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene("LevelTransition");
        }

        // Restart screen pops up if all objects hit 0
        if (ShouldRestart())
        {
            // Allow time for hitting the wall animation
            restartScreenTimer += Time.deltaTime;
            if (restartScreenTimer >= RESTART_SCREEN_TIME)
            {
                restartScreenTimer = 0.0f;
                TutorialRestart tr = GameObject.FindGameObjectWithTag("RestartCanvas").GetComponent<TutorialRestart>();
                GameObject oth = GameObject.FindGameObjectWithTag("TutorialCanvas");
                if (oth != null)
                {
                    if (oth.activeSelf)
                    {
                        oth.SetActive(false);
                    }
                }
                // if the restart canvas is not active
                if (!tr.PanelCanvas.activeSelf)
                {
                    // spawn restart screen
                    tr.PanelCanvas.SetActive(true);
                }
            }
        }
    }

    // Disable selection if in pause menu OR if any OBJECT IS MOVING
    private void ToggleSelectionDisabled()
    {
        // every frame, if in pause menu
        if (GameObject.FindGameObjectWithTag("PauseMenuCanvas").GetComponent<PauseMenu>().Paused)
        {
            // Disable selection and unselect all dice
            mSelectionDisabled = true;
            foreach (var dice in mDice)
            {
                DieBehavior db = dice.GetComponent<DieBehavior>();
                if (db != null)
                {
                    db.SetSelection(false);
                }
            }
        }
        // if not in pause menu
        else
        {
            // Enable selection
            mSelectionDisabled = false;
            // Check if there's any objects moving
            foreach (var dice in mDice)
            {
                DieBehavior db = dice.GetComponent<DieBehavior>();
                // if any object is moving, disable selection
                if (db != null)
                {
                    mSelectionDisabled |= db.GetIsMoving();
                }
            }
        }
    }

    // Check the condition of restart screen display
    private bool ShouldRestart()
    {
        bool allDiceDead = false;
        bool allHolesCompleted = false;
        bool holesCountingDown = false;
        // Check if all dice and coins reaches 0
        if (mDice.Any())
        {
            allDiceDead = (mDice[0].GetComponent<DieBehavior>().Moves == 0);
            foreach (var dice in mDice)
            {
                DieBehavior diceBehavior = dice.GetComponent<DieBehavior>();
                allDiceDead &= (diceBehavior.Moves == 0);
            }
        }
        // Check all hole numbers if no dice are alive
        else
        {
            allDiceDead = true;
            allHolesCompleted = (mHoles[0].GetComponent<HoleBehavior>().GetCurrentHoleCount() == 0);
            foreach (var hole in mHoles)
            {
                HoleBehavior holeBehavior = hole.GetComponent<HoleBehavior>();
                allHolesCompleted &= (holeBehavior.GetCurrentHoleCount() == 0);
                if(holeBehavior.GetIsCounting() == true)
                {
                    holesCountingDown = true;
                }
            }
        }
        return allDiceDead && !allHolesCompleted && !holesCountingDown;
    }

    private void OutputTelemetryData()
    {
        // output telemtry data
        GameObject TelemetryManager = GameObject.FindGameObjectWithTag("Telemetry");
        Telemetry tele = null;
        if (TelemetryManager != null)
        {
            tele = TelemetryManager.GetComponent<Telemetry>();
        }
        if (tele != null)
        {
            // only outputing Telemetry data for the level scenes
            // NOTE: CHANGE THE RANGE IF MORE LEVELS ARE ADDED
            if (SceneManager.GetActiveScene().buildIndex >= 1 && SceneManager.GetActiveScene().buildIndex <= 24)
            {
                string filePath = Path.Combine(Application.streamingAssetsPath, "TelemetryData.txt");
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine("Level " + SceneManager.GetActiveScene().buildIndex + ": Restarts: " + tele.GetRestartTimes() + " Time: " + tele.GetTimeEachLevel());
                sw.Close();
            }
        }
    }

    private void EnableLevelSelectButton()
    {
        // enable next level select button
        GameObject GameProgress = GameObject.FindGameObjectWithTag("GameProgress");
        GameProgress gp = null;
        if (GameProgress != null)
        {
            gp = GameProgress.GetComponent<GameProgress>();
        }
        if (gp != null)
        {
            // Every time a level is completed, enable the level button for the next level
            int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
            // NOTE: CHANGE THE RANGE IF MORE LEVELS ARE ADDED
            if (nextSceneBuildIndex >= 2 && nextSceneBuildIndex <= 24)
            {
                gp.GetLevelUnlocked()[nextSceneBuildIndex - 1] = true;
                //print(gp.GetLevelUnlocked()[1]);
            }
        }
    }
}
