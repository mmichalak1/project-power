using System;
using UnityEngine;
using Assets.LogicSystem;

[RequireComponent(typeof(MatrixBlender))]
public class CameraManager : MonoBehaviour
{
    #region AnimatorTriggers
    private const string ENTERBATTLE = "EnterBattle";
    private const string ENDBATTLE = "EndBattle";

    #endregion

    Animator myAnimator;

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

    private Matrix4x4 startingMatrix;

    private Events.MyEvent OnEnterTheFight, OnExitTheFight;

    MatrixBlender blender;


    // Use this for initialization
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        blender = GetComponent<MatrixBlender>();
        startingMatrix = cameraExploration.GetComponent<Camera>().projectionMatrix;
        OnEnterTheFight = EnterFight;
        OnExitTheFight = ExitFight;
        Events.Instance.RegisterForEvent("EnterFight", OnEnterTheFight);
        Events.Instance.RegisterForEvent("BattleWon", OnExitTheFight);
    }

    private void EnterFight(object obj)
    {
        //isChanging = true;
        myAnimator.SetTrigger(ENTERBATTLE);
    }
    private void ExitFight(object obj)
    {
        cameraExploration.GetComponent<Camera>().projectionMatrix = startingMatrix;
        myAnimator.SetTrigger(ENDBATTLE);
    }

    public void OnTransitionToBattleDone()
    {
        Events.Instance.DispatchEvent("activateBattleUI", null);
        Events.Instance.DispatchEvent("ShowHealthBar", null);
        Events.Instance.DispatchEvent("ShowChangeTurnButton", null);
        Events.Instance.DispatchEvent("ShowResourcesDisplay", null);
        Events.Instance.DispatchEvent("ShowSurrenderButton", null);
    }

    public void StartMatrixBlending()
    {
        blender.BlendToMatrix(cameraFight.GetComponent<Camera>().projectionMatrix, TransitionDuration);
    }
}
