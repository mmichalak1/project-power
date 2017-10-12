using UnityEngine;
using Assets.LogicSystem;
using Assets.Scripts.Interfaces;

public class CheckIfPlayerEnter : MonoBehaviour {
    public GameObject Player;
    public GameObject BattleUI;
	public GameObject ExplorationUI;

    private bool isDetecting = true;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Call");
        if (other.gameObject == Player && isDetecting)
        {
                ToTheBattle();
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
