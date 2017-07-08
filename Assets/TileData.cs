using UnityEngine;
using System.Collections.Generic;

public class TileData : MonoBehaviour {

    public BlockDataHolder ParentBlock;
    public TileData RightNeighbour, LeftNeighbour, UpNeighbour, DownNeighbour;

    public TileData GetTile(Facing face)
    {
        switch (face)
        {
            case Facing.Up:
                return UpNeighbour;
            case Facing.Right:
                return RightNeighbour;
            case Facing.Down:
                return DownNeighbour;
            case Facing.Left:
                return LeftNeighbour;
            default:
                Debug.LogError("Incorrent facing: " + face);
                return null;
        }
    }

}
