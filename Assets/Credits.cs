using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject _creditsPanel;
    [SerializeField] GameObject _creditsOffBtn;

    private void Awake()
    {
        _creditsPanel.SetActive(false);
        _creditsOffBtn.SetActive(false);
    }

    public void ShowCredits()
    {
        _creditsPanel.SetActive(true);
        _creditsOffBtn.SetActive(true);
    }

    public void HideCredits()
    {
        _creditsPanel.SetActive(false);
        _creditsOffBtn.SetActive(false);
    }
}
