using UnityEngine;
using System.Collections;

public class ResetSheeps : MonoBehaviour {

    public EntityData[] SheepData;
    public EntityData[] SheepDataBackup;
    public WoolUpdater woolUpdater;
    public LevelData[] levelData;

    [SerializeField]
    private WoolCounter DefaultWoolCounter;
    [SerializeField]
    private ResourceCounter DefaultResourceCounter;


    public void Reset()
    {
        DefaultWoolCounter.WoolCount = 0;
        DefaultResourceCounter.Resources = DefaultResourceCounter.BasicResources;
        woolUpdater.UpdateWoolView();

        foreach (LevelData item in levelData)
        {
            item.Progress = 0;
        }
        for (int i = 0; i < 4; i++)
        {
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
                if(j != 1) // for class skill
                    SheepData[i].SheepSkills.Skills[j].IsActive = false;
            }
            SheepData[i].OffensiveItem = null;
            SheepData[i].DefensiveItem = null;
        }
    }
}
