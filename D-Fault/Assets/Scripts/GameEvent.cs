using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvent : MonoBehaviour
{
    public List<DieBehavior> mDice;
    private bool mSelectionDisabled = false;
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
    }
}
