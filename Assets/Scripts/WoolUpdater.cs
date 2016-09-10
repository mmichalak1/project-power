using UnityEngine;

public class WoolUpdater : MonoBehaviour {

    public UnityEngine.UI.Text text;
	// Use this for initialization
	void Start () {
        UpdateWoolView();
	}

    public void UpdateWoolView()
    {
        text.text = "Wool: " + GameObject.FindGameObjectWithTag("GameStatus").GetComponent<GameStatus>().WoolCounter.WoolCount;
    }
}
