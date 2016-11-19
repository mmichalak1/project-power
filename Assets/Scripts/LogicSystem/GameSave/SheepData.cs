using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class SheepData
{
    public string IconPath;
    public string Name;
    public EntityData.Class Class;

    public int Health;
    public int Attack;
    public int Defence;

    public int Level;
    public int ExperienceGained;
    public int Experience;
    public int ExperienceForNexLevel;

    public int Wool;
    public int MaxWool;
    public float WoolGrowth;

    public List<string> CurrentSkills = new List<string>();
    public string OffensiveItemPath, DefensiveItemPath;

    public static SheepData CreateFromRuntime(EntityData data)
    {
        SheepData result = new SheepData();
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
        foreach (var item in data.SheepSkills.Skills)
            result.CurrentSkills.Add(item.name);
        if (data.OffensiveItem != null)
            result.OffensiveItemPath = data.OffensiveItem.name;
        if (data.DefensiveItem != null)
            result.DefensiveItemPath = data.DefensiveItem.name;

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

    }
}
