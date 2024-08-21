using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SafeSystem
{
    public static void SavePlayerData(GameManager gameManager)
    {
        string path = Application.persistentDataPath + "/player.sav";

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                PlayerData data = new PlayerData(gameManager);
                formatter.Serialize(stream, data);
            }
        }
        catch (IOException ex)
        {
            Debug.LogError("Failed to save player data: " + ex.Message);
        }
    }

    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/player.sav";
        if (File.Exists(path))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    PlayerData data = formatter.Deserialize(stream) as PlayerData;
                    return data;
                }
            }
            catch (IOException ex)
            {
                Debug.LogError("Failed to load player data: " + ex.Message);
                return null;
            }
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            return null;
        }
    }

    public static void DeletePlayerData()
    {
        string path = Application.persistentDataPath + "/player.sav";
        if (File.Exists(path))
        {
            try
            {
                File.Delete(path);
            }
            catch (IOException ex)
            {
                Debug.LogError("Failed to delete player data: " + ex.Message);
            }
        }
    }
}
