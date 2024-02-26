using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class SaveData : MonoBehaviour
{
    public static SaveData instance;
    int _level = 1;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
       if(PlayerPrefs.GetInt("Level") != 0)
        {
            _level = PlayerPrefs.GetInt("Level");
            Debug.Log("You have unlocked " + _level +" levels");
        }
        Debug.LogWarning("There is no Level Stored !");
    }
    [Button]
    public void Saving()
    {
        _level++;
        PlayerPrefs.SetInt("Level", _level);
    }
}
