using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockButton : MonoBehaviour
{
     [SerializeField]    bool isUnlocked = false;
    private void Start()
    {
        if (!isUnlocked)
        {
            GetComponent<Button>().enabled = isUnlocked;
        }
        else
        {
            Unlocking();
        }
    }

    public void Unlocking()
    {
        isUnlocked = true;
        GetComponent<Button>().enabled = isUnlocked;
    }
}
