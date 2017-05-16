using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node Up;
    public Node Down;
    public Node Left;
    public Node Right;

    public Vector3 Position;

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
