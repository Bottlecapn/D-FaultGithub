using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    GameObject mainCamera;
    Camera cam;
    //[SerializeField]
    float rotationSpeed = 3.5f;
    float camRotationTimer;
    Quaternion defaultRotation;
    float autoRotateSpeed = 0.25f;
    bool autoRotate;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindFirstObjectByType<Camera>().gameObject;
        cam = mainCamera.GetComponent<Camera>();
        defaultRotation = transform.rotation;
        camRotationTimer = 0f;
        autoRotate = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mainCamera.transform.LookAt(transform);

        if (Input.GetKeyDown(KeyCode.I))
        {
            autoRotate = !autoRotate;
        }
        // Rotate the camera pivot upon holding either mouse button.
        if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            camRotationTimer += Time.deltaTime;
            // the button must be held for a brief moment for camera rotation to be enabled.
            if (camRotationTimer >= 0.15f)
            {
                float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
                transform.Rotate(Vector3.up, XaxisRotation);
                if (transform.rotation.x > 360)
                {
                    transform.rotation = new Quaternion(transform.rotation.x - 360, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                }
            }
        }
        else if (autoRotate)
        {
            transform.Rotate(Vector3.up, autoRotateSpeed);
            if (transform.rotation.x > 360)
            {
                transform.rotation = new Quaternion(transform.rotation.x - 360, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            }
        }
        else

        {
            RestoreDefaultRotation();
            camRotationTimer = 0f;
        }

    }

    void RestoreDefaultRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, 0.2f);
    }
}
