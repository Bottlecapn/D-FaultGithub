using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hole : Tile
{
    [SerializeField] int holeCount;
    int currentCount;
    [SerializeField] TextMeshPro countDisplay;

    private void Awake()
    {
        countDisplay = GameObject.Find("GoalText").GetComponent<TextMeshPro>();
        currentCount = holeCount;
    }

    private void Update()
    {
        countDisplay.text = "Goal: " + currentCount.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Die>())
        {
            Die d = other.gameObject.GetComponent<Die>();
            currentCount -= d.Moves;
        }
        Destroy(other.gameObject);
        print("aa");
    }
}
