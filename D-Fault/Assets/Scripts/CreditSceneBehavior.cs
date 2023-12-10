using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditSceneBehavior : MonoBehaviour
{
    private bool mIsRolling = true;
    private float mReturnTimer = 0.0f;
    private const float RETURN_TIME = 5.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if animation ends
        if (!mIsRolling)
        {
            // wait 5 seconds and return to main menu
            mReturnTimer += Time.deltaTime;
            if (mReturnTimer >= RETURN_TIME)
            {
                mReturnTimer = 0.0f;
                SceneManager.LoadScene(0);
            }
        }
    }

    // Call by credits rolling animation
    public void SetIsRolling(int rolling)
    {
        if (rolling == 0)
        {
            mIsRolling = false;
        }
        else
        {
            mIsRolling = true;
        }
    }
}
