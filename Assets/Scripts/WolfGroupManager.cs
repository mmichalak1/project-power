using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WolfGroupManager : MonoBehaviour
{
    GameObject ExplorationUI;
    GameObject BattleUI;
    int wolvesCounter;


    public int WoolForFight = 100;
    [HideInInspector]
    public List<Transform> enemies = new List<Transform>();

    private Assets.LogicSystem.Events.MyEvent wolfDeath;

    // Use this for initialization
    void Start()
    {
        wolfDeath = x => { OnWolfDeath(x as GameObject); };
        foreach (Transform child in transform)
            if (child.CompareTag("Enemy"))
                enemies.Add(child);
        wolvesCounter = enemies.Count;
        foreach(var enemy in enemies)
        {
            Assets.LogicSystem.Events.Instance.RegisterForEvent(enemy.name + "death", wolfDeath);
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
            BattleUI.SetActive(false);
            ExplorationUI.SetActive(true);
            foreach (var item in enemies)
            {
                Assets.LogicSystem.Events.Instance.UnregisterForEvent(item.name + "death", wolfDeath);
            }
            GameObject.FindGameObjectWithTag("GameStatus").GetComponent<GameStatus>().WoolCounter.WoolCount += WoolForFight;
            Destroy(gameObject);
            Assets.LogicSystem.Events.Instance.DispatchEvent("BattleWon", gameObject);
            //Debug.Log("Battle won");
        }
    }
}
