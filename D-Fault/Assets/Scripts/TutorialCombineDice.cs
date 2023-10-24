using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCombineDice : MonoBehaviour
{
    public GameObject PanelCanvas;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dice") || other.gameObject.CompareTag("Coin"))
        {
            PanelCanvas.SetActive(false);
        }
    }
}
