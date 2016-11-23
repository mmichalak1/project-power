using System.Collections.Generic;
using System.Linq;
using System;

public class GameSaveData {

    public static void ToGameForm(ref GameSave save, GameSaveData data)
    {
        for (int i = 0; i < save.SheepData.Length; i++)
            SheepData.CreateFromSavedData(ref save.SheepData[i], data.Sheep[i]);
        foreach (var item in data.UnlockedItems)
            save.AllItems.Items.Where(x => x.name == item).First().Bought = true;

    }

    public static GameSaveData ToBinaryForm(GameSave save)
    {
        var result = new GameSaveData();
        result.Sheep = new List<SheepData>();
        result.UnlockedItems = new List<string>();
        //Serialize Sheep data to store on disc
        for (int i = 0; i < save.SheepData.Length; i++)
            result.Sheep.Add(SheepData.CreateFromRuntime(save.SheepData[i]));
        //Serialize item's name as unlocked
        foreach (var item in save.AllItems.Items)
            if (item.Bought)
                result.UnlockedItems.Add(item.name);
        return result;
    }


    public List<string> UnlockedItems
    {
        get;
        set;
    }
    public List<SheepData> Sheep
    {
        get;
        set;
    }


}
