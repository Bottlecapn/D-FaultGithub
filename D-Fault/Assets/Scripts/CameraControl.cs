using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    GameObject pivot;
    // Start is called before the first frame update
    void Start()
    {
        pivot = GameObject.Find("Camera Pivot");
        gameObject.transform.parent = pivot.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(pivot.transform);
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            pivot.transform.Rotate(new Vector3(0,-90,0));
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            pivot.transform.Rotate(new Vector3(0, 90, 0));
        }*/
    }
}
