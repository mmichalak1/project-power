using UnityEngine;
using Assets.LogicSystem;
using Assets.Scripts.Interfaces;

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
                ToTheBattle();
                isFaceToFace = true;
        }

    }

    void ToTheBattle()
    {
        isDetecting = !isDetecting;
        Events.Instance.DispatchEvent("EnterFight", gameObject.GetComponent<EnemyGroup>());
        SystemAccessor.GetSystem<ITurnManager>().BeginFight(gameObject.GetComponent<EnemyGroup>());
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
