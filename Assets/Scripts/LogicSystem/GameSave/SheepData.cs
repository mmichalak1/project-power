using System;
using UnityEditor;
using System.Linq;
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

    public string SkillTreePath;
    public string OffensiveItemPath, DefensiveItemPath;

    public static SheepData CreateFromRuntime(EntityData data)
    {
        SheepData result = new SheepData();
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



        return result;
    }
}
