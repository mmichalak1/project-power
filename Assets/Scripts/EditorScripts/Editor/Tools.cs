using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class EditorTools {

    [MenuItem("Tools/Blocks/RoundPositions")]
	private static void RoundPostions()
    {
        Vector3 newPos;
        Vector3 pos;
        foreach (GameObject obj in Selection.gameObjects)
        {
            pos = obj.transform.position;
            newPos = new Vector3(Mathf.RoundToInt(pos.x), 0.0f, Mathf.RoundToInt(pos.z));
            obj.transform.position = newPos;
        }
    }

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
        List<GameObject> AllTiles = new List<GameObject>();
        foreach (GameObject obj in Selection.gameObjects)
        {
            Transform child = obj.transform.GetChild(0);
            for (int i = 0; i < child.childCount; i++)
            {
                AllTiles.Add(child.GetChild(i).gameObject);
                Debug.Log(AllTiles.Count);
               
            }
        }
    }
}
