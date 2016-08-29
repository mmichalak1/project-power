using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WolfGroupManager : MonoBehaviour
{
    GameObject ExplorationUI;
    GameObject BattleUI;
    int wolvesCounter;

    List<Transform> enemies = new List<Transform>();

    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
            if (child.CompareTag("Enemy"))
                enemies.Add(child);
        wolvesCounter = enemies.Count;
        foreach (var item in enemies)
        {
            Assets.LogicSystem.Events.Instance.RegisterForEvent(item.name + "death", x => OnWolfDeath(x as GameObject));
        }
        Assets.LogicSystem.Events.Instance.RegisterForEvent("SetExplorationUI", x =>
        {
            ExplorationUI = x as GameObject;
        });
        Assets.LogicSystem.Events.Instance.RegisterForEvent("SetBattleUI", x =>
        {
            BattleUI = x as GameObject;
        });
    }

    public void ApplyGroupTurn()
    {
        foreach (var attack in GetComponentsInChildren<AttackController>())
            attack.PerformAction();
    }

    public void OnWolfDeath(GameObject x)
    {
        if (x.transform.parent.name != gameObject.name)
            return;
        wolvesCounter--;
        if (wolvesCounter == 0)
        {
            Assets.LogicSystem.Events.Instance.DispatchEvent("BattleWon", gameObject);
            BattleUI.SetActive(false);
            ExplorationUI.SetActive(true);
            Debug.Log("Battle won");
        }
    }
}
