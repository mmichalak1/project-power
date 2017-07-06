using UnityEngine;
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
    [SerializeField]
    private GameObject nodeToMain;
    
    public List<GameObject> PassableTiles { get { return passableTiles; } }
    public List<GameObject> ConnectingTiles { get { return connectingTiles; } }
    public List<BlockDataHolder> NeighbouringBlocks { get { return neighbouringBlocks; } }
    public GameObject SpawnTile { get { return startingTile; } set { startingTile = value; } }
    public GameObject NodeToMain { get { return nodeToMain; } set { nodeToMain = value; } }
}
