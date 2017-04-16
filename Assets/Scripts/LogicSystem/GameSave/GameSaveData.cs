using System.Collections.Generic;
using System.Linq;
using System;

public class GameSaveData
{

    public static void ToGameForm(ref GameSave save, GameSaveData data)
    {
        //Read and unlock all items listed
        foreach (var item in data.UnlockedItems)
            save.AllItems.Items.Where(x => x.name == item).First().Bought = true;
        //Load SheepData to runtime and apply which skills are applied to given sheep
        for (int i = 0; i < save.SheepData.Length; i++)
        {
            save.SheepData[i].OffensiveItem = save.AllItems.Items.FirstOrDefault(x => x.name == data.Sheep[i].OffensiveItem);
            save.SheepData[i].DefensiveItem = save.AllItems.Items.FirstOrDefault(x => x.name == data.Sheep[i].DefensiveItem);
            SheepData.CreateFromSavedData(ref save.SheepData[i], data.Sheep[i]);
            for (int j = 0; j < data.Sheep[i].CurrentSkills.Count; j++)
            {
                save.SheepData[i].SheepSkills.Skills[j] = save.AllSkills.Skills.First(x => x.Name == data.Sheep[i].CurrentSkills[j]);
            }
        }
        //Load LevelData from save
        for (int i = 0; i < save.LevelData.Length; i++)
        {
            Level.CreateFromSavedData(ref save.LevelData[i], data.Levels[i]);
        }
        //Load Seen Tutorials
        foreach (var item in data.Tutorials)
        {
            var myTut = save.Tutorials.Tutorials.FirstOrDefault(x => x.name == item.TutorialName);
            if (myTut != null)
                TutorialEntry.FromData(ref myTut, item);
        }
        //Load Player Account Level
        Player.CreateFromSave(ref save.PlayerData, data.PlayerData);
        //Read current wool and resources data
        save.ResourceCounter.Resources = data.Resources;
        save.WoolCounter.WoolCount = data.WoolAmount;

    }


    public static GameSaveData ToBinaryForm(GameSave save)
    {
        var result = new GameSaveData();
        result.Sheep = new List<SheepData>();
        result.UnlockedItems = new List<string>();
        result.Levels = new List<Level>();
        result.Tutorials = new List<TutorialEntry>();
        //Serialize Sheep data to store on disc
        for (int i = 0; i < save.SheepData.Length; i++)
            result.Sheep.Add(SheepData.CreateFromRuntime(save.SheepData[i]));
        //Serialize item's name as unlocked
        foreach (var item in save.AllItems.Items)
            if (item.Bought)
                result.UnlockedItems.Add(item.name);
        //Serialize All Levels Data
        for (int i = 0; i < save.LevelData.Length; i++)
            result.Levels.Add(Level.CreateFromRuntime(save.LevelData[i]));
        //Serialize Tutorials
        foreach(var item in save.Tutorials.Tutorials)
        {
            result.Tutorials.Add(TutorialEntry.FromRuntime(item));
        }
        //Serialize PlayerData
        result.PlayerData = Player.CreateFromRuntime(save.PlayerData);
        //Serialize Wool and resources
        result.WoolAmount = save.WoolCounter.WoolCount;
        result.Resources = save.ResourceCounter.Resources;
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

    public List<Level> Levels
    {
        get;
        set;
    }

    public List <TutorialEntry> Tutorials
    {
        get;
        set;
    }
    public Player PlayerData { get; set; }
    public int WoolAmount { get; set; }
    public int Resources { get; set; }

}
