using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public List<DieBehavior> mDice;
    private bool mSelectionDisabled = false;
    // Telemetry support
    private int mRestartTimes = 0;
    private float mTimeEachLevel = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mTimeEachLevel += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R))
        {
            mRestartTimes++;
        }

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

    public int GetRestartTimes() 
    { 
        return mRestartTimes; 
    }

    public float GetTimeEachLevel()
    {
        return mTimeEachLevel;
    }
}
