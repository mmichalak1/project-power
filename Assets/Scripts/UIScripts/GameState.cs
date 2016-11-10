#pragma warning disable 0649
using UnityEngine;
using System.Collections.Generic;
using Assets.LogicSystem;

public class GameState : MonoBehaviour {

    [SerializeField]
    private List<GameObject> EnemyGroups;
    [SerializeField]
    private GameObject Panel;
    [SerializeField]
    private int WoolForWin = 100;

	// Use this for initialization
	void Start () {
       // ExplorationResult.Reset();
        Events.Instance.RegisterForEvent("BattleWon", x =>
         {
             EnemyGroups.Remove(x as GameObject);
             if (EnemyGroups.Count == 0)
                 ExplorationWon();
         });
	}
	
    void ExplorationWon()
    {
        ExplorationResult.Instance.GameResult = Assets.Scripts.GameResult.Win;
        Panel.SetActive(true);
        foreach (var item in GameObject.FindGameObjectsWithTag("Sheep"))
        {
            item.GetComponent<EntityDataHolder>().RevertItemsChange();
        }
        
    }
}
