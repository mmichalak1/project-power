using UnityEngine;
using System.IO;

public class GameSaverScript : MonoBehaviour
{

    public GameSave Save;
    public bool SaveOnStart = false, LoadOnStart = false;

    void Start()
    {
        if (SaveOnStart)
            SaveGame();

        if (LoadOnStart)
            LoadGame();
    }

    public void SaveGame()
    {
        //Debug.Log(Application.persistentDataPath);

        Saver.Save(GameSaveData.ToBinaryForm(Save));

        Debug.Log("Game Saved Successfully");
    }

    public void LoadGame()
    {
        var gs = Saver.Load();
        if (gs == null)
        {
            Debug.Log("Load Error");
            return;
        }
        GameSaveData.ToGameForm(ref Save, gs);
        Debug.Log("Game Loaded Successfully");

    }
}
