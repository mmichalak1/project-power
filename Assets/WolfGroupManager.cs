using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WolfGroupManager : MonoBehaviour
{

    int wolvesCounter;

    // Use this for initialization
    void Start()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        wolvesCounter = enemies.Length;
        foreach (var item in enemies)
        {
            Assets.LogicSystem.Events.Instance.RegisterForEvent(item.name, x => OnWolfDeath());
        }
    }



    public void OnWolfDeath()
    {
        wolvesCounter--;
        if (wolvesCounter == 0)
        {
            Assets.LogicSystem.Events.Instance.DispatchEvent("GameWon", null);
            Debug.Log("Battle won");
        }
    }
}
