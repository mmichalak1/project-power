﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class CameraFacingBillboard : MonoBehaviour 
{
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }
 
    void Update()
    {
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward,
            camera.transform.rotation * Vector3.up);
    }
}