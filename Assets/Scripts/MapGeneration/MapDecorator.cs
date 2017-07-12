using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MapDecorator : MonoBehaviour
{

    public MapGenerator generator;
    public Spawner spawner;
    public Vector3 StartingPoint;

    public GameObject ErrorBlock;
    public GameObject Map;

    #region BlocksLists
    public List<GameObject> UpRights;
    public List<GameObject> UpDowns;
    public List<GameObject> UpLefts;

    public List<GameObject> RightDowns;
    public List<GameObject> RightLefts;
    public List<GameObject> RightUps;

    public List<GameObject> DownLefts;

    public List<GameObject> RightDownLefts;
    public List<GameObject> DownLeftUps;
    public List<GameObject> LeftUpRights;
    public List<GameObject> UpRightDowns;

    public List<GameObject> Crosses;

    public List<GameObject> StartRights;
    public List<GameObject> StartDowns;
    public List<GameObject> StartLefts;
    public List<GameObject> StartUps;

    #endregion

    public BiDictionary<Tile, GameObject> NodesTiles { get; private set; }


    public void Decorate()
    {
        NodesTiles = new BiDictionary<Tile, GameObject>();
        Map.transform.position = StartingPoint;
        InstantiateAll();
        SetClosestNodeToStart();
        spawner.Spawn();
    }
    private void InstantiateAll()
    {
        InstantiateNodes(generator.AllNodes);
        InstantiatePaths(generator.Paths);
        Debug.Log("Nodes in dict: " + NodesTiles.Count);
    }
    private void InstantiateNodes(IEnumerable<Node> Node)
    {
        foreach (Node node in Node)
        {
           
            GameObject TilePrefab = SelectPrefabForNode(node);
            GameObject go = Instantiate(TilePrefab, new Vector3(node.Position.x, 0, node.Position.y), Quaternion.identity) as GameObject;
            go.name += " - Node";
            go.transform.SetParent(Map.transform, true);
            NodesTiles.Add(node, go);
            if (node != generator.StartingNode && node != generator.FinishNode)
            {
                spawner.PossibleSpawnPoints.Add(go);
            }
        }


    }
    private void InstantiatePaths(List<Path> paths)
    {
        GameObject go;
        BlockDataHolder holder, hold;
        Debug.Log(paths.Count);
        foreach (Path path in paths)
        {
            GameObject TilePrefab;
            if (!NodesTiles.ContainsKey(path.source) || !NodesTiles.ContainsKey(path.target))
                continue;
            holder = NodesTiles[path.source].GetComponent<BlockDataHolder>();
            if (path.source.Position.x == path.target.Position.x)
                TilePrefab = UpDowns[0];
            else
                TilePrefab = RightLefts[0];

            foreach (Tile t in path.tiles)
            {
                go = Instantiate(TilePrefab, new Vector3(t.Position.x, 0, t.Position.y), Quaternion.identity) as GameObject;
                hold = go.GetComponent<BlockDataHolder>();
                hold.NeighbouringBlocks.Add(holder);
                holder.NeighbouringBlocks.Add(hold);
                holder = hold;
                go.transform.SetParent(Map.transform, false);
                NodesTiles.Add(t, go);
                spawner.PossibleSpawnPoints.Add(go);
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

        if (up && right && down && left)
            return Crosses.GetRandomElement();

        if (!up && right && down && left)
            return RightDownLefts.GetRandomElement();

        if (up && !right && down && left)
            return UpRightDowns.GetRandomElement();

        if (up && right && !down && left)
            return LeftUpRights.GetRandomElement();

        if (up && right && down && !left)
            return DownLeftUps.GetRandomElement();

        if (!up && !right && down && left)
            return RightDowns.GetRandomElement();

        if (up && !right && !down && left)
            return UpRights.GetRandomElement();

        if (up && right && !down && !left)
            return UpLefts.GetRandomElement();

        if (!up && right && down && !left)
            return DownLefts.GetRandomElement();

        if (!up && right && !down && left)
            return RightLefts.GetRandomElement();

        if (up && !right && down && !left)
            return UpDowns.GetRandomElement();

        if (up && !right && !down && !left)
            return StartUps.GetRandomElement();

        if (!up && right && !down && !left)
            return StartRights.GetRandomElement();

        if (!up && !right && down && !left)
            return StartDowns.GetRandomElement();

        if (!up && !right && !down && left)
            return StartLefts.GetRandomElement();

        return ErrorBlock;
    }
    private void SetClosestNodeToStart()
    {
        var startingBlockData = NodesTiles[generator.StartingNode].GetComponent<BlockDataHolder>();
        var path = generator.Paths.First(x => x.source == generator.StartingNode);
        startingBlockData.NodeToMain = NodesTiles[path.target];

        foreach (Path p in generator.Paths)
        {
            foreach (Tile t in p.tiles)
            {
                var go = NodesTiles[t];
                go.GetComponent<BlockDataHolder>().NodeToMain = NodesTiles[p.source];
            }
            NodesTiles[p.target].GetComponent<BlockDataHolder>().NodeToMain = NodesTiles[p.source];
        }
    }

}
