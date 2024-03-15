using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using System;
using GooglePlayGames;
using TMPro;

public class SaveData: MonoBehaviour
{
    struct levelData
    {
        public int stars;
        public bool isUnlocked;
    }
    int tStars = 0;
    public static SaveData instance;
    public IDataServices services = new JSonDataService();
    [SerializeField] List<UnlockButton> buttons = new List<UnlockButton>();
    levelData[] levelsData = new levelData[20];
    [SerializeField] SceneController sc;
    [SerializeField] TextMeshProUGUI starsT;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void OnEnable()
    {
        sc = GetComponent<SceneController>();
        sc.OnLoadSelect += SetData;
        sc.OnLoadSelect += Load;
    }
    private void OnDisable()
    {
        sc.OnLoadSelect -= SetData;
        sc.OnLoadSelect -= Load;
    }
    //[Button]
    public void UpdateLEVEL(int level, int stars)
    {
        Debug.Log(level);
        levelsData[level + 1].isUnlocked = true;
        levelsData[level].stars = stars;
        tStars = 0;
        foreach(levelData levelData in levelsData)
        {
            tStars += levelData.stars;
        }
        PlayGamesPlatform.Instance.ReportProgress("Cgkli7nLgfQPEAIQAA", (tStars / 10) * 100, (bool success) => { if (success) Debug.Log("succ�s d�bloqu� !"); });
        PlayGamesPlatform.Instance.ReportProgress("Cgkli7nLgfQPEAIQAQ", (tStars / 20) * 100, (bool success) => { if (success) Debug.Log("succ�s d�bloqu� !"); });
        PlayGamesPlatform.Instance.ReportProgress("Cgkli7nLgfQPEAIQAg", (tStars / 30) * 100, (bool success) => { if (success) Debug.Log("succ�s d�bloqu� !"); });
        PlayGamesPlatform.Instance.ReportProgress("Cgkli7nLgfQPEAIQAW", (tStars / 40) * 100, (bool success) => { if (success) Debug.Log("succ�s d�bloqu� !"); });
        PlayGamesPlatform.Instance.ReportProgress("Cgkli7nLgfQPEAIQBA", (tStars / 50) * 100, (bool success) => { if (success) Debug.Log("succ�s d�bloqu� !"); });
        PlayGamesPlatform.Instance.ReportProgress("Cgkli7nLgfQPEAIQBQ", (tStars / 60) * 100, (bool success) => { if (success) Debug.Log("succ�s d�bloqu� !"); });
        Save();
    }
    public void Save()
    {
        levelsData[0].isUnlocked = true;
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
    public void Load(GameObject balek = null)
    {
        Debug.Log("on a load en sah");
        starsT = GameObject.Find("NSTars").GetComponent<TextMeshProUGUI>();
        starsT.text = tStars.ToString();
        try
        {
            levelsData = services.LoadData<levelData[]>("/levelsData.json", false);
            for (int i = 0; i < buttons.Count; i++)
            {
                levelData data = levelsData[i];
                //buttons[i].IsUnlocked = data.isUnlocked;
                if (data.isUnlocked)
                {
                    buttons[i].Unlocking(data.stars);
                }

            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Could not read file !");
        }
    }
}
