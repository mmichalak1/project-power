﻿using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Game/Sheep")]
public class EntityData : ScriptableObject
{

    public string Name = "Wacław";
    public Sprite Portrait;

    public int BasicMaxHealth = 100;
    public int BasicAttack = 20;
    public int BasicDefence = 0;

    public int MaxHealthFromItems
    {
        get
        {
            int res = 0;
            if (OffensiveItem != null)
            {
                res += OffensiveItem.GetBonusToStat(Item.Stats.Health);
            }
            if (DefensiveItem != null)
            {
                res += DefensiveItem.GetBonusToStat(Item.Stats.Health);
            }
            return res;
        }
    }
    public int AttackFromItems
    {
        get
        {
            int res = 0;
            if (OffensiveItem != null)
            {
                res += OffensiveItem.GetBonusToStat(Item.Stats.Attack);
            }
            if (DefensiveItem != null)
            {
                res += DefensiveItem.GetBonusToStat(Item.Stats.Attack);
            }
            return res;
        }
    }
    public int DefenceFromItems
    {
        get
        {
            int res = 0;
            if (OffensiveItem != null)
            {
                res += OffensiveItem.GetBonusToStat(Item.Stats.Defense);
            }
            if (DefensiveItem != null)
            {
                res += DefensiveItem.GetBonusToStat(Item.Stats.Defense);
            }
            return res;
        }
    }

    public int TotalHealth
    {
        get { return BasicMaxHealth + MaxHealthFromItems; }
    }
    public int TotalDefence
    {
        get
        {
            //Debug.Log("DefenceFromItems " + DefenceFromItems);
            float result = DefenceFromWool() + DefenceFromItems;
            return (int)result;
        }
    }
    public float DefenceFromWool()
    {
        return ((float)Wool / (float)MaxWool) * 30;
    }
    public int TotalAttack
    {
        get { return AttackFromItems + BasicAttack; }
    }


    public int Level = 0;
    public int Experience = 0;
    public int ExperienceForNextLevel = 100;
    public int Wool = 1;
    public int MaxWool = 3;
    public float WoolGrowth = 0.0f;
    public SkillHolder SheepSkills;
    public Item OffensiveItem, DefensiveItem;
    public int ExperienceGained;
    public Class SheepClass;

    public void ResetStats()
    {
        ExperienceGained = 0;
    }

    public void LevelUp()
    {
        Level++;
        Experience -= ExperienceForNextLevel;
        if (Level > 1)
            ExperienceForNextLevel = ExperienceForNextLevel * 5;
        MaxWool += 15;
        switch (SheepClass)
        {
            case Class.Warrior:
                LevelUpWarrior();
                break;
            case Class.Mage:
                LevelUpMage();
                break;
            case Class.Cleric:
                LevelUpCleric();
                break;
            case Class.Rouge:
                LevelUpRouge();
                break;
            default:
                break;
        }
    }

    public enum Class
    {
        Warrior,
        Mage,
        Cleric,
        Rouge
    }

    private void LevelUpWarrior()
    {
        BasicMaxHealth += 30;
        BasicAttack += 5;
    }

    private void LevelUpMage()
    {
        BasicMaxHealth += 10;
        BasicAttack += 7;
    }

    private void LevelUpRouge()
    {
        BasicAttack += 7;
        BasicMaxHealth += 15;
    }

    private void LevelUpCleric()
    {
        BasicMaxHealth += 20;
        BasicAttack += 5;
    }

    public void GrowWool()
    {
        int additionalWool = Mathf.FloorToInt(WoolGrowth);
        if (Wool + additionalWool < MaxWool)
            Wool += additionalWool;
        else
            Wool = MaxWool;

        WoolGrowth -= additionalWool;
    }
}
