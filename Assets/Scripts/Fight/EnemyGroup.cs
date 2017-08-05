using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.LogicSystem;

public class EnemyGroup : MonoBehaviour
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
            Events.Instance.RegisterForEvent(enemy.name + "death", OnEnemyDeath);
        }
    }

    public void ApplyGroupTurn()
    {
        foreach (var attack in GetComponentsInChildren<AttackController>())
        {
            attack.PerformAction();
        }
    }

    public void OnEnemyDeath(object wolf)
    {
        GameObject x = (GameObject)wolf;
        if (x.transform.parent.name != gameObject.name)
            return;
        wolvesCounter--;
        x.GetComponent<ProvideExperience>().ProvideExp();
        if (wolvesCounter == 0)
        {
            Debug.Log("Changing flag"); 
            foreach (var item in enemies)
            {
                Events.Instance.UnregisterForEvent(item.name + "death", OnEnemyDeath);
            }
            Events.Instance.DispatchEvent("EnemyGroupDestroyed", gameObject);
            gameObject.GetComponent<Collider>().enabled = false;
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

    public bool IsGroupFinished()
    {
        bool result = false;
        foreach (var x in enemies)
        {
            result |= x.GetComponent<HealthController>().IsAlive;
        }
        return !result;
    }
}
