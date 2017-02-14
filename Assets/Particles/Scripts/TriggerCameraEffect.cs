using UnityEngine;
using System.Collections;

public class TriggerCameraEffect : MonoBehaviour {

    public CameraShake cameraShake;

    public float shakeAmount;
    public float shakeDuration;

	// Use this for initialization
	void Start () {
        cameraShake = GameObject.Find("CameraExploration").GetComponent<CameraShake>();
        cameraShake.ShakeCamera(shakeAmount, shakeDuration);
	}
	
}
