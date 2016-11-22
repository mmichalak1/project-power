using Polenter.Serialization;
using System.IO;
using UnityEngine;
#if NETFX_CORE
using Windows.Storage;
using System.Threading.Tasks;
#endif
public static class Saver
{
    private static string FILENAME = "Save.dat";
#if NETFX_CORE
    public static GameSaveData Load()
    {
        GameSaveData state;
        var storageFolder = ApplicationData.Current.LocalFolder;
        var file = storageFolder.GetFileAsync(FILENAME).GetResults();
        if (file == null)
            return null;
        var stream = file.OpenAsync(FileAccessMode.ReadWrite).GetResults();
        using (System.IO.Stream inputStream = stream.AsStream())
        {
            SharpSerializer reader = new SharpSerializer(true);
            state = reader.Deserialize(inputStream);
        }
        return state;
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
        var folder = ApplicationData.Current.LocalFolder;
        StorageFile file = folder.CreateFileAsync(FILENAME, CreationCollisionOption.ReplaceExisting).GetResults();
        var stream = file.OpenAsync(FileAccessMode.ReadWrite).GetResults();
        using (var inputStream = stream.AsStream())
        {
            SharpSerializer writer = new SharpSerializer(true);
            writer.Serialize(state, inputStream);
        }
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
