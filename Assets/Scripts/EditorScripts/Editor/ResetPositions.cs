using UnityEngine;
using UnityEditor;

public class ResetPositions {

    [MenuItem("Tools/RoundPositions")]
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
}
