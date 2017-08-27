using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Chest", menuName = "Game/Chest")]
public class ChestData : ScriptableObject {

    [SerializeField]
    private int _woolForChest;

    public int WoolForChest
    {
        get { return _woolForChest; }
        set { _woolForChest = value; }
    }

}
