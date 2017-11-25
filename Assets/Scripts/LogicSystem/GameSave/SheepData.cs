using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class SheepData
{
    public string IconPath { get; set; }
    public string Name { get; set; }
    public EntityData.Class Class { get; set; }

    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defence { get; set; }

    public int Level { get; set; }
    public int ExperienceGained { get; set; }
    public int Experience { get; set; }
    public int ExperienceForNexLevel { get; set; }

    public int Wool { get; set; }
    public int MaxWool { get; set; }
    public float WoolGrowth { get; set; }
    public string OffensiveItem { get; set; }
    public string DefensiveItem { get; set; }
    public SheepSkillsData SheepSkills { get; set; }

    public static SheepData CreateFromRuntime(EntityData data)
    {
        SheepData result = new SheepData();
        var sheepSkills = new SheepSkillsData();
        SheepSkillsData.CreateFromRuntime(data.SheepSkills, ref sheepSkills);

        result.IconPath = data.Portrait.name;
        result.Name = data.name;
        result.Class = data.SheepClass;
        result.Health = data.BasicMaxHealth;
        result.Attack = data.BasicAttack;
        result.Defence = data.BasicDefence;
        result.Level = data.Level;
        result.Experience = data.Experience;
        result.ExperienceGained = data.ExperienceGained;
        result.ExperienceForNexLevel = data.ExperienceForNextLevel;
        result.Wool = data.Wool;
        result.MaxWool = data.MaxWool;
        result.WoolGrowth = data.WoolGrowth;
        result.SheepSkills = sheepSkills;



        if (data.OffensiveItem != null)
            result.OffensiveItem = data.OffensiveItem.name;
        if (data.DefensiveItem != null)
            result.DefensiveItem = data.DefensiveItem.name;

        return result;
    }

    public static void CreateFromSavedData(ref EntityData entity, SheepData data)
    {
        entity.Portrait = Resources.Load<Sprite>("Sprites/Portraits/" + data.IconPath);
        entity.Name = data.Name;
        entity.SheepClass = data.Class;
        entity.BasicMaxHealth = data.Health;
        entity.BasicAttack = data.Attack;
        entity.BasicDefence = data.Defence;
        entity.Level = data.Level;
        entity.Experience = data.Experience;
        entity.ExperienceGained = data.ExperienceGained;
        entity.ExperienceForNextLevel = data.ExperienceForNexLevel;
        entity.Wool = data.Wool;
        entity.MaxWool = data.MaxWool;
        entity.WoolGrowth = data.WoolGrowth;
        SheepSkillsData.CreateFromGameSave(ref entity.SheepSkills, data.SheepSkills);

    }
}
