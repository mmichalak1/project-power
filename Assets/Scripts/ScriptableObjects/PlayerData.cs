using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private int level;
    [SerializeField]
    private int experience;
    [SerializeField]
    private int experienceForNextLevel;
    [SerializeField]
    private int experienceGained;

    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    public int Experience
    {
        get { return experience; }
        set { experience = value; }
    }
    public int ExperienceForNextLevel
    {
        get { return experienceForNextLevel; }
        set { experienceForNextLevel = value; }
    }
    public int ExperienceGained
    {
        get { return experienceGained; }
        set { experienceGained = value; }
    }

    public void LevelUp()
    {
        Level++;
        Experience -= ExperienceForNextLevel;
        if (Level > 1)
            ExperienceForNextLevel = ExperienceForNextLevel * 5;
    }

}
