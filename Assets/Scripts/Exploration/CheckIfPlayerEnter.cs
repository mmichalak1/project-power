using UnityEngine;
using Assets.LogicSystem;

public class CheckIfPlayerEnter : MonoBehaviour {
    public GameObject Player;
    public GameObject BattleUI;
	public GameObject ExplorationUI;

    private Quaternion newRot;
    private float rotationSpeed = 180.0f;
    private bool isDetecting = true;
    private bool isFaceToFace = true;
    private float time;
    private float timer = 0;


    void Start()
    {

    }

    void Update()
    {
        if(!isFaceToFace)
        {
            Player.transform.rotation = Quaternion.RotateTowards(Player.transform.rotation, newRot, Time.deltaTime * rotationSpeed);
            timer += Time.deltaTime;
            if (timer > time)
            {
                isFaceToFace = true;
                timer = 0;
                ToTheBattle();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Call");
        if (other.gameObject == Player && isDetecting)
        {
            if (Round(Player.transform.rotation.eulerAngles.y) == Round(transform.rotation.eulerAngles.y))
            {
                ToTheBattle();
                isFaceToFace = true;
            }
            else
            {
                Events.Instance.DispatchEvent("DisableSwipe", null);
                isFaceToFace = false;
                float rot = transform.rotation.eulerAngles.y - Player.transform.rotation.eulerAngles.y;
                time = Mathf.Abs((rot * 1.5f) / 180);
                newRot = Player.transform.rotation * Quaternion.AngleAxis(rot, Vector3.up);
                Player.GetComponent<MovementController>().newRot = newRot;
            }
        }

    }

    void ToTheBattle()
    {
        isDetecting = !isDetecting;
        Events.Instance.DispatchEvent("EnterFight", gameObject.GetComponent<EnemyGroup>());
        SystemAccessor.GetSystem<TurnManagerInteface>().BeginFight(gameObject.GetComponent<EnemyGroup>());
        BattleUI.SetActive(true);
        ExplorationUI.SetActive(false);
        Events.Instance.DispatchEvent("SetExplorationUI", ExplorationUI);
        Events.Instance.DispatchEvent("SetBattleUI", BattleUI);
    }

    private int Round(float number)
    {
        if (80 < number && number < 100)
            return 90;
        if (170 < number && number < 190)
            return 180;
        if (260 < number && number < 280)
            return 270;
        else
            return 0;
    }
}
