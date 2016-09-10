using UnityEngine;
using System.Collections;

public class GameStatus : MonoBehaviour {

    public WoolCounter WoolCounter;
    public SheepData[] Sheep;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
    }
	
}
