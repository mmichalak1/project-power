using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Game/Sheep")]
public class SheepData : ScriptableObject {

    public string Name = "Wacław";

    public int BasicMaxHealth = 100;
    public int BasicAttack = 20;
    public int BasicDefence = 0;

    [HideInInspector]
    public int MaxHealth = 100;
    public int Attack = 20;
    [HideInInspector]
    public int Defence = 0;

    public int Level = 0;
    public int Experience = 0;
    public int ExperienceForNextLevel = 100;
    public int Wool = 1;
    public int MaxWool = 3;
    public SkillHolder SheepSkills;
    public Item OffensiveItem, DefensiveItem;
    public int ExperienceGained;
    public Class SheepClass;

    public void ResetStats()
    {
        ExperienceGained = 0;
        MaxHealth = BasicMaxHealth;
        Attack = BasicAttack;
        Defence = BasicDefence;
    }

    public void LevelUp()
    {
        Level++;
        Experience = 0;
        ExperienceForNextLevel = 50 * Level * (--Level);
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
        BasicAttack += 10;
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
}
