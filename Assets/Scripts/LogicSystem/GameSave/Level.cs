using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Level
{
    public static Level CreateFromRuntime(LevelData data)
    {
        Level result = new Level();
        result.Name = data.Name;
        result.Progress = data.Progress;
        result.TargetProgress = data.TargetProgress;
        result.Visited = data.Visited;
        return result;
    }

    public static void CreateFromSavedData(ref LevelData level, Level data)
    {
        level.Name = data.Name;
        level.Progress = data.Progress;
        level.TargetProgress = data.TargetProgress;
        level.Visited = data.Visited;
    }

    public string Name { get; set; }
    public int Progress { get; set; }
    public int TargetProgress { get; set; }
    public int Visited { get; set; }
}

