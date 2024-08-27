using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public static class SafeSystem
{
    private static string GetMapFilePath(string mapName)
    {
        return Application.persistentDataPath + "/" + mapName + ".sav";
    }

    // Method to save player data for a specific map
    public static void SaveMapData(string mapName, int health, int money, List<TurretData> turrets)
    {
        string path = GetMapFilePath(mapName);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                MapData mapData = new MapData(mapName, health, money, turrets);
                formatter.Serialize(stream, mapData);
            }
        }
        catch (IOException ex)
        {
            Debug.LogError("Failed to save map data: " + ex.Message);
        }
    }

    // Method to load player data for a specific map
    public static MapData LoadMapData(string mapName)
    {
        string path = GetMapFilePath(mapName);

        if (File.Exists(path))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    MapData mapData = formatter.Deserialize(stream) as MapData;
                    return mapData;
                }
            }
            catch (IOException ex)
            {
                Debug.LogError("Failed to load map data: " + ex.Message);
                return null;
            }
        }
        else
        {   
            Debug.LogWarning("Map save file not found for " + mapName);
            return null;
        }
    }

    // Method to delete player data for a specific map
    public static void DeleteMapData(string mapName)
    {
        string path = GetMapFilePath(mapName);
        if (File.Exists(path))
        {
            try
            {
                File.Delete(path);
                Debug.Log("Deleted save file for " + mapName);
            }
            catch (IOException ex)
            {
                Debug.LogError("Failed to delete map data: " + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("Save file not found for " + mapName);
        }
    }
}
