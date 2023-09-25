using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hole : Tile
{
    [SerializeField] int holeCount;
    int currentCount;
    TextMeshProUGUI countDisplay;
    AudioSource sfx;
    [SerializeField] AudioClip scoreSound, completeSound;

    private void Awake()
    {
        sfx = GetComponent<AudioSource>();
        countDisplay = GameObject.Find("GoalText").GetComponent<TextMeshProUGUI>();
        currentCount = holeCount;
        countDisplay.text = "Goal: " + currentCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DieBehavior>())
        {
            DieBehavior d = other.gameObject.GetComponent<DieBehavior>();
            currentCount -= d.Moves;
            d.Moves = 0;
            if(currentCount <= 0) { 
                currentCount = 0;
                countDisplay.text = "Success!";
                sfx.PlayOneShot(completeSound);
            } else
            {
                sfx.PlayOneShot(scoreSound);
                countDisplay.text = "Goal: " + currentCount.ToString();
            }
        }

    }
}
