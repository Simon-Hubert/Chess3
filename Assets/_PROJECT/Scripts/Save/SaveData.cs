using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class SaveData
{
    public static SaveData instance;
    int _level = 1;

    public int Level { get => _level; set => _level = value; }

    private void Start()
    {
       if(PlayerPrefs.GetInt("Level") != 0)
        {
            Level = PlayerPrefs.GetInt("Level");
            Debug.Log("You have unlocked " + Level +" levels");
        }
        Debug.LogWarning("There is no Level Stored !");
    }
    [Button]
    public void Saving()
    {
        Level++;
        PlayerPrefs.SetInt("Level", Level);
    }
}
