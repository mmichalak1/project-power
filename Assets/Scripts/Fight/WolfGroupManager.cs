using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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

    private Assets.LogicSystem.Events.MyEvent wolfDeath;

    // Use this for initialization
    void Start()
    {
        wolfDeath = x => { OnWolfDeath(x as GameObject); };
        foreach (Transform child in transform)
            if (child.CompareTag("Enemy"))
                enemies.Add(child.gameObject);
        wolvesCounter = enemies.Count;
        foreach (var enemy in enemies)
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
    public void CheckForDeadAndApplyExperience()
    {
        foreach (var item in enemies)
        {
            if(!item.activeInHierarchy)
                item.GetComponent<ProvideExperience>().ProvideExp();
        }
    }

}
