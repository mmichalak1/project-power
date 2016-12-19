using UnityEngine;
using System.Collections;

public class ResetSheeps : MonoBehaviour
{

    public EntityData[] SheepData;
    public EntityData[] SheepDataBackup;
    public ResourceCounter ResourceCounter;
    public WoolCounter WoolCounter;
    public LevelData[] levelData;
    public ItemsLists Items;
    public AllSkills AllSkkills;


    public void Reset()
    {
        WoolCounter.WoolCount = 0;
        ResourceCounter.Resources = ResourceCounter.BasicResources;

        foreach (var item in Items.Items)
            item.Bought = false;

        foreach (var skill in AllSkkills.Skills)
            skill.IsActive = false;


        foreach (LevelData item in levelData)
        {
            item.Progress = 0;
            item.Visited = 0;
            if (item.name != "GreenValley")
                item.IsLocked = true;

        }
        for (int i = 0; i < 4; i++)
        {
            SheepData[i].Name = SheepDataBackup[i].Name;
            SheepData[i].BasicAttack = SheepDataBackup[i].BasicAttack;
            SheepData[i].BasicMaxHealth = SheepDataBackup[i].BasicMaxHealth;
            SheepData[i].BasicDefence = SheepDataBackup[i].BasicDefence;
            SheepData[i].Level = SheepDataBackup[i].Level;
            SheepData[i].Experience = SheepDataBackup[i].Experience;
            SheepData[i].ExperienceForNextLevel = SheepDataBackup[i].ExperienceForNextLevel;
            SheepData[i].Wool = SheepDataBackup[i].Wool;
            SheepData[i].MaxWool = SheepDataBackup[i].MaxWool;
            SheepData[i].WoolGrowth = SheepDataBackup[i].WoolGrowth;
            SheepData[i].SheepClass = SheepDataBackup[i].SheepClass;
            for (int j = 0; j < 4; j++)
            {
                SheepData[i].SheepSkills.Skills[j] = SheepDataBackup[i].SheepSkills.Skills[j];
            }
            SheepData[i].OffensiveItem = null;
            SheepData[i].DefensiveItem = null;
            SheepData[i].DefenceFromItems = 0;
        }
    }
}
