using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSonDataService : IDataServices
{
    public bool SaveData<T>(string Path, T Data, bool Encrypted)
    {
        string path = Application.persistentDataPath + Path;
        try
        {
            if(File.Exists (path))
            {
                Debug.Log("Data exists. Deleting old file and writing a new one !");
                File.Delete(path);
            }
            else
            {
                Debug.Log("Creating file for the first time! ");
            }
            using FileStream stream = File.Create(path);
            stream.Close(); // il faut fermer le stream après avoir créer le nouveaux fichier si on veut écrirer à l'intérieur
            File.WriteAllText(path, JsonConvert.SerializeObject(Data));
            return true;
        }
        catch(Exception e)
        {
            Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
            return false;
        }
    }

    public T LoadData<T>(string Path, bool Encrypted)
    {
        string path = Application.persistentDataPath + Path;
        if(!File.Exists (path))
        {
            Debug.LogError($"Cannot load file at {path}. File does not exist !");
            throw new FileNotFoundException($"{path} does not exist!");
        }
        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
            throw e;
        }
    }
}
