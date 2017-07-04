using UnityEngine;
using System.Collections.Generic;

public class MapDecorator : MonoBehaviour
{

    public MapGenerator generator;
    public Spawner spawner;
    public Vector3 StartingPoint;

    public GameObject ErrorBlock;
    public GameObject Map;

    public List<GameObject> VerForwardBlocks;
    public List<GameObject> HorForwardBlocks;
    public List<GameObject> CrossingBlocks;
    public List<GameObject> LeftForwardBlocks;
    public List<GameObject> LeftBlocks;
    public List<GameObject> RightForwardBlocks;
    public List<GameObject> RightLeftBlocks;
    public List<GameObject> RightBlocks;

    public Dictionary<Node, GameObject> NodesTiles { get; private set; }


    public void Decorate()
    {
        NodesTiles = new Dictionary<Node, GameObject>();
        Map.transform.position = StartingPoint;
        InstantiateAll();      
        spawner.Spawn();
    }
    private void InstantiateAll()
    {
        InstantiateNodes(generator.MainNodes);
        InstantiateNodes(generator.AdditionalNodes);
        //Debug.Log("Nodes in dict: " + NodesTiles.Count);
        InstantiatePaths(generator.Paths);
    }



    private void InstantiateNodes(IEnumerable<Node> Node)
    {
        foreach (Node node in Node)
        {
            GameObject TilePrefab = SelectPrefabForNode(node);
            GameObject go = Instantiate(TilePrefab, new Vector3(node.Position.x, 0, node.Position.y), TilePrefab.transform.rotation) as GameObject;
            go.transform.SetParent(Map.transform, true);
            NodesTiles.Add(node, go);
        }


    }
    private void InstantiatePaths(List<Path> MainPaths)
    {
        GameObject go;
        BlockDataHolder holder, hold;
        //Debug.Log(MainPaths.Count);
        foreach (Path path in MainPaths)
        {
            GameObject TilePrefab;
            if (!NodesTiles.ContainsKey(path.source) || !NodesTiles.ContainsKey(path.target))
                continue;
            holder = NodesTiles[path.source].GetComponent<BlockDataHolder>();
            if (path.source.Position.x == path.target.Position.x)
                TilePrefab = HorForwardBlocks[0];
            else
                TilePrefab = VerForwardBlocks[0];

            foreach (Tile t in path.tiles)
            {
                go = Instantiate(TilePrefab, new Vector3(t.Position.x, 0, t.Position.y), TilePrefab.transform.rotation) as GameObject;
                hold = go.GetComponent<BlockDataHolder>();
                hold.NeighbouringBlocks.Add(holder);
                holder.NeighbouringBlocks.Add(hold);
                holder = hold;
                go.transform.SetParent(Map.transform, false);
            }
            hold = NodesTiles[path.target].GetComponent<BlockDataHolder>();
            hold.NeighbouringBlocks.Add(holder);
            holder.NeighbouringBlocks.Add(hold);
        }
    }


    private GameObject SelectPrefabForNode(Node node)
    {
        bool right = node.Right != null;
        bool left = node.Left != null;
        bool up = node.Up != null;
        bool down = node.Down != null;

        if (down && up && left && right)
        {
            return CrossingBlocks[0];
        }

        if (down && left && up && !right)
        {
            return LeftForwardBlocks[0];
        }

        if (down && left && !right && !up)
        {
            return LeftBlocks[0];
        }

        if (up && right && down && !left)
        {
            return RightForwardBlocks[0];
        }

        if (right && left && down && !up)
        {
            return RightLeftBlocks[0];
        }

        if (down && right && !up && !left)
        {
            return RightBlocks[0];
        }

        if ((left || right) && !up && !down)
            return HorForwardBlocks[0];

        if ((up || down) && !right && !left)
            return VerForwardBlocks[0];

            return ErrorBlock;
    }


}
