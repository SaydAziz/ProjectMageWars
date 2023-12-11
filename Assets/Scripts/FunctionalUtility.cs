using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public static class FunctionalUtility
{
    public static void SetLayerRecursively(this GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetLayerRecursively(layer);
        }
    }

    public static void SetScaleRecursively(this GameObject obj, float scale)
    {
        obj.transform.localScale *= scale;

        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetScaleRecursively(scale);
        }
    }

    public static bool SaveData<T>(string path, T data)
    {
        try
        {
            if (File.Exists(path))
            {
                Debug.Log("Replacing existing data file...");
                File.Delete(path);
            }
            else
            {
                Debug.Log("Creating new data file...");
            }
            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
            return true;
        }
        catch
        {
            Debug.Log("Failed to save...");
            return false;
        }
    }

    public static T LoadData<T>(string path)
    {
        Debug.Log("Loading from data file...");
        T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        return data;

    }


}

