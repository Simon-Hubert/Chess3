using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tuto : MonoBehaviour
{
    public GameObject tutoScreen;


    public void Start()
    {
        tutoScreen.SetActive(false);
    }
    public void ShowTuto()
    {
        if (!tutoScreen.activeSelf)
        {
            tutoScreen.SetActive(true);

        }
    }

    public void HideTuto()
    {
        if (tutoScreen.activeSelf)
        {
            tutoScreen.SetActive(false);

        }
    }
}
