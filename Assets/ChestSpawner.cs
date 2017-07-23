using UnityEngine;
using System.Collections.Generic;

public class ChestSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject ChestPrefab;
    [SerializeField]
    private IList<BlockDataHolder> chestsSpawns = new List<BlockDataHolder>();
    [SerializeField]
    private IList<GameObject> chests = new List<GameObject>();
    [SerializeField]
    private PlayerSpawner playerSpawner;
    [SerializeField]
    private GameObject woolWindow;
    [SerializeField]
    private SwipeManager swipeManager;
    [SerializeField]
    private GameSaverScript gameSaver;
    

    public int ChestsCount = 2;
    public int WoolForChest = 10;
    public IList<BlockDataHolder> ChestsSpawns { get { return chestsSpawns; } }
    public IList<GameObject> Chests { get { return chests; } }



    public void SpawnChests()
    {
        for (int i = 0; i < ChestsCount; i++)
        {
            var blk = ChestsSpawns.GetRandomElement();
            if(blk == null)
            {
                Debug.LogError("Not enough Chest spawns");
                return;
            }
            ChestsSpawns.Remove(blk);
            SpawnChest(blk);
        }

    }

    private void SpawnChest(BlockDataHolder blk)
    {
        var go = Instantiate(ChestPrefab, blk.TileForChest.transform.position, Quaternion.identity) as GameObject;
        var scr = go.GetComponent<ChestScript>();

        scr.Player = playerSpawner.Player;
        scr.WoolWindow = woolWindow;
        scr.SwipeManager = swipeManager;
        scr.GameSaver = gameSaver;
        scr.WoolForChest = WoolForChest;

        chests.Add(go);
    }
}
