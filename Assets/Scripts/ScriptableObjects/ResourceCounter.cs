using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Game/ResourceCounter")]
public class ResourceCounter : ScriptableObject {

    [SerializeField]
    private int _resources = 7;
    [SerializeField]
    private int _basicResources = 7;

    [SerializeField]
    private int _maxResources = 10;

    //Maximum possible resources
    public int MaxResources
    {
        get { return _maxResources; }
    }

    //Actual unlocked resources
    public int Resources
    {
        get { return _resources; }
        set
        {
            _resources = value;
        }
    }

    //Minimal count of resources - DO NOT USE for game logic
    public int BasicResources
    {
        get { return _basicResources; }
        set
        {
            _basicResources = value;
        }
    }

}
