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
    public LevelData Data;

    public List<GameObject> NormalEnemiesGroups;
    public List<GameObject> SpecialEnemiesGroups;
    public List<GameObject> LastEnemiesGroups;
    public GameObject BossGroupPrefab;
    public GameObject PlayerPrefab;
    public Vector3 SpawnOffsetValue;

    public int MinNormalEnemies = 3;
    public int MaxNormalEnemies = 5;
    public int NormalEnemiesCount;

    private List<GameObject> _possibleSpawnPoints = new List<GameObject>();
    public List<GameObject> PossibleSpawnPoints { get { return _possibleSpawnPoints; } }

    #region Spawn Methods
    public void Spawn()
    {
        Debug.Log("Possible Spawns: " + PossibleSpawnPoints.Count);
        SpawnPlayer();
        if(Data.Progress < Data.TargetProgress)
        {
            Debug.Log("Spawn Last Group");
            SpawnLastEnemy();
        }
        else
        {
            Debug.Log("Spawn Boss");
            SpawnBoss();
        }
        NormalEnemiesCount = Random.Range(MinNormalEnemies, MaxNormalEnemies);
        for (int i=0; i<NormalEnemiesCount; i++)
        {
            SpawnNormalGroup();
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

        SpawnGroup(go, finishBlockData);
       

    }
    
    private void SpawnBoss()
    {
        var finishBlockData = decorator.NodesTiles[generator.FinishNode].GetComponent<BlockDataHolder>();

        SpawnGroup(BossGroupPrefab, finishBlockData);
    }

    private void SpawnNormalGroup()
    {
        var group = NormalEnemiesGroups.GetRandomElement();
        var spawn = PossibleSpawnPoints.GetRandomElement();
        PossibleSpawnPoints.Remove(spawn);
        var blk = spawn.GetComponent<BlockDataHolder>();

        SpawnGroup(group, blk);
    }

    private void SpawnGroup(GameObject prefab, BlockDataHolder blockData)
    {
        var spawnPoint = blockData.SpawnTile.transform.position + SpawnOffsetValue;
        var group = Instantiate(prefab, spawnPoint, Quaternion.identity) as GameObject;
        group.transform.rotation *= CalculateEuler(decorator.NodesTiles[blockData.gameObject], decorator.NodesTiles[blockData.NodeToMain]) * Quaternion.Euler(0, 180,0);
        var comp = group.GetComponent<CheckIfPlayerEnter>();
        comp.ExplorationUI = ExploraionUI;
        comp.BattleUI = BattleUI;
        comp.Player = Player;
    }
    #endregion

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

    private Quaternion CalculateEuler(Tile source, Tile target)
    {
        if ( source.Position.x == target.Position.x)
        {
            if (source.Position.y > target.Position.y)
            {
                return Quaternion.Euler(0, 90, 0);
            }
            else
            {
                return Quaternion.Euler(0, -90, 0);
            }
        }
        else if(source.Position.x > target.Position.x)
        {
            return Quaternion.Euler(0, 180, 0);
        }
        else
        {
            return Quaternion.identity;
        }

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

    

}
