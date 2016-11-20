using UnityEngine;

public class ResourcesUpdater : MonoBehaviour
{

    public UnityEngine.UI.Text text;
    [SerializeField]
    private WoolCounter DefaultWoolCounter;
    // Use this for initialization
    void Start()
    {
        UpdateResourcesView();
    }

    public void UpdateResourcesView()
    {
        text.text = "Resources: " + DefaultWoolCounter.Resources;
    }
}
