using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    GameObject mainCamera;
    float rotationSpeed = 0.5f;
    Quaternion defaultRotation;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindFirstObjectByType<Camera>().gameObject;
        defaultRotation = transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mainCamera.transform.LookAt(transform);
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            pivot.transform.Rotate(new Vector3(0,-90,0));
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            pivot.transform.Rotate(new Vector3(0, 90, 0));
        }*/
        if (Input.GetMouseButton(1))
        {
            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            transform.Rotate(Vector3.down, XaxisRotation);
            if (transform.rotation.x > 360)
            {
                transform.rotation = new Quaternion(transform.rotation.x - 360, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            }
        } 
        else
        {
            RestoreDefaultRotation();
        }
    }

    void RestoreDefaultRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, 0.2f);
    }
}
