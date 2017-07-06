using UnityEngine;
using System.Collections.Generic;

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
       
        Vector3 spawnPoint = decorator.NodesTiles[spawnNode].GetComponent<BlockDataHolder>().SpawnTile.transform.position +
            SpawnOffsetValue;
       
        Player = Instantiate(PlayerPrefab, spawnPoint, Quaternion.identity) as GameObject;
        Player.transform.localRotation *= CalculateEuler(spawnNode, targetNode);

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

        SpawnGroup(go, finishBlockData.SpawnTile.transform.position + SpawnOffsetValue, finishBlockData);
       

    }
    
    private Quaternion CalculateEuler(Node source, Node target)
    {
        if (source.Up == target)
            return Quaternion.Euler(0,-90,0);
        if (source.Down == target)
            return Quaternion.Euler(0, 90, 0);
        if (source.Right == target)
            return Quaternion.Euler(0, 180, 0);

        return Quaternion.identity;
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
