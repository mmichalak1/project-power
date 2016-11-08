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
            text.text = "Wool: " + DefaultWoolCounter.WoolCount;
    }
}
