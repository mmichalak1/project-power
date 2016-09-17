using UnityEngine;

public class WoolUpdater : MonoBehaviour
{

    public UnityEngine.UI.Text text;
    [SerializeField]
    private WoolCounter DefaultWoolCounter;
    // Use this for initialization
    void Start()
    {
        UpdateWoolView();
    }

    public void UpdateWoolView()
    {
        var go = GameObject.FindGameObjectWithTag("GameStatus");
        if (go != null)
            text.text = "Wool: " + go.GetComponent<GameStatus>().WoolCounter.WoolCount;
        else
            text.text = "Wool: " + DefaultWoolCounter.WoolCount;
    }
}
