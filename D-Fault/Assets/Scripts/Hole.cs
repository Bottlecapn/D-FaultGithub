using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : Tile
{
    [SerializeField] int holeCount;
    int currentCount;
    [SerializeField] TextMesh countDisplay;

    private void Start()
    {
        currentCount = holeCount;
    }

    private void Update()
    {
        countDisplay.text = currentCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Die>())
        {
            Die d = other.gameObject.GetComponent<Die>();
            currentCount -= d.Moves;
        }
        Destroy(other.gameObject);
    }
}
