﻿using UnityEngine;
using System.Collections.Generic;

public class BlockDataHolder : MonoBehaviour {


    [SerializeField]
    private List<GameObject> passableTiles;
    [SerializeField]
    private List<GameObject> connectingTiles;
    [SerializeField]
    private List<BlockDataHolder> neighbouringBlocks;
    [SerializeField]
    private GameObject startingTile;
    
    public List<GameObject> PassableTiles { get { return passableTiles; } }
    public List<GameObject> ConnectingTiles { get { return connectingTiles; } }
    public List<BlockDataHolder> NeighbouringBlocks { get { return neighbouringBlocks; } }
    public GameObject StartingTile { get { return startingTile; } set { startingTile = value; } }
}