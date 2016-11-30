using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Game/Sheep")]
public class EntityData : ScriptableObject {

    public string Name = "Wacław";
    public Sprite Portrait;

    public int BasicMaxHealth = 100;
    public int BasicAttack = 20;
    public int BasicDefence = 0;

    [HideInInspector]
    public int MaxHealthFromItems = 100;
    [HideInInspector]
    public int AttackFromItems = 20;
    [HideInInspector]
    public int DefenceFromItems = 0;
    
    public int TotalHealth
    {
        get { return BasicMaxHealth + MaxHealthFromItems; }
    }
    public int TotalDefence
    {
        get
        {
            float defenceFromWool = (Wool / MaxWool) * 0.3f;
            float defenceSum = (BasicDefence + DefenceFromItems / 60) * 0.3f;
            float result = defenceFromWool + defenceSum;
            if (result > 60)
                result = 60.0f;
            return (int)result;
        }
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
        MaxHealthFromItems = 0;
        AttackFromItems = 0;
        DefenceFromItems = 0;
    }

    public void LevelUp()
    {
        Level++;
        Experience -= ExperienceForNextLevel;
        if (Level > 1)
            ExperienceForNextLevel = ExperienceForNextLevel * 5;
        //TODO DELETE IT!
        //MaxWool += 2;
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

    private  void LevelUpWarrior()
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
        WoolGrowth -= additionalWool;
        Wool += additionalWool;
    }
}
