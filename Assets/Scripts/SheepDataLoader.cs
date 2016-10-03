using UnityEngine;
using System.Collections;

public class SheepDataLoader : MonoBehaviour
{

    [SerializeField]
    private GameObject[] Children;
    [SerializeField]
    private SheepData[] DefaultLoadout;

    // Use this for initialization
    void Start()
    {
        var go = GameObject.FindGameObjectWithTag("GameStatus");
        SheepData[] sheep;
        if (go != null)
            sheep = go.GetComponent<GameStatus>().Sheep;
        else
            sheep = DefaultLoadout;

        int i = 0;
        foreach (var sh in sheep)
        {
            Children[i].GetComponent<SheepDataHolder>().LoadSheepData(sh);
            i++; 
        }
    }
}
