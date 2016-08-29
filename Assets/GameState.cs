using UnityEngine;
using System.Collections.Generic;
using Assets.LogicSystem;

public class GameState : MonoBehaviour {

    [SerializeField]
    private List<GameObject> EnemyGroups;
    [SerializeField]
    private GameObject Panel;

	// Use this for initialization
	void Start () {
        Events.Instance.RegisterForEvent("BattleWon", x =>
         {
             EnemyGroups.Remove(x as GameObject);
             if (EnemyGroups.Count == 0)
                 ExplorationWon();
         });
	}
	
    void ExplorationWon()
    {
        Panel.SetActive(true);
    }
}
