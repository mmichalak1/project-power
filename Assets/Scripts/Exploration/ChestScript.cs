using System;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
    public int WoolForChest { get; set; }
    public GameObject Player;
    public GameObject WoolWindow;
    public SwipeManager Manager;
    public GameSaverScript GameSaver;
    public bool IsCountingDown = false;

    void Start()
    {
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Player)
            return;
        WoolWindow.SetActive(true);
        WoolWindow.GetComponent<AfterBattleScreen>().OnEvoke(WoolForChest);
        GameSaver.SaveGame();
    }
}
