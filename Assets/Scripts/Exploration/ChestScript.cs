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
        if (ChestData == default(ChestData) || diffrence.TotalHours < 24)
        {
            TimerText.enabled = IsCountingDown = true;
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            TimerText.enabled = IsCountingDown = false;
        }
    }

    void FixedUpdate()
    {
        if (!IsCountingDown)
            return;
        var span = TimeSpan.FromHours(24.0) - (DateTime.UtcNow - ChestData.LastOpened);
        TimerText.text = TimeSpanToString(span);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Player)
            return;
        GetComponent<Collider>().enabled = false;
        WoolWindow.SetActive(true);
        WoolWindow.GetComponent<AfterBattleScreen>().OnEvoke(ChestData.WoolForChest);
        ChestData.LastOpened = DateTime.UtcNow;
        Manager.enabled = false;
        IsCountingDown = TimerText.enabled = true;
        GameSaver.SaveGame();
    }

    private static string TimeSpanToString(TimeSpan span)
    {
        return span.Hours.ToString("00") + ":" + 
            span.Minutes.ToString("00") + ":" +
            span.Seconds.ToString("00");
    }
}
