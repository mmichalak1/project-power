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
    [SerializeField]
    private PlayerData playerData;

    public GameObject Map;
    
    public List<GameObject> EnemyGroups
    {
        get { return _enemyGroups; }
        set { _enemyGroups = value; }
    }

    public PlayerData PlayerData
    {
        get { return playerData; }
    }

	// Use this for initialization
	void Start () {
        // ExplorationResult.Reset();
        Events.Instance.RegisterForEvent("EnemyGroupDestroyed", OnBattleWon);
         
	}
	
    void ExplorationWon()
    {
        holder.GameResult = GameResult.Win;
        WinWindow.SetActive(true);
        Map.SetActive(false);
        foreach (var item in GameObject.FindGameObjectsWithTag("Sheep"))
        {
            EntityDataHolder.RevertItemsChange(item.GetComponent<EntityDataHolder>().SheepData);
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
