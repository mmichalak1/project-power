using System.Collections.Generic;
using UnityEngine;

public class Node : Tile
{
    public Node Up;
    public Node Down;
    public Node Left;
    public Node Right;


    public bool CanGetNextDirection()
    {
        return Up == null || Down == null || Left == null || Right == null;
    }

    public int GetNextDirection()
    {
        int direction;
        while(true)
        {
            direction = Random.Range(0, 4);
            if (IsOk(direction))
                return direction;
        }
    }
    public void RemoveNode(Node node)
    {
        if (Up == node)
            Up = null;
        if (Down == node)
            Down = null;
        if (Left == node)
            Left = null;
        if (Right == node)
            Right = null;
    }
    private bool IsOk(int dir)
    {
        if (dir == 0)
            return Up == null;
        else if (dir == 1)
            return Left == null;
        else if (dir == 2)
            return Right == null;
        else
            return Down == null;
            
        

    }
}
