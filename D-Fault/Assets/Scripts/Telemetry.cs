using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Telemetry : MonoBehaviour
{
    // Telemetry support
    private int mRestartTimes = 0;
    private float mTimeEachLevel = 0.0f;
    private static Telemetry telemetry = null;
    private int mPreviousLevel = 0;

    void Awake()
    {
        // static variable "telemetry" gets set to this Class instance if there is none yet.
        if (telemetry == null)
        {
            telemetry = this;
            DontDestroyOnLoad(gameObject);
        }

        // because "telemetry" is static, it will be shared across all instances of this Class. So if there exists another instance already when this one is created, destroy itself. 
        else if (telemetry != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

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
    }
    public int GetRestartTimes()
    {
        return mRestartTimes;
    }

    public void ResetRestartTimes()
    {
        mRestartTimes = 0;
    }

    public float GetTimeEachLevel()
    {
        return mTimeEachLevel;
    }

    public void ResetTimeEachLevel()
    {
        mTimeEachLevel = 0.0f;
    }

    public int GetPreviousLevel()
    {
        return mPreviousLevel;
    }

    public void SetPreviousLevel(int level)
    {
        mPreviousLevel = level;
    }
}
