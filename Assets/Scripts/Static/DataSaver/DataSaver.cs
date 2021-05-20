using System.IO;
using UnityEngine;


public static class DataSaver
{
    public static string PlayerInventoryFileName = "PlayerInventory.json";
    public static T LoadingJson<T>(string filePath)
    {
        if (File.Exists(filePath))
            return JsonUtility.FromJson<T>(File.ReadAllText(filePath));
        return default;
    }

    public static void SaveJson(string filePath, object data)
    {
        File.WriteAllText(filePath, JsonUtility.ToJson(data));
    }
}
