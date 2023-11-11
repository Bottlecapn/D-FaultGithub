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

        // update mCanSelect of all dice every frame
        if (mSelectionDisabled)
        {
            foreach (var dice in mDice)
            {
                DieBehavior db = dice.GetComponent<DieBehavior>();
                if (db != null)
                {
                    db.SetCanSelect(false);
                }
            }
        }
        else
        {
            foreach (var dice in mDice)
            {
                DieBehavior db = dice.GetComponent<DieBehavior>();
                if (db != null)
                {
                    db.SetCanSelect(true);
                }
            }
        }

        // every frame, check if every hole is completed
        mLevelCompleted = mHoles[0].GetComponent<HoleBehavior>().GetCompleted();
        foreach (var hole in mHoles)
        {
            HoleBehavior hb = hole.GetComponent<HoleBehavior>();
            mLevelCompleted &= hb.GetCompleted();
        }

        // if this level is completed, load transition scene
        if (mLevelCompleted)
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
                if (SceneManager.GetActiveScene().buildIndex >= 1 && SceneManager.GetActiveScene().buildIndex <= 21)
                {
                    string filePath = Path.Combine(Application.streamingAssetsPath, "TelemetryData.txt");
                    StreamWriter sw = new StreamWriter(filePath, true);
                    sw.WriteLine("Level " + SceneManager.GetActiveScene().buildIndex + ": Restarts: " + tele.GetRestartTimes() + " Time: " + tele.GetTimeEachLevel());
                    sw.Close();
                }
            }
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
                if (oth != null) {
                    TutorialMenu tmu = oth.GetComponent<TutorialMenu>();
                    if (tmu.PanelCanvas.activeSelf) {
                    tmu.PanelCanvas.SetActive(false);
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

    private bool ShouldRestart()
    {
        bool allDiceDead = false;
        bool allHolesCompleted = false;
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
            }
        }
        return allDiceDead && !allHolesCompleted;
    }
}
