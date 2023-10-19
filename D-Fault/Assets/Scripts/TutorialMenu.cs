using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    public GameObject PanelCanvas;

    // Update is called once per frame
    void Update()
    {
        if (PanelCanvas.activeSelf) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                PanelCanvas.SetActive(false);
            }
        }
    }
}
