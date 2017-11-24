#pragma warning disable 0649
using UnityEngine;
using System.Collections.Generic;
using Assets.LogicSystem;

public class GameState : MonoBehaviour {

    [SerializeField]
    private ExplorationHolder holder;
    [SerializeField]
    private GameObject WinWindow;

    public GameObject Map;
    public GameObject LastGroup;

	// Use this for initialization
	void Start () {
        Events.Instance.RegisterForEvent("EnemyGroupDestroyed", OnBattleWon);
	}
	
    void ExplorationWon()
    {
        holder.GameResult = GameResult.Win;
        WinWindow.SetActive(true);
        Map.SetActive(false);
    }

    private void OnBattleWon(object x)
    {
        GameObject group = x as GameObject;
        Map.SetActive(true);
        if (LastGroup == group)
            ExplorationWon();
    }
}
