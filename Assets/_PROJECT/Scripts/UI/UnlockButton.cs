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
    }

    public void Unlocking(int stars)
    {
        IsUnlocked = true;
        GetComponent<Button>().enabled = IsUnlocked;
        GetComponent<Image>().sprite = tamponUnlocked;
        if (stars == 3) SetGold();
    }
    public void SetGold()
    {
        GetComponent<Image>().sprite = tamponGold;
        Debug.Log("de l'or et des pailettes");
    }
}
