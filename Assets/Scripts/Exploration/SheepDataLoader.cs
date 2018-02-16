using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// Loads sheep group data
/// </summary>
public class SheepDataLoader : MonoBehaviour
{
    [SerializeField]
    private bool LoadAtStartup = false;
    [SerializeField]
    private GameObject[] Children;
    [SerializeField]
    private List<EntityData> GameSheepGroup;
    [SerializeField]
    private EntityData[] BasicSheepGroup;

    private void Start()
    {
        if(LoadAtStartup)
        {
            LoadSheepData();
        }
    }

    public void LoadSheepData()
    {
        EntityData[] sheep;
        if (GameSheepGroup.Count == 4)
        {
            sheep = GameSheepGroup.ToArray();
        }
        else
        {
            Debug.LogError("Incorrect sheep count loading default group");
            sheep = BasicSheepGroup;
        }

        for (int i = 0; i < sheep.Length; i++)
        {
            Children[i].GetComponent<EntityDataHolder>().LoadSheepData(sheep[i]);
            Children[i].GetComponent<SheepWoolDisplay>().SetupWoolModels();
        }
    }
}
