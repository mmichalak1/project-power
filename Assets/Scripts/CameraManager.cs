using UnityEngine;
using System.Collections;

using Assets.LogicSystem;

public class CameraManager : MonoBehaviour
{

    public GameObject cameraExploration;
    public GameObject cameraFight;

    private Vector3 positionCamera;
    private Vector3 positionCameraFight;

    private Quaternion rotationCamera;
    private Quaternion rotationCameraFight;

    private float timeCounter = 0;
    private bool flag = false;

    // Use this for initialization
    void Start () {
        positionCamera = new Vector3(cameraExploration.transform.position.x, cameraExploration.transform.position.y, cameraExploration.transform.position.z);
        positionCameraFight = new Vector3(cameraFight.transform.position.x, cameraFight.transform.position.y, cameraFight.transform.position.z);
        rotationCamera = new Quaternion(cameraExploration.transform.rotation.x, cameraExploration.transform.rotation.y, cameraExploration.transform.rotation.z, cameraExploration.transform.rotation.w);
        rotationCameraFight = new Quaternion(cameraFight.transform.rotation.x, cameraFight.transform.rotation.y, cameraFight.transform.rotation.z, cameraFight.transform.rotation.w);
    }
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime;
        if (timeCounter > 5 && flag == false)
        {
            cameraExploration.transform.position = Vector3.Lerp(cameraExploration.transform.position, positionCameraFight, Time.deltaTime);
            cameraExploration.transform.rotation = Quaternion.Lerp(cameraExploration.transform.rotation, rotationCameraFight, Time.deltaTime);
            if (timeCounter > 9)
            {
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 0, Time.deltaTime);
            }

            if (timeCounter > 10)
            {
                flag = true;
            }
            //if (flag == true)
            //{
            //    cameraExploration.SetActive(false);
            //    cameraFight.SetActive(true);
            //}
        }

    }
}
