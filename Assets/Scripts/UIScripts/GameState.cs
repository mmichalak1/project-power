#pragma warning disable 0649
using UnityEngine;
using System.Collections.Generic;
using Assets.LogicSystem;

public class GameState : MonoBehaviour {

    [SerializeField]
    private ExplorationHolder holder;
    [SerializeField]
    private List<GameObject> _enemyGroups;
    [SerializeField]
    private GameObject WinWindow;
    [SerializeField]
    private int WoolForWin = 100;

    public GameObject Map;
    
    public List<GameObject> EnemyGroups
    {
        get { return _enemyGroups; }
        set { _enemyGroups = value; }
    }

	// Use this for initialization
	void Start () {
        // ExplorationResult.Reset();
        Events.Instance.RegisterForEvent("EnemyGroupDestroyed", OnBattleWon);
         
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

    private void OnBattleWon(object x)
    {
        _enemyGroups.Remove(x as GameObject);
        Map.SetActive(true);
        if (_enemyGroups.Count == 0)
            ExplorationWon();
    }
}
