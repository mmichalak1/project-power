using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Chest", menuName = "Game/Chest")]
public class ChestData : ScriptableObject {

    [SerializeField]
    private DateTime _lastOpened;
    [SerializeField]
    private int _woolForChest;
    [SerializeField]
    private Duration _duration;


    public Duration Duration
    {
        get { return _duration; }
        set { _duration = value; }
    }
    public DateTime LastOpened
    {
        get { return _lastOpened; }
        set { _lastOpened = value; }
    }

    public int WoolForChest
    {
        get { return _woolForChest; }
        set { _woolForChest = value; }
    }

}
