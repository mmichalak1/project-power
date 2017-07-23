using UnityEngine;
using System.Collections.Generic;

public class ChestSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject ChestPrefab;
    [SerializeField]
    private IList<BlockDataHolder> chestsSpawns = new List<BlockDataHolder>();

    public IList<BlockDataHolder> ChestsSpawns { get { return chestsSpawns; } }

    public void SpawnChests()
    {

    }
}
