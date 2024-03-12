using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using System;

public class SaveData: MonoBehaviour
{
    struct levelData
    {
        public int stars;
        public bool isUnlocked;
    }
    public static SaveData instance;
    public IDataServices services = new JSonDataService();
    List<UnlockButton> buttons = new List<UnlockButton>();
    levelData[] levelsData = new levelData[20];
    SceneController sc;

    private void OnEnable()
    {
        sc = GetComponent<SceneController>();
        sc.OnLoadSelect += SetData;
    }
    private void OnDisable()
    {
        sc.OnLoadSelect -= SetData;
    }
    [Button]
    public void UpdateLEVEL(int level, int stars)
    {
        levelsData[level].isUnlocked = true;
        levelsData[level].stars = stars;
        Save();
    }
    public void Save()
    {
        services.SaveData("/levelsData.json", levelsData, false);
    }
    public void SetData(GameObject parentB)
    {
        buttons.Clear();
        foreach(Button button in parentB.GetComponentsInChildren<Button>())
        {
            if(button.name.StartsWith("Level"))
            {
                buttons.Add(button.GetComponent<UnlockButton>());
            }
        }
        for(int i=0; i<buttons.Count; i++)
        {
            levelData data = levelsData[i];
            data.isUnlocked = buttons[i].IsUnlocked;
        }
    }
    public void Load()
    {
        try
        {
            levelsData = services.LoadData<levelData[]>("/levelsData.json", false);
            for (int i = 0; i < buttons.Count; i++)
            {
                levelData data = levelsData[i];
                //buttons[i].IsUnlocked = data.isUnlocked;
                if (data.isUnlocked) buttons[i].Unlocking();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Could not read file !");
        }
    }
}
