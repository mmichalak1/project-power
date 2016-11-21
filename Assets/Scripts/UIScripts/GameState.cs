#pragma warning disable 0649
using UnityEngine;
using System.Collections.Generic;
using Assets.LogicSystem;

public class GameState : MonoBehaviour {

    [SerializeField]
    private ExplorationHolder holder;
    [SerializeField]
    private List<GameObject> EnemyGroups;
    [SerializeField]
    private GameObject WinWindow;
    [SerializeField]
    private int WoolForWin = 100;

    public GameObject Map;

	// Use this for initialization
	void Start () {
       // ExplorationResult.Reset();
        Events.Instance.RegisterForEvent("BattleWon", x =>
         {
             EnemyGroups.Remove(x as GameObject);
             Map.SetActive(true);
             if (EnemyGroups.Count == 0)
                 ExplorationWon();
         });
	}
	
    void ExplorationWon()
    {
        holder.GameResult = Assets.Scripts.GameResult.Win;
        WinWindow.SetActive(true);
        Map.SetActive(false);
        foreach (var item in GameObject.FindGameObjectsWithTag("Sheep"))
        {
            item.GetComponent<EntityDataHolder>().RevertItemsChange();
        }
        
    }
}
