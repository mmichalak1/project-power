﻿using UnityEngine;
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

    [MenuItem("Tools/Block/SetStartingTiles")]
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
            AllTiles.OrderByDescending(x => x.transform.localPosition.z)
                 .ThenByDescending(x => x.transform.localPosition.x).ToArray();

            int blockSize = (int)Mathf.Sqrt(AllTiles.Count);
            int index = (blockSize * (blockSize / 2)) + blockSize/2;
            obj.GetComponent<BlockDataHolder>().StartingTile = AllTiles[index];
        }
    }
}