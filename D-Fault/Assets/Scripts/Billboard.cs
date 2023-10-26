using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // creates a basic "billboard" effect -- the object always faces the camera. 
    Camera mainCamera;
    [SerializeField] bool fixedRotation;
    bool rotationLocked;
    void Start()
    {
        mainCamera = Camera.main;
        rotationLocked = false;
    }
    
    void LateUpdate()
    {
        if (!rotationLocked && fixedRotation) { 
            /*Vector3 cameraPlane = new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);
            transform.LookAt(cameraPlane, Vector3.up);
            transform.Rotate(Vector3.right * -90);*/
            FixedBillboard();
        }
        if (!fixedRotation)
        {
            FreeBillboard();
        }
        //transform.LookAt(mainCamera.transform);
        //transform.Rotate(0, 180, 0);
    }

    public void LockRotation(bool yn)
    {
        rotationLocked = yn;
    }
    void FreeBillboard()
    {
        //transform.LookAt(mainCamera.transform);
        //transform.Rotate(0, 180, 0);
    }

    void FixedBillboard()
    {
        //Vector3 cameraPlane = new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);
        //transform.LookAt(Vector3.forward, Vector3.up);
        //transform.Rotate(Vector3.right * -90);
        //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, 1);
    }
}
