using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Player
{
    public static Player CreateFromRuntime(PlayerData data)
    {
        var result = new Player
        {
            Level = data.Level,
            Experience = data.Experience,
            ExperienceForNextLevel = data.ExperienceForNextLevel
        };
        return result;
    }

    public static void CreateFromSave(ref PlayerData target, Player source)
    {
        target.Level = source.Level;
        target.Experience = source.Experience;
        target.ExperienceForNextLevel = source.ExperienceForNextLevel;
        target.ExperienceGained = 0;
    }

    public int Level { get; set; }
    public int Experience { get; set; }
    public int ExperienceForNextLevel { get; set; }
}
