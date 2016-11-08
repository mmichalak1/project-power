using UnityEngine;
using System.Collections;

public class SheepDataLoader : MonoBehaviour
{

    [SerializeField]
    private GameObject[] Children;
    [SerializeField]
    private EntityData[] DefaultLoadout;

    // Use this for initialization
    void Start()
    {
        var go = GameObject.FindGameObjectWithTag("GameStatus");
        EntityData[] sheep;
        if (go != null)
            sheep = go.GetComponent<GameStatus>().Sheep;
        else
            sheep = DefaultLoadout;

        for (int i = 0; i < sheep.Length; i++)
        {
            Children[i].GetComponent<EntityDataHolder>().LoadSheepData(sheep[i]);
        }
    }
}
