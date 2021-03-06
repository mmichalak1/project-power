﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    public MapDecorator Decorator;

    public int blockSize = 1;

    public int BranchMinLength = 3;
    public int BranchMaxLength = 7;

    public int MinMainBranches = 5;
    public int MaxMainBranches = 7;

    public int MinAddidtionalBranches = 2;
    public int MaxAdditionalBranches = 4;

    public int LevelSeed;

    private int MainNodesCount;
    private int AdditionalNodesCount;

    private Node startingNode, finishNode;

    private List<Node> allNodes = new List<Node>();
    private List<Node> mainNodes = new List<Node>();
    private List<Node> additionalNodes = new List<Node>();
    private List<Path> MainPaths = new List<Path>();


    public List<Node> AllNodes { get { return allNodes; } }
    public List<Node> MainNodes { get { return mainNodes; } }
    public List<Node> AdditionalNodes { get { return additionalNodes; } }
    public Node StartingNode { get { return startingNode; } }
    public Node FinishNode { get { return finishNode; } }
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

        startingNode = new Node();
        startingNode.Position = Decorator.StartingPoint;
        mainNodes.Add(startingNode);
        allNodes.Add(startingNode);

        MainNodesCount = Random.Range(MinMainBranches, MaxMainBranches + 1);
        AdditionalNodesCount = Random.Range(MinAddidtionalBranches, MaxAdditionalBranches + 1);
        BuildMainNodes();

        BuildAdditionalNodes();
        BuildPaths(startingNode, null);

        CheckPaths();
        //Debug.Log("Total nodes: " + (MainNodes.Count + AdditionalNodes.Count));
        Decorator.Decorate();


    }


    private void BuildMainNodes()
    {
        int direction, length;
        Node lastNode = startingNode;
        for (int i = 0; i < MainNodesCount; i++)
        {
            direction = Random.Range(0, 2);
            length = Random.Range(BranchMinLength, BranchMaxLength);
            Node newNode = new Node();
            newNode.Position = lastNode.Position;
            allNodes.Add(newNode);
            if (i != MainNodesCount - 1)
                mainNodes.Add(newNode);
            switch (direction)
            {
                case 0:
                    lastNode.Up = newNode;
                    newNode.Position.y += length * blockSize;
                    newNode.Down = lastNode;
                    break;
                case 1:
                    lastNode.Left = newNode;
                    newNode.Position.x += length * blockSize;
                    newNode.Right = lastNode;
                    break;
            }
            lastNode = newNode;

        }
        finishNode = lastNode;

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
                targetNode = mainNodes.GetRandomElement();
            }
            while (!targetNode.CanGetNextDirection() || targetNode == finishNode);

            //get next node direction and path length
            nextTargetDirection = targetNode.GetNextDirection();
            length = Random.Range(BranchMinLength, BranchMaxLength + 1);
            newNode = new Node();
            newNode.Position = targetNode.Position;
            switch (nextTargetDirection)
            {
                case 0:
                    targetNode.Up = newNode;
                    newNode.Position.y += length * blockSize;
                    newNode.Down = targetNode;
                    break;
                case 1:
                    targetNode.Left = newNode;
                    newNode.Position.x += +length * blockSize;
                    newNode.Right = targetNode;
                    break;
                case 2:
                    targetNode.Right = newNode;
                    newNode.Position.x -= length * blockSize;
                    newNode.Left = targetNode;
                    break;
                case 3:
                    targetNode.Down = newNode;
                    newNode.Position.y -= length * blockSize;
                    newNode.Up = targetNode;
                    break;
            }
            allNodes.Add(newNode);
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
            path.target.ClosestTileToStart = path.source;
            distance = (int)(target.Up.Position.y - target.Position.y);
            distance /= blockSize;
            for (int i = 1; i < distance; i++)
            {
                tile = new Tile();
                tile.Position = new Vector2(target.Position.x, target.Position.y + blockSize * i);
                tile.ClosestTileToStart = source;
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
            path.target.ClosestTileToStart = path.source;
            distance = (int)(target.Position.y - target.Down.Position.y);
            distance /= blockSize;
            for (int i = 1; i < distance; i++)
            {
                tile = new Tile();
                tile.Position = new Vector2(target.Position.x, target.Position.y - blockSize * i);
                path.tiles.Add(tile);
                tile.ClosestTileToStart = source;
            }
            MainPaths.Add(path);
            BuildPaths(target.Down, target);
        }

        if (target.Left != null && target.Left != source)
        {
            path = new Path();
            path.source = target;
            path.target = target.Left;
            path.target.ClosestTileToStart = path.source;
            distance = (int)(target.Left.Position.x - target.Position.x);
            distance /= blockSize;
            for (int i = 1; i < distance; i++)
            {
                tile = new Tile();
                tile.Position = new Vector2(target.Position.x + i * blockSize, target.Position.y);
                path.tiles.Add(tile);
                tile.ClosestTileToStart = source;
            }
            MainPaths.Add(path);
            BuildPaths(target.Left, target);
        }

        if (target.Right != null && target.Right != source)
        {
            path = new Path();
            path.source = target;
            path.target = target.Right;
            path.target.ClosestTileToStart = path.source;
            distance = (int)(target.Position.x - target.Right.Position.x);
            distance /= blockSize;
            for (int i = 1; i < distance; i++)
            {
                tile = new Tile();
                tile.Position = new Vector2(target.Position.x - i * blockSize, target.Position.y);
                path.tiles.Add(tile);
                tile.ClosestTileToStart = source;
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
            foreach (Path p in MainPaths.Where(p => additionalNodes.Contains(p.target) || additionalNodes.Contains(p.target)))
            {
                int counter = 0;
                foreach (Path path in MainPaths)
                {
                    if (path != p)
                        counter += CountOverlappingTiles(p, path);
                }
                if (counter > 3)
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

        if (V2Equal(p1.target.Position, p2.target.Position))
            res++;
        if (V2Equal(p1.target.Position, p2.source.Position))
            res++;
        if (V2Equal(p1.source.Position, p2.target.Position))
            res++;
        if (V2Equal(p1.source.Position, p2.source.Position))
            res++;

        foreach (Tile t in p1.tiles)
        {
            if (p2.tiles.Any(x => V2Equal(t.Position, x.Position)))
                res++;
        }


        return res;
    }

    private void RemovePath(Path p)
    {
        Debug.Log("Removing path");
        p.target.RemoveNode(p.source);
        p.source.RemoveNode(p.target);
        allNodes.Remove(p.target);
        additionalNodes.Remove(p.target);
        Paths.Remove(p);


    }



    private bool V2Equal(Vector2 v1, Vector2 v2)
    {

        return Vector3.SqrMagnitude(v1 - v2) < 0.0001;
    }
}
