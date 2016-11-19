using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public static class Saver
{
    private static string FILENAME = "Save";
    public static GameSave Load()
    {
        string path = Application.persistentDataPath + FILENAME;
        GameSave state;
        if (!File.Exists(path))
            return null;
        using (var stream = File.Open(path, FileMode.Open))
        {
            BinaryFormatter reader = new BinaryFormatter();
            state = reader.Deserialize(stream) as GameSave;
        }
        return state;
    }

    public static void Save(GameSave state)
    {
        string path = Application.persistentDataPath + FILENAME;

        if (File.Exists(path))
            File.Delete(path);
        using (var stream = File.Open(path, FileMode.CreateNew))
        {
            BinaryFormatter writer = new BinaryFormatter();
            writer.Serialize(stream, state);
        }
    }
}
