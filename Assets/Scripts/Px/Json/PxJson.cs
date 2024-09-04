using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class PxJson 
{
    public static void SaveToJson<T>(T obj) where T : PxJsonData
    {
        string json = obj.ToJson();
        if (!Directory.Exists(Application.persistentDataPath + "/" + obj.SaveFolder))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + obj.SaveFolder);            
        }        
        File.WriteAllText(Application.persistentDataPath + "/" + obj.FilePath + ".json", json);
    }
    public static T LoadFromJson<T>(string filePath)
    {
        if (!File.Exists(Application.persistentDataPath + "/" + filePath + ".json"))
        {
            Debug.Log("File doesn't exist");
            return default;
        }
        string jsonData = File.ReadAllText(Application.persistentDataPath + "/" + filePath + ".json");
        if(jsonData != null)
        {
            Debug.Log($"Loading {filePath}.json");
            return FromJson<T>(jsonData);
        }
        return default;
    }
    public static string ToJson(this object obj) => JsonUtility.ToJson(obj);
    public static T FromJson<T>(string json) => JsonUtility.FromJson<T>(json);
}