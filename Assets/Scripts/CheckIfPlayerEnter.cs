using UnityEngine;
using Assets.LogicSystem;

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
            isDetecting = !isDetecting;
            ExplorationResult.Instance.BattlesFought++;
            Events.Instance.DispatchEvent("EnterFight", gameObject.GetComponent<WolfGroupManager>());
            BattleUI.SetActive(true);
			ExplorationUI.SetActive (false);
			TurnManager.ourTurn = true;
            Events.Instance.DispatchEvent("SetExplorationUI", ExplorationUI);
            Events.Instance.DispatchEvent("SetBattleUI", BattleUI);
        }

    }
}
