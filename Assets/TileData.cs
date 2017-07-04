using UnityEngine;
using System.Collections.Generic;

public class TileData : MonoBehaviour {

    public BlockDataHolder ParentBlock;
    public TileData RightNeighbour, LeftNeighbour, UpNeighbour, DownNeighbour;
}
