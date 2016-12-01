using System;
using UnityEngine;
using Assets.LogicSystem;

public class CameraManager : MonoBehaviour
{
    private float FOV;
    public GameObject cameraExploration;
    public GameObject cameraFight;

    private Vector3 positionCamera;
    private Vector3 positionCameraFight;

    private Quaternion rotationCamera;
    private Quaternion rotationCameraFight;

    private Vector3 explorationPosition;
    private Quaternion explorationRotation;

    private float sourceSize;
    private float targetSize;

    private float timeCounter = 0;
    private bool isChanging = false;

    private Events.MyEvent OnEnterTheFight, OnExitTheFight;


    // Use this for initialization
    void Start () {
        FOV = Camera.main.fieldOfView;
        targetSize = cameraFight.GetComponent<Camera>().orthographicSize;
        OnEnterTheFight = EnterFight;
        OnExitTheFight = ExitFight;
        Events.Instance.RegisterForEvent("EnterFight", OnEnterTheFight);
        Events.Instance.RegisterForEvent("BattleWon", OnExitTheFight);
    }
	
   


	// Update is called once per frame
	void Update () {
        
        if (isChanging)
        {
            timeCounter += Time.deltaTime;
            cameraExploration.transform.position = Vector3.Lerp(cameraExploration.transform.position, positionCameraFight, Time.deltaTime);
            cameraExploration.transform.rotation = Quaternion.Lerp(cameraExploration.transform.rotation, rotationCameraFight, Time.deltaTime);
            if(timeCounter > 4)
            {
                var camera = cameraExploration.GetComponent<Camera>();
                if(!camera.orthographic)
                    camera.orthographic = true;
                camera.orthographicSize = Mathf.Lerp(sourceSize, targetSize, 1.0f);

            }
            if (timeCounter > 7)
            {
                isChanging = false;
				TurnManager.ourTurn = true;
                TurnManager.UpdateResource(0);
                Events.Instance.DispatchEvent("ShowHealthBar", null);
                Events.Instance.DispatchEvent("ShowChangeTurnButton", null);
                timeCounter = 0f;
            }
        }

    }

    private void EnterFight(object obj)
    {
        isChanging = true;
        explorationPosition = cameraExploration.transform.position;
        explorationRotation = cameraExploration.transform.rotation;
        positionCamera = new Vector3(cameraExploration.transform.position.x, cameraExploration.transform.position.y, cameraExploration.transform.position.z);
        positionCameraFight = new Vector3(cameraFight.transform.position.x, cameraFight.transform.position.y, cameraFight.transform.position.z);
        rotationCamera = new Quaternion(cameraExploration.transform.rotation.x, cameraExploration.transform.rotation.y, cameraExploration.transform.rotation.z, cameraExploration.transform.rotation.w);
        rotationCameraFight = new Quaternion(cameraFight.transform.rotation.x, cameraFight.transform.rotation.y, cameraFight.transform.rotation.z, cameraFight.transform.rotation.w);
        sourceSize = cameraExploration.GetComponent<Camera>().orthographicSize;
    }
    private void ExitFight(object obj)
    {
        cameraExploration.transform.position = explorationPosition;
        cameraExploration.transform.rotation = explorationRotation;
        cameraExploration.GetComponent<Camera>().orthographicSize = sourceSize;
        cameraExploration.GetComponent<Camera>().orthographic = false;
        Camera.main.fieldOfView = FOV;
    }
}
