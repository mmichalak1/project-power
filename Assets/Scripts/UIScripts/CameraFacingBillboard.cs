using UnityEngine;
using System.Collections;
using System.Linq;

public class CameraFacingBillboard : MonoBehaviour 
{
    private Camera targetCamera;

    void Start()
    {
        targetCamera = Camera.main;
    }
 
    void Update()
    {
        transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward,
            targetCamera.transform.rotation * Vector3.up);
    }
}
