using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    public MapDecorator Decorator;

    public int tileWidth = 1;

    public int BranchMinLength = 3;
    public int BranchMaxLength = 7;

    public int MinMainBranches = 5;
    public int MaxMainBranches = 7;

    public int MinAddidtionalBranches = 2;
    public int MaxAdditionalBranches = 4;

    public int LevelSeed;

    private int MainNodesCount;
    private int AdditionalNodesCount;

    private Node StartingNode, FinishNode;

    private List<Node> Nodes = new List<Node>();
    private List<Node> mainNodes = new List<Node>();
    private List<Node> additionalNodes = new List<Node>();

    public List<Node> MainNodes { get { return mainNodes; } }
    public List<Node> AdditionalNodes { get { return additionalNodes; } }

    private List<Path> MainPaths = new List<Path>();
    public List<Path> Paths { get { return MainPaths; } }
    // Use this for initialization
    void Start()
    {
        Random.Range(0, 1);
        if (LevelSeed != 0)
        {
            Random.seed = LevelSeed;
        }
        LevelSeed = Random.seed;

        StartingNode = new Node();
        StartingNode.Position = Decorator.StartingPoint;
        mainNodes.Add(StartingNode);
        Nodes.Add(StartingNode);

        MainNodesCount = Random.Range(MinMainBranches, MaxMainBranches);
        AdditionalNodesCount = Random.Range(MinAddidtionalBranches, MaxAdditionalBranches);
        BuildMainNodes();
        BuildAdditionalNodes();
        BuildPaths(StartingNode, null);

        CheckPaths();

        Decorator.Decorate();
    }


    private void BuildMainNodes()
    {
        int direction, length;
        Node lastNode = StartingNode;
        for (int i = 0; i < MainNodesCount; i++)
        {
            direction = Random.Range(0, 2);
            length = Random.Range(BranchMinLength, BranchMaxLength);
            Node newNode = new Node();
            newNode.Position = lastNode.Position;
            mainNodes.Add(newNode);
            if (i != MainNodesCount - 1)
                Nodes.Add(newNode);
            switch (direction)
            {
                case 0:
                    lastNode.Up = newNode;
                    newNode.Position.y += length * tileWidth;
                    newNode.Down = lastNode;
                    break;
                case 1:
                    lastNode.Left = newNode;
                    newNode.Position.x += length * tileWidth;
                    newNode.Right = lastNode;
                    break;
                case 2:
                    lastNode.Right = newNode;
                    newNode.Position.x -= length * tileWidth;
                    newNode.Left = lastNode;
                    break;
            }
            lastNode = newNode;

        }
        FinishNode = lastNode;

    }
    private void BuildAdditionalNodes()
    {
        int nextTargetDirection, length;
        Node targetNode, newNode;

        for (int i = 0; i < AdditionalNodesCount; i++)
        {
            //randomize next node
            do
            {
                targetNode = Nodes[Random.Range(0, Nodes.Count - 1)];
            }
            while (!targetNode.CanGetNextDirection());

            //get next node direction and path length
            nextTargetDirection = targetNode.GetNextDirection();
            length = Random.Range(BranchMinLength, BranchMaxLength);
            newNode = new Node();
            newNode.Position = targetNode.Position;
            switch (nextTargetDirection)
            {
                case 0:
                    targetNode.Up = newNode;
                    newNode.Position.y += length * tileWidth;
                    newNode.Down = targetNode;
                    break;
                case 1:
                    targetNode.Left = newNode;
                    newNode.Position.x += +length * tileWidth;
                    newNode.Right = targetNode;
                    break;
                case 2:
                    targetNode.Right = newNode;
                    newNode.Position.x -= length * tileWidth;
                    newNode.Left = targetNode;
                    break;
                case 3:
                    targetNode.Down = newNode;
                    newNode.Position.y -= length * tileWidth;
                    newNode.Up = targetNode;
                    break;
            }
            Nodes.Add(newNode);
            additionalNodes.Add(newNode);


        }

    }
    private void BuildPaths(Node target, Node source)
    {
        int distance;
        Path path;
        Tile tile;
        if (target.Up != null && target.Up != source)
        {
            path = new Path();
            path.source = target;
            path.target = target.Up;
            distance = (int)(target.Up.Position.y - target.Position.y);
            distance /= tileWidth;
            for (int i = 1; i < distance; i++)
            {
                tile = new Tile();
                tile.Position = new Vector2(target.Position.x, target.Position.y + tileWidth * i);
                path.tiles.Add(tile);
            }
            MainPaths.Add(path);
            BuildPaths(target.Up, target);
        }

        if (target.Down != null && target.Down != source)
        {
            path = new Path();
            path.source = target;
            path.target = target.Down;
            distance = (int)(target.Position.y - target.Down.Position.y);
            distance /= tileWidth;
            for (int i = 1; i < distance; i++)
            {
                tile = new Tile();
                tile.Position = new Vector2(target.Position.x, target.Position.y - tileWidth * i);
                path.tiles.Add(tile);
            }
            MainPaths.Add(path);
            BuildPaths(target.Down, target);
        }

        if (target.Left != null && target.Left != source)
        {
            path = new Path();
            path.source = target;
            path.target = target.Left;
            distance = (int)(target.Left.Position.x - target.Position.x);
            distance /= tileWidth;
            for (int i = 1; i < distance; i++)
            {
                tile = new Tile();
                tile.Position = new Vector2(target.Position.x + i * tileWidth, target.Position.y);
                path.tiles.Add(tile);
            }
            MainPaths.Add(path);
            BuildPaths(target.Left, target);
        }

        if (target.Right != null && target.Right != source)
        {
            path = new Path();
            path.source = target;
            path.target = target.Right;
            distance = (int)(target.Position.x - target.Right.Position.x);
            distance /= tileWidth;
            for (int i = 1; i < distance; i++)
            {
                tile = new Tile();
                tile.Position = new Vector2(target.Position.x - i * tileWidth, target.Position.y);
                path.tiles.Add(tile);
            }
            MainPaths.Add(path);
            BuildPaths(target.Right, target);
        }
    }

    private void CheckPaths()
    {
        bool restart = true;
        while (restart)
        {
            restart = false;
            foreach (Path p in MainPaths.Where(p => additionalNodes.Contains(p.source) || additionalNodes.Contains(p.target)))
            {
                int counter = 0;
                foreach (Path path in MainPaths)
                {
                    if (path != p)
                        counter += CountOverlappingTiles(p, path);
                }
                if (counter > 2)
                {
                    RemovePath(p);
                    restart = true;
                    break;
                }
            }
        }
    }

    private int CountOverlappingTiles(Path p1, Path p2)
    {
        int res = 0;

        if (p1.target.Position == p2.target.Position)
            res++;
        if (p1.target.Position == p2.source.Position)
            res++;
        if (p1.source.Position == p2.target.Position)
            res++;
        if (p1.source.Position == p2.source.Position)
            res++;

        foreach(Tile t in p1.tiles)
        {
            if (p2.tiles.Any(x => x.Position == t.Position))
                res++;
        }


        return res;
    }

    private void RemovePath(Path p)
    {
        p.source.RemoveNode(p.target);
        p.target.RemoveNode(p.source);
        additionalNodes.Remove(p.source);
        additionalNodes.Remove(p.target);
        MainPaths.Remove(p);


    }
}
