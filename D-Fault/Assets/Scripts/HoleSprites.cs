using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoleSprites : Tile
{
    [SerializeField] int holeCount;
    int currentCount;
    TextMeshProUGUI countDisplay;
    AudioSource sfx;
    [SerializeField] AudioClip scoreSound, completeSound;
    NumberDisplay nums;

    private void Awake()
    {
        nums = GetComponent<NumberDisplay>();
        sfx = GetComponent<AudioSource>();
        countDisplay = GameObject.Find("GoalText").GetComponent<TextMeshProUGUI>();
        currentCount = holeCount;
        countDisplay.text = "Goal: " + currentCount.ToString();
        nums.UpdateNumber(currentCount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DieBehavior>())
        {
            StartCoroutine(ScoreSequence(other.gameObject));
        }

    }

    private IEnumerator ScoreSequence(GameObject dice)
    {
        DieBehavior d = dice.GetComponent<DieBehavior>();
        while (true)
        {
            if (dice == null && !nums.IsCounting()) {
                break;
            }
            if(nums.IsCounting())
                    yield return new WaitForEndOfFrame();
            yield return null;
        }

        StartCoroutine(nums.CountDown(currentCount, currentCount - d.Moves));
        while (true)
        {
            if (nums.IsCounting()) {
                yield return null;
            } else {
                break;
            }
        }
        currentCount -= d.Moves;
        d.Moves = 0;
        if (currentCount <= 0) {
            currentCount = 0;
            countDisplay.text = "Success!";
            sfx.PlayOneShot(completeSound);
        } else {
            sfx.PlayOneShot(scoreSound);
            print("WAH");
            countDisplay.text = "Goal: " + currentCount.ToString();
        }
        yield break;
    }
}
