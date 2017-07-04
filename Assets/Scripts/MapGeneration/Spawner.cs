using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    public MapGenerator generator;
    public MapDecorator decorator;
    public List<GameObject> NormalEnemiesGroups;
    public List<GameObject> SpecialEnemiesGroups;
    public List<GameObject> LastEnemiesGroups;
    public GameObject BossGroup;
    public GameObject Player;
    public Vector3 PlayerTileOffset;

    public int MinNormalEnemies = 3;
    public int MaxNormalEnemies = 5;
    public int NormalEnemiesCount;
    public void Spawn()
    {
        SpawnPlayer();
        NormalEnemiesCount = Random.Range(MinNormalEnemies, MaxNormalEnemies);
        for (int i=0; i<NormalEnemiesCount; i++)
        {

        }
    }

    public void SpawnPlayer()
    {
        Vector3 spawnPoint = decorator.NodesTiles[generator.StartingNode].GetComponent<BlockDataHolder>().StartingTile.transform.position +
            PlayerTileOffset;
        Instantiate(Player, spawnPoint, Quaternion.identity);
    }
    




}
