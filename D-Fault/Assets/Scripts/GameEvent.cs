using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvent : MonoBehaviour
{
    public List<DieBehavior> mDice;
    private bool mSelectionDisabled = false;
    public List<HoleBehavior> mHoles;
    private bool mLevelCompleted = false;
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
        // every frame, if no die is moving, enable selection
        mSelectionDisabled = false;
        foreach (var dice in mDice)
        {
            DieBehavior db = dice.GetComponent<DieBehavior>();
            // if any die is moving, disable selection
            if (db != null && db.GetIsMoving())
            {
                mSelectionDisabled = true;
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
        foreach (var hole in mHoles)
        {
            HoleBehavior hb = hole.GetComponent<HoleBehavior>();
            // if any hole is not completed, exit the loop
            if (!hb.GetCompleted())
            {
                break;
            }
            // if all holes are completed
            mLevelCompleted = true;
        }

        // if this level is completed, load transition scene
        if (mLevelCompleted)
        {
            print("Level Cleared");
            PlayerPrefs.SetInt("buildIndex", SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene("LevelTransition");
        }
    }
}
