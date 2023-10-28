using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelNumber : MonoBehaviour
{

    public TMP_Text LevelText;
    // Start is called before the first frame update
    void Awake()
    {
        LevelText.text = "Level " + SceneManager.GetActiveScene().buildIndex.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
