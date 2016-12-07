using UnityEngine;

public class ResourcesUpdater : MonoBehaviour
{

    public UnityEngine.UI.Text text;
    [SerializeField]
    private ResourceCounter DefaultResourceCounter;
    // Use this for initialization
    void Start()
    {
        UpdateResourcesView();
    }

    public void UpdateResourcesView()
    {
        text.text = "Resources: " + DefaultResourceCounter.Resources;
    }
}
