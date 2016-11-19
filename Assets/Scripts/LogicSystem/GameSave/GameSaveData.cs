using System.Collections.Generic;
using System.Linq;
using System;

public class GameSaveData {

    private List<SheepData> _sheep = new List<SheepData>();

    public static void ToGameSave (ref GameSave save, GameSaveData data)
    {
        for (int i = 0; i < save.SheepData.Length; i++)
            SheepData.CreateFromSavedData(ref save.SheepData[i], data.Sheep[i]);
        
    }

    public static GameSaveData ToDataSave(GameSave save)
    {
        var result = new GameSaveData();
        result.Sheep = new List<SheepData>();
        for (int i = 0; i < save.SheepData.Length; i++)
            result.Sheep.Add(SheepData.CreateFromRuntime(save.SheepData[i]));
        return result;
    }

    public List<SheepData> Sheep
    {
        get;
        set;
    }
}
