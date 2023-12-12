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
    [SerializeField]
    ParticleSystem completionParticles;
    [SerializeField]
    ParticleSystem scoreParticles;

    private void Awake()
    {
        holeNumberDisplay = GetComponent<NumberDisplay>();
        sfx = GetComponent<AudioSource>();
        mCurrentHoleCount = holePar;
        holeNumberDisplay.UpdateNumber(mCurrentHoleCount);
        completionParticles = GetComponent<ParticleSystem>();
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
        DieBehavior d = dice.GetComponent<DieBehavior>();

        // play initial scoring sound
        sfx.pitch = 1f;
        if (d.Moves > 0)
        {
            if (dice.CompareTag("Dice"))
            {
                sfx.PlayOneShot(scoreSoundDice);
            }
            else if (dice.CompareTag("Coin"))
            {
                sfx.PlayOneShot(scoreSoundCoin);
            }
        }

        // waits until die is destroyed AND the previous coroutine is finished before counting down.

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
        if (d.Moves > 0)
        {
            /*if (scoreParticles.isPlaying())
            {
                scoreParticles.Stop();
            }*/
            scoreParticles.Play();
        }
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


        // if the hole is already 0, do not play any additional effects or sounds.
        if (mCurrentHoleCount <= 0)
        {
            mCompleted = true;
            d.Moves = 0;
        }
        else
        {
            mCurrentHoleCount -= d.Moves;
            d.Moves = 0;
            if (mCurrentHoleCount <= 0)
            {
                mCurrentHoleCount = 0;
                sfx.PlayOneShot(completeSound);
                completionParticles.Play();
                scoreParticles.Stop();
                mCompleted = true;
            }
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
