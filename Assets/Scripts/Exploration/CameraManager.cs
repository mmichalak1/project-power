using System;
using UnityEngine;
using Assets.LogicSystem;

[RequireComponent(typeof(MatrixBlender))]
public class CameraManager : MonoBehaviour
{
    public float TransitionDuration = 2f;
    public float TimeToChangeCamera = 4f;
    public float TimeToStopTransition = 6.5f;
    public float LerpSpeed = 1.0f;
    public GameObject cameraExploration;
    public GameObject cameraFight;

    private Vector3 positionCamera;
    private Vector3 positionCameraFight;

    private Quaternion rotationCamera;
    private Quaternion rotationCameraFight;

    private Vector3 explorationPosition;
    private Quaternion explorationRotation;

    private Matrix4x4 startingMatrix;

    private float timeCounter = 0;
    private bool isChanging = false;

    private Events.MyEvent OnEnterTheFight, OnExitTheFight;

    MatrixBlender blender;


    // Use this for initialization
    void Start()
    {
        blender = GetComponent<MatrixBlender>();
        startingMatrix = cameraExploration.GetComponent<Camera>().projectionMatrix;
        OnEnterTheFight = EnterFight;
        OnExitTheFight = ExitFight;
        Events.Instance.RegisterForEvent("EnterFight", OnEnterTheFight);
        Events.Instance.RegisterForEvent("BattleWon", OnExitTheFight);
    }




    // Update is called once per frame
    void Update()
    {

        if (isChanging)
        {
            timeCounter += Time.deltaTime;
            cameraExploration.transform.position = Vector3.Lerp(cameraExploration.transform.position, positionCameraFight, LerpSpeed);
            cameraExploration.transform.rotation = Quaternion.Lerp(cameraExploration.transform.rotation, rotationCameraFight, LerpSpeed);
            if (timeCounter > TimeToChangeCamera && !blender.Working)
            {
                blender.BlendToMatrix(cameraFight.GetComponent<Camera>().projectionMatrix, TransitionDuration);
            }
            if (timeCounter > TimeToStopTransition)
            {
                isChanging = false;
                TurnManager.ourTurn = true;
                TurnManager.UpdateResource(0);
                Events.Instance.DispatchEvent("ShowHealthBar", null);
                Events.Instance.DispatchEvent("ShowChangeTurnButton", null);
                Events.Instance.DispatchEvent("ShowResourcesDisplay", null);
                Events.Instance.DispatchEvent("ShowSurrenderButton", null);
                timeCounter = 0f;
            }
        }

    }

    private void EnterFight(object obj)
    {
        isChanging = true;
        explorationPosition = cameraExploration.transform.position;
        explorationRotation = cameraExploration.transform.rotation;
        positionCamera = cameraExploration.transform.position;
        positionCameraFight = cameraFight.transform.position;
        rotationCamera = cameraExploration.transform.rotation;
        rotationCameraFight = cameraFight.transform.rotation;
    }
    private void ExitFight(object obj)
    {
        cameraExploration.transform.position = explorationPosition;
        cameraExploration.transform.rotation = explorationRotation;
        cameraExploration.GetComponent<Camera>().projectionMatrix = startingMatrix;
    }
}
