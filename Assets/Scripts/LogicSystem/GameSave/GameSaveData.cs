using System.Collections.Generic;
using System.Linq;
using System;

[Serializable]
public class GameSaveData
{
    public static void ToGameSave (ref GameSave save, GameSaveData data)
    {
        for (int i = 0; i < save.SheepData.Length; i++)
            SheepData.CreateFromSavedData(ref save.SheepData[i], data.Sheep[i]);
        
    }

    public static GameSaveData ToDataSave(GameSave save)
    {
        var result = new GameSaveData();
        for (int i = 0; i < save.SheepData.Length; i++)
            result.Sheep.Add(SheepData.CreateFromRuntime(save.SheepData[i]));


        return result;

    }

    List<SheepData> Sheep = new List<SheepData>();
}
