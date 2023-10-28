using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Tutorial : MonoBehaviour
{


    public GameObject PanelCanvas;
    public GameObject SelectDiceTutorial;
    public GameObject UseWASDTutorial;
    public GameObject DiceAndHoleExplanation;
    public GameObject DiceNumber;
    public TMP_Text DiceMoves;
    public GameObject RightArrow;
    public GameObject DownArrow;
    bool enteredCollider = false;
    bool triggeredMethod = false;


    void Start()
    {
        SelectDiceTutorial.SetActive(true);
        UseWASDTutorial.SetActive(false);
        DiceAndHoleExplanation.SetActive(false);
        DiceNumber.SetActive(false);
    }


    void Update()
    {
        if (SelectDiceTutorial.activeSelf) {
            GameObject[] dice = GameObject.FindGameObjectsWithTag("Dice");
            DieBehavior die = dice[0].GetComponent<DieBehavior>();
            if (die.getmIsSelected()) {
                SelectDiceTutorial.SetActive(false);
                UseWASDTutorial.SetActive(true);
            }
        }
        if (UseWASDTutorial.activeSelf) {
            GameObject[] dice = GameObject.FindGameObjectsWithTag("Dice");
            DieBehavior die = dice[0].GetComponent<DieBehavior>();
            string movesLeft = die.getMoves().ToString();
            DiceMoves.text = movesLeft + " moves left";
            if (die.getMoves() < 7) {
                RightArrow.SetActive(false);
            }
        }
        if (DiceNumber.activeSelf && !enteredCollider && !triggeredMethod) {
            triggeredMethod = true;
            GameObject[] dice = GameObject.FindGameObjectsWithTag("Dice");
            DieBehavior die = dice[0].GetComponent<DieBehavior>();
            die.SetSelection(false);
            die.getCubeRenderer().material = die.getRedMaterial();
            Invoke("EnableMovement", 1.5f);
        }
        if (DiceNumber.activeSelf && enteredCollider) {
            GameObject[] dice = GameObject.FindGameObjectsWithTag("Dice");
            DieBehavior die = dice[0].GetComponent<DieBehavior>();
            die.SetSelection(true);
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                DiceNumber.SetActive(false);
                DownArrow.SetActive(false);
            }
        }
    }

    private void EnableMovement() {
        enteredCollider = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dice"))
        {
            UseWASDTutorial.SetActive(false);
            DiceAndHoleExplanation.SetActive(true);
            DiceNumber.SetActive(true);
        }
    }
}



