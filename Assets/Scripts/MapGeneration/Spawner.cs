using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    public MapGenerator generator;
    public MapDecorator decorator;
    public PlayerSpawner playerSpawner;
    public ChestSpawner chestSpawner;
    public GameObject BattleUI;
    public GameObject ExplorationUI;
    public LevelData Data;
    public GameState gameState;

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
        playerSpawner.SpawnPlayer();
        //Debug.Log("Possible Spawns: " + PossibleSpawnPoints.Count);
        if(Data.Progress < Data.TargetProgress)
        {
            Debug.Log("Spawn Last Group");
            var lastGroup = SpawnLastEnemy();
            gameState.LastGroup = lastGroup;
        }
        else
        {
            //Debug.Log("Spawn Boss");
            var lastGroup = SpawnBoss();
            gameState.LastGroup = lastGroup;
        }
        NormalEnemiesCount = Random.Range(MinNormalEnemies, MaxNormalEnemies);
        for (int i=0; i<NormalEnemiesCount; i++)
        {
           SpawnNormalGroup();
        }
        chestSpawner.SpawnChests();
    }

    

    private GameObject SpawnLastEnemy()
    {
        var go = LastEnemiesGroups.GetRandomElement();
        var finishBlockData = decorator.NodesTiles[generator.FinishNode].GetComponent<BlockDataHolder>();

        return SpawnGroup(go, finishBlockData);
       

    }
    
    private GameObject SpawnBoss()
    {
        var finishBlockData = decorator.NodesTiles[generator.FinishNode].GetComponent<BlockDataHolder>();

        return SpawnGroup(BossGroupPrefab, finishBlockData);
    }

    private void SpawnNormalGroup()
    {
        var group = NormalEnemiesGroups.GetRandomElement();
        var spawn = PossibleSpawnPoints.GetRandomElement();
        PossibleSpawnPoints.Remove(spawn);
        var blk = spawn.GetComponent<BlockDataHolder>();
        chestSpawner.ChestsSpawns.Remove(blk);

        SpawnGroup(group, blk);
    }

    private GameObject SpawnGroup(GameObject prefab, BlockDataHolder blockData)
    {
        var spawnPoint = blockData.SpawnTile.transform.position + SpawnOffsetValue;
        var group = Instantiate(prefab, spawnPoint, Quaternion.identity) as GameObject;
        group.transform.rotation *= CalculateEuler(decorator.NodesTiles[blockData.gameObject], decorator.NodesTiles[blockData.NodeToMain]) * Quaternion.Euler(0, 180,0);
        var comp = group.GetComponent<CheckIfPlayerEnter>();
        comp.ExplorationUI = ExplorationUI;
        comp.BattleUI = BattleUI;
        comp.Player = playerSpawner.Player;
        return group;
    }
    #endregion

    
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
    

    

}
