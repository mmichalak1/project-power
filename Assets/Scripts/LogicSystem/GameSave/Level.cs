using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.LogicSystem.GameSave;


public class Level
{
    public static Level CreateFromRuntime(LevelData data)
    {
        Level result = new Level();
        result.Chests = new List<Chest>();

        result.Progress = data.Progress;
        result.TargetProgress = data.TargetProgress;
        result.Visited = data.Visited;
        result.IsLocked = data.IsLocked;
        foreach (var chest in data.Chests)
        {
            result.Chests.Add(Chest.CreateFromRuntime(chest));
        }
        return result;
    }

    public static void CreateFromSavedData(ref LevelData level, Level data)
    {
        level.Progress = data.Progress;
        level.TargetProgress = data.TargetProgress;
        level.Visited = data.Visited;
        level.IsLocked = data.IsLocked;
        foreach (var chest in data.Chests)
        {
            var target = level.Chests.First(x => x.name == chest.Name);
            Chest.CreateFromSavedData(ref target, chest);
        }
    }

    public int Progress { get; set; }
    public int TargetProgress { get; set; }
    public int Visited { get; set; }
    public bool IsLocked { get; set; }
    public List<Chest> Chests { get; set; }
}

