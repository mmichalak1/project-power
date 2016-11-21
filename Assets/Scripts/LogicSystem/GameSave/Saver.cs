using Polenter.Serialization;
using UnityEngine;
#if NETFX_CORE
using Windows.Storage;
#else
using System.IO;
#endif
public static class Saver
{
    private static string FILENAME = "Save.dat";
#if NETFX_CORE
        public static GameSaveData Load()
    {
    return null;
    }
#else
    public static GameSaveData Load()
    {
        GameSaveData state;
        string path = Application.persistentDataPath + FILENAME;
        if (!File.Exists(path))
            return null;
        using (var stream = File.Open(path, FileMode.Open))
        {
            SharpSerializer reader = new SharpSerializer(true);
            state = reader.Deserialize(stream) as GameSaveData;
        }
        return state;
    }
#endif



#if NETFX_CORE
    public static void Save(GameSaveData state)
    {
    }
#else
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
#endif


}
