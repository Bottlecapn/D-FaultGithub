using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // creates a basic "billboard" effect -- the object always faces the camera. 
    Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    
    void LateUpdate()
    {
        transform.LookAt(mainCamera.transform);
        transform.Rotate(0, 180, 0);
    }
}
