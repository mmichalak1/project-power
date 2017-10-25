using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour {

    public GameObject Player;

    public GameObject PlayerPrefab;
    public MapGenerator generator;
    public MapDecorator decorator;
    public GameObject ExplorationUI;
    public GameObject LostPanel;
    public TurnManager TurnManager;

    public Vector3 SpawnOffsetValue;

    public void SpawnPlayer()
    {
        Node spawnNode = generator.StartingNode;
        Node targetNode = decorator.NodesTiles[decorator.NodesTiles[spawnNode].GetComponent<BlockDataHolder>().NodeToMain] as Node;

        BlockDataHolder blk = decorator.NodesTiles[spawnNode].GetComponent<BlockDataHolder>();
        Vector3 spawnPoint = blk.SpawnTile.transform.position +
            SpawnOffsetValue;

        Player = Instantiate(PlayerPrefab, spawnPoint, Quaternion.identity) as GameObject;

        Quaternion rot = Quaternion.identity;
        Facing face = Facing.Up;
        CalculatePlayerFacing(spawnNode, targetNode, out rot, out face);
        Player.transform.localRotation *= rot;
        var comp = Player.GetComponent<MovementController>();
        comp.currentFacing = face;
        comp.ShallNotPass = ExplorationUI.transform.Find("ShallNotPass").GetComponent<Image>();
        comp.currentTile = blk.SpawnTile.GetComponent<TileData>();
        TurnManager.SheepGroup = Player.GetComponentInChildren<SheepGroupManager>();
        TurnManager.SheepGroup.LostPanel = LostPanel;
        foreach (var x in Player.GetComponentsInChildren<SheepDataLoader>())
            x.LoadSheepData();

    }

    private void CalculatePlayerFacing(Node source, Node target, out Quaternion res, out Facing face)
    {
        res = CalculateEuler(source, target);
        if (source.Up == target)
        {
            face = Facing.Up;
            return;
        }
        if (source.Down == target)
        {
            face = Facing.Down;
            return;
        }
        if (source.Right == target)
        {
            face = Facing.Left;
            return;
        }
        face = Facing.Right;
    }
    private Quaternion CalculateEuler(Node source, Node target)
    {

        if (source.Up == target)
        {
            return Quaternion.Euler(0, -90, 0);
        }
        if (source.Down == target)
        {
            return Quaternion.Euler(0, 90, 0);
        }
        if (source.Right == target)
        {
            return Quaternion.Euler(0, 180, 0);
        }

        return Quaternion.identity;
    }

}
