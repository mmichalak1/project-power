using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Level
{
    public static Level CreateFromRuntime(LevelData data)
    {
        Level result = new Level();

        result.Progress = data.Progress;
        result.TargetProgress = data.TargetProgress;
        result.Visited = data.Visited;
        result.IsLocked = data.IsLocked;
        return result;
    }

    public static void CreateFromSavedData(ref LevelData level, Level data)
    {
        level.Progress = data.Progress;
        level.TargetProgress = data.TargetProgress;
        level.Visited = data.Visited;
        level.IsLocked = data.IsLocked;
    }

    public int Progress { get; set; }
    public int TargetProgress { get; set; }
    public int Visited { get; set; }
    public bool IsLocked { get; set; }
}

