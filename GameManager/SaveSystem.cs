using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "SaveData.ns");
        FileStream fs = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData();

        formatter.Serialize(fs, data);
        Debug.Log("Game saved");
        fs.Close();
    }

    public static SaveData Load()
    {
        string path = Path.Combine(Application.persistentDataPath, "SaveData.ns");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            if (fs.Length != 0)
            {
                SaveData data = formatter.Deserialize(fs) as SaveData;
                fs.Close();

                PlayerStats.instance.loadedFile = true;
                return data;
            }
            else
            {
                Debug.Log("Empty save file: " + path);
                PlayerStats.instance.loadedFile = false;
                return null;
            }
        }
        else
        {
            Debug.Log("Error: File not found in: " + path);
            // no game loaded, start new game
            PlayerStats.instance.loadedFile = false;
            return null;
        }
    }
}
