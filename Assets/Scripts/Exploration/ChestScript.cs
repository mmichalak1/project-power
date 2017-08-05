using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{

    public GameObject Player;
    public GameObject WoolWindow;
    public SwipeManager Manager;
    public ChestData ChestData;
    public GameSaverScript GameSaver;

    public ExplorationHolder ExplorationHolder;
    public Text TimerText;

    public bool IsCountingDown = false;

    void Start()
    {
        if (ExplorationHolder.LevelPlayed == null)
        {
            GetComponent<Collider>().enabled = false;
            Debug.Log("Current level played is not set, can't setup chest");
            return;
        }
        ChestData = ExplorationHolder.LevelPlayed.Chests.First(x => x.name == gameObject.name);
        var now = DateTime.UtcNow;
        var diffrence = now - ChestData.LastOpened;
        if (ChestData == default(ChestData) || diffrence < ChestData.Duration.ToTimeSpan())
        {
            Disable();
        }
        else
        {
            Enable();
        }
    }

    void FixedUpdate()
    {
        if (!IsCountingDown)
            return;

        if (ChestData.Duration.ToTimeSpan() < DateTime.UtcNow - ChestData.LastOpened)
        {
            Enable();
            return;
        }

        var span = ChestData.Duration.ToTimeSpan() - (DateTime.UtcNow - ChestData.LastOpened);
        TimerText.text = TimeSpanToString(span);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Player)
            return;
        WoolWindow.SetActive(true);
        WoolWindow.GetComponent<AfterBattleScreen>().Show(ChestData.WoolForChest);
        ChestData.LastOpened = DateTime.UtcNow;
        Manager.enabled = false;
        Disable();
        GameSaver.SaveGame();
    }

    private void Disable()
    {
        TimerText.enabled = IsCountingDown = true;
        GetComponent<Collider>().enabled = false;
    }

    private void Enable()
    {
        TimerText.enabled = IsCountingDown = false;
        GetComponent<Collider>().enabled = true;
    }


    private static string TimeSpanToString(TimeSpan span)
    {
        return span.Hours.ToString("00") + ":" + 
            span.Minutes.ToString("00") + ":" +
            span.Seconds.ToString("00");
    }
}
