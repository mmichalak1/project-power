using UnityEngine;
using System.IO;

public class GameSaverScript : MonoBehaviour
{

    public GameSave Save;
    public bool ToSaveGame = false, ToLoadGame = false;

    void Start()
    {
        if (ToSaveGame)
            SaveGame();

        if (ToLoadGame)
            LoadGame();
    }

    public void SaveGame()
    {
        Debug.Log(Application.persistentDataPath);

        Saver.Save(GameSaveData.ToDataSave(Save));

        Debug.Log("Game Saved Successfully");
    }

    public void LoadGame()
    {
        var gs = Saver.Load();
        GameSaveData.ToGameSave(ref Save, gs);
        Debug.Log("Game Loaded Successfully");

    }
}
