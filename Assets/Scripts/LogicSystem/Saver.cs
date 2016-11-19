using Polenter.Serialization;
using UnityEngine;
using System.IO;

public static class Saver
{
    private static string FILENAME = "Save.dat";
    public static GameSaveData Load()
    {
        string path = Application.persistentDataPath + FILENAME;
    GameSaveData state;
        if (!File.Exists(path))
            return null;
        using (var stream = File.Open(path, FileMode.Open))
        {
            SharpSerializer reader = new SharpSerializer(true);
            state = reader.Deserialize(stream) as GameSaveData;
        }
        return state;
    }

    public static void Save(GameSaveData state)
    {
        string path = Application.persistentDataPath + FILENAME;

        if (File.Exists(path))
            File.Delete(path);
        using (var stream = File.Open(path, FileMode.CreateNew))
        {
            SharpSerializer writer = new SharpSerializer(true);
            writer.Serialize(state, stream);
        }
    }

}
