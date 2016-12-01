using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.LogicSystem;

public class WolfGroupManager : MonoBehaviour
{
    [SerializeField]
    WoolCounter Counter;

    GameObject ExplorationUI;
    GameObject BattleUI;
    int wolvesCounter;


    public int WoolForFight = 100;
    [HideInInspector]
    public List<GameObject> enemies = new List<GameObject>();

    private Events.MyEvent OnSetBattleUI, OnSetExplorationUI;

    // Use this for initialization
    void Start()
    {
        OnSetBattleUI = new Events.MyEvent(x =>
        {
            BattleUI = x as GameObject;
        });
        OnSetExplorationUI = new Events.MyEvent(x =>
        {
            ExplorationUI = x as GameObject;
        });

        foreach (Transform child in transform)
            if (child.CompareTag("Enemy"))
                enemies.Add(child.gameObject);
        wolvesCounter = enemies.Count;
        foreach (var enemy in enemies)
        {
            Events.Instance.RegisterForEvent(enemy.name + "death", OnWolfDeath);
        }
        Events.Instance.RegisterForEvent("SetExplorationUI", OnSetExplorationUI);
        Events.Instance.RegisterForEvent("SetBattleUI", OnSetBattleUI);
    }

    public void ApplyGroupTurn()
    {
        foreach (var attack in GetComponentsInChildren<AttackController>())
        {
            attack.PerformAction();
        }
    }

    public void OnWolfDeath(object wolf)
    {
        GameObject x = (GameObject)wolf;
        if (x.transform.parent.name != gameObject.name)
            return;
        wolvesCounter--;
        x.GetComponent<ProvideExperience>().ProvideExp();
        if (wolvesCounter == 0)
        {
            BattleUI.SetActive(false);
            ExplorationUI.SetActive(true);
            foreach (var item in enemies)
            {
                Assets.LogicSystem.Events.Instance.UnregisterForEvent(item.name + "death", OnWolfDeath);
            }
            Destroy(gameObject);
            //Counter.WoolCount += WoolForFight;
            Assets.LogicSystem.Events.Instance.DispatchEvent("AfterBattleScreen", WoolForFight);
            Assets.LogicSystem.Events.Instance.DispatchEvent("BattleWon", gameObject);
        }
    }

    public GameObject[] GetNeighbouringEnemies(GameObject enemy)
    {
        GameObject[] result = new GameObject[2];
        int enemyPosition = enemies.IndexOf(enemy);
        result[0] = enemies.ElementAtOrDefault(enemyPosition + 1);
        result[1] = enemies.ElementAtOrDefault(enemyPosition - 1);

        return result;

    }
}
