using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockButton : MonoBehaviour
{
     [SerializeField]    bool isUnlocked = false;

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
    }
}
