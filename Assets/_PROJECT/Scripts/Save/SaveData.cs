using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

public class SaveData: MonoBehaviour
{
    public static SaveData instance;
    int _level = 1;
    [SerializeField] List<UnlockButton> buttons = new List<UnlockButton>();
    SceneController sc;
    public int Level { get => _level; set => _level = value; }
    private void OnEnable()
    {
        sc = GetComponent<SceneController>();
        sc.OnLoadSelect += SetListButton;
    }
    private void OnDisable()
    {
        sc.OnLoadSelect -= SetListButton;
    }
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
    public void SetListButton(GameObject parentB)
    {
        buttons.Clear();
        foreach(Button button in parentB.GetComponentsInChildren<Button>())
        {
            if(button.name.StartsWith("Level"))
            {
                buttons.Add(button.GetComponent<UnlockButton>());
            }
        }
    }
}
