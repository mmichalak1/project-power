using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WolfGroupManager : MonoBehaviour
{
    GameObject ExplorationUI;
    int wolvesCounter;

    // Use this for initialization
    void Start()
    {
        List<Transform> enemies = new List<Transform>();
        foreach (Transform child in transform)
            if (child.CompareTag("Enemy"))
                enemies.Add(child);
        wolvesCounter = enemies.Count;
        foreach (var item in enemies)
        {
            Assets.LogicSystem.Events.Instance.RegisterForEvent(item.name, x => OnWolfDeath());
        }
        Assets.LogicSystem.Events.Instance.RegisterForEvent("SetExplorationUI", x =>
        {
            ExplorationUI = x as GameObject;
        });
    }



    public void OnWolfDeath()
    {
        wolvesCounter--;
        if (wolvesCounter == 0)
        {
            Assets.LogicSystem.Events.Instance.DispatchEvent("BattleWon", null);
            GameObject.Find("BattleUI").SetActive(false);
            ExplorationUI.SetActive(true);
            Debug.Log("Battle won");
        }
    }
}
