using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;


public static class DataSaver
{
    public static string PlayerInventoryFileName = "PlayerInventory.json";
    public static string PlayerEquipFileName = "PlayerEquip.json";
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
    
    public static void SaveListToJson <T> (string filePath, List<T> saveList)
    {
        using (StreamWriter fileWriter = new StreamWriter(filePath, false))
        {
            for (int i = 0; i < saveList.Count; i++)
            {
                fileWriter.WriteLine(JsonUtility.ToJson(saveList[i]));
            }
        }
    }

    public static async Task<List<T>> LoadingJsonToList<T>(string filePath) 
    {
        List<T> itemList = new List<T>();
        if (File.Exists(filePath))
        {
            using (StreamReader fileReader = new StreamReader(filePath))
            {
                string line;
                while ((line = await fileReader.ReadLineAsync()) != null)
                {
                    itemList.Add(JsonUtility.FromJson<T>(line));
                }
                
               
            }
        }
        return itemList;
    }
}
