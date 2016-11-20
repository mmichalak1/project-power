using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Game/WoolCounter")]
public class WoolCounter : ScriptableObject {

    [SerializeField]
    private int _woolCount = 0;
    [SerializeField]
    private int _resources = 7;
    [SerializeField]
    private int _basicResources = 7;

    public int WoolCount
    {
        get { return _woolCount; }
        set
        {
            _woolCount = value;
            GameObject.FindGameObjectWithTag("WoolCounter").GetComponent<WoolUpdater>().UpdateWoolView();
        }
    }
    public int Resources
    {
        get { return _resources; }
        set
        {
            _resources = value;
        }
    }
    public int BasicResources
    {
        get { return _basicResources; }
        set
        {
            _basicResources = value;
        }
    }

}
