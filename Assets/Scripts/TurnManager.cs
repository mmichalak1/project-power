using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour {

    GameObject[] enemies;
    bool ourTurn;
    public Button turnButton;

	// Use this for initialization
	void Start () {
        ourTurn = true;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void ChangeTurn()
    {
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<AttackController>().PerformAction();
        }
    }
}
