using UnityEngine;
using Assets.LogicSystem;

public class CheckIfPlayerEnter : MonoBehaviour {
    public GameObject Player;
    public GameObject BattleUI;

    private bool isDetecting = true;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Call");
        if (other.gameObject == Player && isDetecting)
        {
            isDetecting = !isDetecting;
            Events.Instance.DispatchEvent("EnterFight", null);
            BattleUI.SetActive(true);
        }

    }
}
