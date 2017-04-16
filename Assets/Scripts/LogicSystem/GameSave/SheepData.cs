using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class SheepData
{
    private List<string> _skills = new List<string>();

    public string IconPath { get; set; }
    public string Name { get; set; }
    public EntityData.Class Class { get; set; }

    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defence { get; set; }

    public int Wool { get; set; }
    public int MaxWool { get; set; }
    public float WoolGrowth { get; set; }
    public string OffensiveItem { get; set; }
    public string DefensiveItem { get; set; }
    public List<string> CurrentSkills
    {
        get { return _skills; }
        set { _skills = value; }
    }

    public static SheepData CreateFromRuntime(EntityData data)
    {
        SheepData result = new SheepData();
        result.IconPath = data.Portrait.name;
        result.Name = data.name;
        result.Class = data.SheepClass;
        result.Health = data.BasicMaxHealth;
        result.Attack = data.BasicAttack;
        result.Defence = data.BasicDefence;
        result.Wool = data.Wool;
        result.MaxWool = data.MaxWool;
        result.WoolGrowth = data.WoolGrowth;
        foreach (var item in data.SheepSkills.Skills)
            result.CurrentSkills.Add(item.name);
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
        entity.Wool = data.Wool;
        entity.MaxWool = data.MaxWool;
        entity.WoolGrowth = data.WoolGrowth;
    }
}
