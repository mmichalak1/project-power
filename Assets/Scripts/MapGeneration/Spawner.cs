using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

    public MapGenerator generator;
    public MapDecorator decorator;
    public GameObject BattleUI;
    public GameObject ExploraionUI;
    public GameObject Player;
    public TurnManager TurnManager;

    public List<GameObject> NormalEnemiesGroups;
    public List<GameObject> SpecialEnemiesGroups;
    public List<GameObject> LastEnemiesGroups;
    public GameObject BossGroupPreafab;
    public GameObject PlayerPrefab;
    public Vector3 SpawnOffsetValue;

    public int MinNormalEnemies = 3;
    public int MaxNormalEnemies = 5;
    public int NormalEnemiesCount;
    public void Spawn()
    {
        SpawnPlayer();
        ///TODO: Add spawning for boss
        SpawnLastEnemy();
        NormalEnemiesCount = Random.Range(MinNormalEnemies, MaxNormalEnemies);
        for (int i=0; i<NormalEnemiesCount; i++)
        {

        }
    }

    private void SpawnPlayer()
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
        comp.ShallNotPass = ExploraionUI.transform.FindChild("ShallNotPass").GetComponent<Image>();
        comp.currentTile = blk.SpawnTile.GetComponent<TileData>();

        var arr = Player.GetComponentsInChildren<EntityDataHolder>();
        for(int i =0;i<4;i++)
        {
            TurnManager.DataHolders[i] = arr[i];
        }

        foreach (var x in Player.GetComponentsInChildren<SheepDataLoader>())
            x.LoadSheepData();

    }

    private void SpawnLastEnemy()
    {
        var go = LastEnemiesGroups.GetRandomElement();
        var finishBlockData = decorator.NodesTiles[generator.FinishNode].GetComponent<BlockDataHolder>();

        //SpawnGroup(go, finishBlockData.SpawnTile.transform.position + SpawnOffsetValue, finishBlockData);
       

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

    private void SpawnGroup(GameObject prefab, Vector3 spawnPoint, BlockDataHolder blockData)
    {
        var group = Instantiate(prefab, spawnPoint, Quaternion.identity) as GameObject;
        group.transform.localRotation *= CalculateEuler(decorator.NodesTiles[blockData.gameObject] as Node, decorator.NodesTiles[blockData.NodeToMain] as Node);
        var comp = group.GetComponent<CheckIfPlayerEnter>();
        comp.ExplorationUI = ExploraionUI;
        comp.BattleUI = BattleUI;
        comp.Player = Player;
    }

}
