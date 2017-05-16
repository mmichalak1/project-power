using UnityEngine;
using System.Collections.Generic;


public class Path
{
    public Node source, target;
    public int Length { get { return tiles.Count; } }
    public List<Tile> tiles = new List<Tile>();
}
