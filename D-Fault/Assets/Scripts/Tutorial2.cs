using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Tutorial2 : MonoBehaviour
{


    public GameObject PanelCanvas;
    public GameObject TextCanvas1;
    bool active1 = true;


    void Start()
    {
    }


    void Update()
    {
        if (active1)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                TextCanvas1.SetActive(false);
                PanelCanvas.SetActive(false);
                active1 = false;
            }
        }
    }

}



