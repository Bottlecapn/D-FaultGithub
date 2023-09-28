using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleSprites : Tile
{
    [SerializeField] string nextScene;
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
        //countDisplay = GameObject.Find("GoalText").GetComponent<TextMeshProUGUI>();
        currentCount = holeCount;
        //countDisplay.text = "Goal: " + currentCount.ToString();
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
        sfx.pitch = 1f;
        sfx.PlayOneShot(scoreSound);
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
            //countDisplay.text = "Success!";
            sfx.PlayOneShot(completeSound);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        } else {
            //sfx.PlayOneShot(scoreSound);
            print("WAH");
            //countDisplay.text = "Goal: " + currentCount.ToString();
        }
        yield break;
    }

    public void SetHoleCount(int holeNum)
    {
        currentCount = holeNum;
        nums.UpdateNumber(currentCount);
    }
}
