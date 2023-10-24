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
    public GameObject TextCanvas4;
    public GameObject TextCanvas5;
    public GameObject TextCanvas6;
    public GameObject TextCanvas7;
    public GameObject TextCanvas8;


    void Start()
    {
        TextCanvas1.SetActive(true);
        TextCanvas2.SetActive(false);
        TextCanvas3.SetActive(false);
        TextCanvas4.SetActive(false);
        TextCanvas5.SetActive(false);
        TextCanvas6.SetActive(false);
        TextCanvas7.SetActive(false);
        TextCanvas8.SetActive(false);
    }


    void Update()
    {
        if (TextCanvas1.activeSelf) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                TextCanvas1.SetActive(false);
                TextCanvas2.SetActive(true);
            }
        }
        if (TextCanvas4.activeSelf) {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                TextCanvas4.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dice"))
        {
            TextCanvas2.SetActive(false);
            TextCanvas3.SetActive(true);
            TextCanvas4.SetActive(true);
            TextCanvas5.SetActive(true);
            TextCanvas6.SetActive(true);
            TextCanvas7.SetActive(true);
            TextCanvas8.SetActive(true);
        }
    }
}



