using UnityEngine;
using System.Collections;

public class GameStatus : MonoBehaviour {

    public WoolCounter WoolCounter;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
    }
	
}
