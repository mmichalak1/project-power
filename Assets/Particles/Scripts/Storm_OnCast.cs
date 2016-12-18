using UnityEngine;
using System.Collections;
using System;

public class Storm_OnCast : MonoBehaviour {

    public Light AmbientLight;
    public float Ratio;

    void Start()
    {
        AmbientLight = GameObject.FindGameObjectWithTag("AmbientLight").GetComponent<Light>();
    }

	void Update () {
        Darkening();
	}

    void Darkening()
    {
        if (AmbientLight.intensity >= 0.1f)
        {
            AmbientLight.intensity -= Time.deltaTime * Ratio;
        }
    }
}
