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

    public int MaxResources
    {
        get { return _maxResources; }
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
