using UnityEngine;
using UnityEngine.UI;
using Assets.LogicSystem;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using System;

public class TurnManager : MonoBehaviour {

    GameObject[] enemies;
    bool ourTurn;
    public Button turnButton;
    [SerializeField]
    private GameObject selectedSheep;

	// Use this for initialization
	void Start () {
        ourTurn = true;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var item in GameObject.FindGameObjectsWithTag("Sheep"))
        {
            Events.Instance.RegisterForEvent(item.name, x =>
            {
                selectedSheep = ((KeyValuePair<Vector2, Transform>)x).Value.gameObject;
            });
        }
        foreach (var item in enemies)
        {
            Events.Instance.RegisterForEvent(item.name, x =>
            {
                if (selectedSheep != null)
                {
                    TurnPlaner.Instance.AddPlan(selectedSheep.name, new Plan(selectedSheep, ((KeyValuePair<Vector2, Transform>)x).Value.gameObject, (act, tar) =>
                    {
                        tar.GetComponent<IReciveDamage>().DealDamage(10);
                        Debug.Log(act + " dealt 10dmg to " + tar);
                    }));
                }
            });
        }
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void ChangeTurn()
    {
        if (!TurnPlaner.Instance.Execute())
            return;

        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<AttackController>().PerformAction();
        }
    }


}
