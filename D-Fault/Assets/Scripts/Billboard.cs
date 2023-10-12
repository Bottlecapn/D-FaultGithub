using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // creates a basic "billboard" effect -- the object always faces the camera. 
    Camera mainCamera;
    [SerializeField] bool fixedRotation;
    void Start()
    {
        mainCamera = Camera.main;
    }
    
    void LateUpdate()
    {
        //Vector3 cameraPlane = new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);
        //transform.LookAt(cameraPlane, Vector3.up);
        //transform.Rotate(Vector3.right * -90);
        transform.LookAt(mainCamera.transform);
        transform.Rotate(0, 180, 0);
    }

    void FreeBillboard()
    {
        transform.LookAt(mainCamera.transform);
        transform.Rotate(0, 180, 0);
    }

    void FixedBillboard()
    {
        // create point in 3D space that is directly below the camera and aligned with this object's position.
        Vector3 cameraPlane = new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);
        //float yRotationSave = transform.rotation.y;
        //transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

        //FUCKING LOOK AT IT, ASSHOLE
        transform.LookAt(cameraPlane, Vector3.up);
        //transform.Rotate(Vector3.right *90);
    }
}
