using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.LogicSystem;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField]
    WoolCounter Counter;

    private int _enemiesLeft;
    public int WoolForFight = 100;
    public List<GameObject> enemies = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        
        foreach (Transform child in transform)
            if (child.CompareTag("Enemy"))
                enemies.Add(child.gameObject);
        _enemiesLeft = enemies.Count;
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
        if (x.transform.parent != gameObject.transform)
            return;
        _enemiesLeft--;
        x.GetComponent<ProvideExperience>().ProvideExp();
        if (_enemiesLeft == 0)
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
            result |= x.GetComponent<EntityStatus>().Alive;
        }
        return !result;
    }
}
