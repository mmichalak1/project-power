using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ChestScript : MonoBehaviour {

    public GameObject Player;
    public GameObject WoolWindow;
    public SwipeManager Manager;

    public int WoolForFight = 20;

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            GetComponent<Collider>().enabled = false;
            WoolWindow.SetActive(true);
            WoolWindow.GetComponent<AfterBattleScreen>().OnEvoke(WoolForFight);
            Manager.enabled = false;
        }
    }
}
