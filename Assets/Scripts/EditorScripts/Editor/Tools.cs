using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class EditorTools {

    [MenuItem("Tools/Blocks/SetTilesParent")]
    private static void SetTilesParent()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Transform child = obj.transform.GetChild(0);
            for(int i=0; i<child.childCount; i++)
            {
                GameObject tile = child.GetChild(i).gameObject;
                var scr = tile.GetComponent<TileData>();
                if (scr != null)
                    Object.DestroyImmediate(scr);
                scr = tile.AddComponent<TileData>();
                scr.ParentBlock = obj.GetComponent<BlockDataHolder>();
            }
        }
    }

    [MenuItem("Tools/Blocks/SetStartingTiles")]
    private static void SetStartingTiles()
    {
        
        foreach (GameObject obj in Selection.gameObjects)
        {
            List<GameObject> AllTiles = new List<GameObject>();
            Transform child = obj.transform.GetChild(0);
            for (int i = 0; i < child.childCount; i++)
            {
                AllTiles.Add(child.GetChild(i).gameObject);                
            }
            AllTiles = AllTiles.OrderByDescending(x => x.transform.localPosition.z)
                 .ThenByDescending(x => x.transform.localPosition.x).ToList();

            int blockSize = (int)Mathf.Sqrt(AllTiles.Count);
            int index = (blockSize * (blockSize / 2)) + blockSize/2;
            obj.GetComponent<BlockDataHolder>().SpawnTile = AllTiles[index];
        }
    }

    [MenuItem("Tools/Blocks/SetNeighbouringTiles")]
    private static void SetNeighbouringTiles()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            List<GameObject> AllTiles = new List<GameObject>();
            Transform child = obj.transform.GetChild(0);
            for (int i = 0; i < child.childCount; i++)
            {
                AllTiles.Add(child.GetChild(i).gameObject);
            }
            Debug.Log("Tiles count :" + AllTiles.Count);
            AllTiles = AllTiles.OrderBy(x => x.transform.position.z)
                 .ThenBy(x => x.transform.position.x).ToList();



           for (int i = 0; i<AllTiles.Count; i++)
           {
                var scr = AllTiles[i].GetComponent<TileData>();
                if (scr == null)
                {
                    Debug.LogError("Tile does not contain TileData component.");
                    return;
                }

                int blockSize = (int)Mathf.Sqrt(AllTiles.Count);
                scr.LeftNeighbour = null;
                scr.RightNeighbour = null;
                scr.UpNeighbour = null;
                scr.DownNeighbour = null;


                //Left Tile
                if (i % blockSize != 0 && i > 0)
                {
                    scr.LeftNeighbour = AllTiles[i - 1].GetComponent<TileData>();
                }
                //Right Tile
                if((i+1) % blockSize != 0 && i + 1 < AllTiles.Count)
                {
                    scr.RightNeighbour = AllTiles[i + 1].GetComponent<TileData>();
                }
                //Up Tile
                if(i+blockSize<AllTiles.Count)
                {
                    scr.UpNeighbour = AllTiles[i + blockSize].GetComponent<TileData>();
                }
                //Down Tile
                if(i>blockSize)
                {
                    scr.DownNeighbour = AllTiles[i - blockSize].GetComponent<TileData>();
                }

            }
        }
    }
}
