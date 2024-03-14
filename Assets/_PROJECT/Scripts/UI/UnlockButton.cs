using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockButton : MonoBehaviour
{
     [SerializeField]    bool isUnlocked = false;
    [SerializeField] Sprite tamponUnlocked, tamponGold;

    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }

    private void Start()
    {
        if (!IsUnlocked)
        {
            GetComponent<Button>().enabled = IsUnlocked;
        }
        else
        {
            Unlocking();
        }
    }

    public void Unlocking()
    {
        IsUnlocked = true;
        GetComponent<Button>().enabled = IsUnlocked;
        GetComponent<Image>().sprite = tamponUnlocked;
    }
    public void SetGold()
    {
        GetComponent<Image>().sprite = tamponGold;
    }
}
