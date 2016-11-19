using UnityEngine;
using System.IO;

public class GameSaverScript : MonoBehaviour {

    public GameSave Save;
    
    void Start()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        Debug.Log(Application.persistentDataPath);
        Saver.Save(Save);
    }
}
