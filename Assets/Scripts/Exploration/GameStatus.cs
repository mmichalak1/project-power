using UnityEngine;
using System.Collections;

public class GameStatus : MonoBehaviour {

    public WoolCounter WoolCounter;
    public SheepData[] Sheep;
	// Use this for initialization
	void Start () {
        var scripts = GameObject.FindGameObjectsWithTag("GameStatus");
        if (scripts.Length>1)
        {
            for (int i = 1; i < scripts.Length; i++)
            {
                Destroy(scripts[i]);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
	
}
