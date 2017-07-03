using UnityEngine;
using System.Collections.Generic;

public class MapDecorator : MonoBehaviour {

    public MapGenerator generator;


    public void Decorate()
    {
        InstantiateAll();
    }


    private void InstantiateAll()
    {
        InstantiateMainNodes();
        InstantiateAdditionalNodes();
        InstantiatePaths();
    }



    private void InstantiateMainNodes(List<Node> MainNodes)
    {
        for (int i = 0; i < MainNodes.Count; i++)
        {
            GameObject go = Instantiate(TilePrefab, new Vector3(MainNodes[i].Position.x, 0.0f, MainNodes[i].Position.y), Quaternion.identity) as GameObject;
            go.GetComponent<MeshRenderer>().material = Blue;

            if (i == 0 || i == MainNodes.Count - 1)
                go.GetComponent<MeshRenderer>().material = Gold;
        }


    }
    private void InstantiateAdditionalNodes(List<Node> AdditionalNodes)
    {
        foreach (Node node in AdditionalNodes)
        {
            GameObject go = Instantiate(TilePrefab, new Vector3(node.Position.x, 0.0f, node.Position.y), Quaternion.identity) as GameObject;
            go.GetComponent<MeshRenderer>().material = Red;
        }
    }
    private void InstantiatePaths(List<Path> MainPaths)
    {
        GameObject go;
        foreach (Path path in MainPaths)
        {
            foreach (Tile t in path.tiles)
            {
                go = Instantiate(TilePrefab, new Vector3(t.Position.x, 0, t.Position.y), Quaternion.identity) as GameObject;
                go.GetComponent<MeshRenderer>().material = Grey;
            }
        }
    }



}
