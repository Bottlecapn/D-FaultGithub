using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private TMP_Text levelNum;
    void Awake() {
        levelNum.text = ((PlayerPrefs.GetInt("buildIndex"))-1).ToString();
    }
}
