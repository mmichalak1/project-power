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
    public int ExperienceToNextLevel = 100;
    public int Wool = 1;
    public int MaxWool = 3;
    public SkillHolder SheepSkills;
    public Item OffensiveItem, DefensiveItem;

    public void ResetStats()
    {
        MaxHealth = BasicMaxHealth;
        Attack = BasicAttack;
        Defence = BasicDefence;
    }
}
