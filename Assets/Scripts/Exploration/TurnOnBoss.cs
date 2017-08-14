using UnityEngine;

public class TurnOnBoss : MonoBehaviour {

    public GameObject BossPrefab;
    public ExplorationHolder Exploration;
    public GameState GameState;

	// Use this for initialization
	void Start () {
        if (Exploration.EnableBoss)
        {
            var res = Instantiate(BossPrefab);
            var checkForPlayer = res.GetComponent<CheckIfPlayerEnter>();
            var mycheckForPlayer = gameObject.GetComponent<CheckIfPlayerEnter>();
            checkForPlayer.Player = mycheckForPlayer.Player;
            checkForPlayer.BattleUI = mycheckForPlayer.BattleUI;
            checkForPlayer.ExplorationUI = mycheckForPlayer.ExplorationUI;
            gameObject.SetActive(false);
        }
	}

}
