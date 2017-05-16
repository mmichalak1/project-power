using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{

    public GameObject TilePrefab;
    public Material Blue, Red, Green, Gold, Yellow, Grey;

    public Vector3 StartingPoint;
    public int tileWidth = 1;

    public int BranchMinLength = 3;
    public int BranchMaxLength = 7;

    public int MinMainBranches = 5;
    public int MaxMainBranches = 7;

    public int MinAddidtionalBranches = 2;
    public int MaxAdditionalBranches = 4;

    private int MainNodesCount;
    private int AdditionalNodesCount;

    private Node StartingNode, FinishNode;

    private List<Node> Nodes = new List<Node>();
    private List<Node> MainNodes = new List<Node>();
    private List<Node> AdditionalNodes = new List<Node>();

    private List<Path> Paths = new List<Path>();
    // Use this for initialization
    void Start()
    {
        StartingNode = new Node();
        StartingNode.Position = StartingPoint;
        MainNodes.Add(StartingNode);
        Nodes.Add(StartingNode);
        MainNodesCount = Random.Range(MinMainBranches, MaxMainBranches);
        AdditionalNodesCount = Random.Range(MinAddidtionalBranches, MaxAdditionalBranches);
        BuildMainNodes();
        BuildAdditionalNodes();
        BuildPaths(StartingNode, null);

        InstantiateMainNodes();
        InstantiateAdditionalNodes();
        InstantiatePaths();
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
            MainNodes.Add(newNode);
            if(i != MainNodesCount-1)
                Nodes.Add(newNode);
            switch (direction)
            {
                case 0:
                    lastNode.Up = newNode;
                    newNode.Position.z += length * tileWidth;
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
        
        for (int i=0; i<AdditionalNodesCount;i++ )
        {
            do
            {
                targetNode = Nodes[Random.Range(0, Nodes.Count - 1)];
            }
            while (!targetNode.CanGetNextDirection());

            nextTargetDirection = targetNode.GetNextDirection();
            length = Random.Range(BranchMinLength, BranchMaxLength);
            newNode = new Node();
            newNode.Position = targetNode.Position;
            switch (nextTargetDirection)
            {
                case 0:
                    targetNode.Up = newNode;
                    newNode.Position.z += length * tileWidth;
                    newNode.Down = targetNode;
                    break;
                case 1:
                    targetNode.Left = newNode;
                    newNode.Position.x += + length * tileWidth;
                    newNode.Right = targetNode;
                    break;
                case 2:
                    targetNode.Right = newNode;
                    newNode.Position.x -= length * tileWidth;
                    newNode.Left = targetNode;
                    break;
                case 3:
                    targetNode.Down = newNode;
                    newNode.Position.z -= length * tileWidth;
                    newNode.Up = targetNode;
                    break;
            }
            Nodes.Add(newNode);
            AdditionalNodes.Add(newNode);

        }

    }
    private void BuildPaths(Node target, Node source)
    {
        int distance;
        Path path = new Path();
        Tile tile;
        if (target.Up != null && target.Up != source)
        {
            distance = (int)(target.Up.Position.z - target.Position.z);
            distance /= tileWidth;
            for (int i = 1; i < distance; i++)
            {
                tile = new Tile();
                tile.Position = new Vector3(target.Position.x, target.Position.y, target.Position.z + tileWidth * i);
                path.tiles.Add(tile);
            }

            BuildPaths(target.Up, target);
        }

        if (target.Down != null && target.Down != source)
        {
            distance = (int)(target.Position.z - target.Down.Position.z);
            distance /= tileWidth;
            for (int i = 1; i < distance; i++)
            {
                tile = new Tile();
                tile.Position = new Vector3(target.Position.x, target.Position.y, target.Position.z - tileWidth * i);
                path.tiles.Add(tile);
            }
            BuildPaths(target.Down, target);
        }

        if (target.Left != null && target.Left != source)
        {
            distance = (int)(target.Left.Position.x - target.Position.x);
            distance /= tileWidth;
            for (int i = 1; i < distance; i++)
            {
                tile = new Tile();
                tile.Position = new Vector3(target.Position.x + i * tileWidth, target.Position.y, target.Position.z);
                path.tiles.Add(tile);
            }
            BuildPaths(target.Left, target);
        }

        if (target.Right != null && target.Right != source)
        {
            distance = (int)(target.Position.x - target.Right.Position.x);
            distance /= tileWidth;
            for (int i = 1; i < distance; i++)
            {
                tile = new Tile();
                tile.Position = new Vector3(target.Position.x - i * tileWidth, target.Position.y, target.Position.z);
                path.tiles.Add(tile);
            }
            BuildPaths(target.Right, target);
        }
    }

    private void InstantiateMainNodes()
    {
        for (int i = 0; i < MainNodes.Count; i++)
        {
            GameObject go = Instantiate(TilePrefab, MainNodes[i].Position, Quaternion.identity) as GameObject;
            go.GetComponent<MeshRenderer>().material = Blue;

            if (i == 0 || i == MainNodes.Count - 1)
                go.GetComponent<MeshRenderer>().material = Gold;
        }


    }
    private void InstantiateAdditionalNodes()
    {
        foreach (Node node in AdditionalNodes)
        {
            GameObject go = Instantiate(TilePrefab, node.Position, Quaternion.identity) as GameObject;
            go.GetComponent<MeshRenderer>().material = Red;
        }
    }
    private void InstantiatePaths()
    {
        GameObject go;
        foreach(Path path in Paths)
        {
            foreach(Tile tile in path.tiles)
            {
                go = Instantiate(TilePrefab, tile.Position, Quaternion.identity) as GameObject;
                go.GetComponent<MeshRenderer>().material = Grey;
            }
        }
    }
}
