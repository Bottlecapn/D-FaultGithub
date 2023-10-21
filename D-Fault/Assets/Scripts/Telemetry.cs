using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telemetry : MonoBehaviour
{
    // Telemetry support
    private int mRestartTimes;
    private float mTimeEachLevel;
    // Start is called before the first frame update
    void Start()
    {
        mRestartTimes = 0;
        mTimeEachLevel = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        mTimeEachLevel += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R))
        {
            mRestartTimes++;
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
