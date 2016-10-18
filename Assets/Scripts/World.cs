using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class World : MonoBehaviour
{

    public bool[,] Paths;
    public int width;
    public int heigth;

    // Use this for initialization
    void Start()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        Paths = new bool[heigth, width];

        var query = tiles.OrderByDescending(x => x.transform.localPosition.z)
                  .ThenByDescending(x => x.transform.localPosition.x)
                  .ToArray();

        int k = 0;

        for (int i = 0; i < heigth; i++)
            for (int j = 0; j < width; j++)
            {
                Paths[i, j] = query[heigth * width - (width * i + j) - 1].GetComponent<TileProperties>().Accessible;
                if (query[i * j + j].GetComponent<TileProperties>().Accessible)
                    k++;
            }

    }
}
