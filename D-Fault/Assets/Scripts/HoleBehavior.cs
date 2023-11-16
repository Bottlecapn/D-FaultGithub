using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleBehavior : Tile
{
    [SerializeField] int holePar;
    private int mCurrentHoleCount;
    AudioSource sfx;
    [SerializeField] AudioClip scoreSoundCoin, scoreSoundDice, completeSound;
    NumberDisplay holeNumberDisplay;
    private bool mCompleted = false; // for game event manager
    private bool mCountingDown = false; // for game event manager

    private void Awake()
    {
        holeNumberDisplay = GetComponent<NumberDisplay>();
        sfx = GetComponent<AudioSource>();
        mCurrentHoleCount = holePar;
        holeNumberDisplay.UpdateNumber(mCurrentHoleCount);
    }


    private void OnTriggerEnter(Collider other)
    {
        // if a die runs into the hole, start scoring sequence coroutine
        if (other.gameObject.GetComponent<DieBehavior>())
        {
            StartCoroutine(ScoreSequence(other.gameObject));
        }
    }

    // Coroutine that starts the scoring sequence and number counting down.
    private IEnumerator ScoreSequence(GameObject dice)
    {
        mCountingDown = true;
        // play initial scoring sound
        sfx.pitch = 1f;
        if (dice.CompareTag("Dice")) { 
            sfx.PlayOneShot(scoreSoundDice);
        } else if (dice.CompareTag("Coin"))
        {
            sfx.PlayOneShot(scoreSoundCoin);
        }

        // waits until die is destroyed AND the previous coroutine is finished before counting down.
        DieBehavior d = dice.GetComponent<DieBehavior>();
        while (true)
        {
            if (dice == null && !holeNumberDisplay.IsCounting())
            {
                break;
            }
            if (holeNumberDisplay.IsCounting())
                yield return new WaitForEndOfFrame();
            yield return null;
        }

        // Calls the CountDown coroutine in NumberDisplay.
        // while that coroutine is running, this one is paused.
        StartCoroutine(holeNumberDisplay.CountDown(mCurrentHoleCount, Mathf.Clamp(mCurrentHoleCount - d.Moves, 0, 1000)));
        while (true)
        {
            if (holeNumberDisplay.IsCounting())
            {
                yield return null;
            }
            else
            {
                break;
            }
        }

        // after CountDown is finished, update the actual hole count to subtract die value.
        // wait for a second if level is complete before loading the next one.
        mCurrentHoleCount -= d.Moves;
        d.Moves = 0;
        if (mCurrentHoleCount <= 0)
        {
            mCurrentHoleCount = 0;
            sfx.PlayOneShot(completeSound);
            yield return new WaitForSeconds(1);
            mCompleted = true;
        }
        else
        {
            print("Scored");
        }
        mCountingDown = false;
        yield break;
    }

    // Sets the hole's "par" (number needed to clear the level).
    public void SetHoleCount(int holePar)
    {
        mCurrentHoleCount = holePar;
        holeNumberDisplay.UpdateNumber(mCurrentHoleCount);
    }

    public bool GetCompleted()
    {
        return mCompleted;
    }

    public int GetCurrentHoleCount()
    {
        return mCurrentHoleCount;
    }

    public bool GetIsCounting()
    {
        return mCountingDown;
    }
}
