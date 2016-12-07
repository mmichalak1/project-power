using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.LogicSystem;

public class WolfGroupManager : MonoBehaviour
{
    [SerializeField]
    WoolCounter Counter;

    int wolvesCounter;


    public int WoolForFight = 100;
    [HideInInspector]
    public List<GameObject> enemies = new List<GameObject>();

    // Use this for initialization
    void Start()
    {

        foreach (Transform child in transform)
            if (child.CompareTag("Enemy"))
                enemies.Add(child.gameObject);
        wolvesCounter = enemies.Count;
        foreach (var enemy in enemies)
        {
            Events.Instance.RegisterForEvent(enemy.name + "death", OnWolfDeath);
        }
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
            foreach (var item in enemies)
            {
                Events.Instance.UnregisterForEvent(item.name + "death", OnWolfDeath);
            }
            Events.Instance.DispatchEvent("EnemyGroupDestroyed", gameObject);
            Destroy(gameObject);
            TurnManager.BattleWon = true;
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
