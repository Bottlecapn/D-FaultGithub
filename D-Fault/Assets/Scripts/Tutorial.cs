using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Tutorial : MonoBehaviour
{


    public GameObject PanelCanvas;
    public GameObject TextCanvas1;
    public GameObject TextCanvas2;
    public GameObject TextCanvas3;
    bool active1 = true;
    bool active2 = false;

    int counter = 0;


    void Start()
    {
        TextCanvas2.SetActive(false);
        TextCanvas3.SetActive(false);
    }


    void Update()
    {
        if (active1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                TextCanvas1.SetActive(false);
                TextCanvas2.SetActive(true);
                active1 = false;
                active2 = true;
            }
        }
        if (active2)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
            {
                TextCanvas2.SetActive(false);
                PanelCanvas.SetActive(false);
                active2 = false;
            }
        }
        if (!active1 && !active2)
        {

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
            {
                counter++;
            }
            if (counter.Equals(5))
            {
                PanelCanvas.SetActive(true);
                TextCanvas3.SetActive(true);
            }
            if (counter.Equals(6))
            {
                TextCanvas3.SetActive(false);
            }

        }
    }

}



