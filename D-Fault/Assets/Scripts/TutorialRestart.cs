using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRestart : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PanelCanvas;

    // Update is called once per frame
    void Start() {
        Debug.Log(PlayerPrefs.GetInt("HasRestarted"));
        if (PlayerPrefs.GetInt("HasRestarted") == 1) {
            Debug.Log("WRONG");
            PanelCanvas.SetActive(false);
        }
    }
    void Update()
    {
        if (PanelCanvas.activeSelf) {
            if (Input.GetKeyDown(KeyCode.R) && false) {
                PlayerPrefs.SetInt("HasRestarted", 1);
            }
        }
    }
}
