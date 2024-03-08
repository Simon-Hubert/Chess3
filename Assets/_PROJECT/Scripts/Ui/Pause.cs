using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public GameObject _settingsMenu;
    public GameObject _settingsOnButton;

    void Start()
    {
        _settingsMenu.SetActive(false);
        _settingsOnButton.SetActive(true);


    }

    private void Reset()
    {
        if (_settingsOnButton == null)
        {
            _settingsOnButton = GameObject.Find("SettingsOnBtn");
        }

        if (_settingsMenu == null)
        {
            _settingsMenu = GameObject.Find("SettingsMenu");
        }
    }

    public void PauseOn()
    {
        if (!_settingsMenu.activeSelf)
        {
            _settingsOnButton.SetActive(false);
            _settingsMenu.SetActive(true);
            Time.timeScale = 0f;

        }

    }

    public void PauseOff()
    {
        if (_settingsMenu.activeSelf)// si le menu est activ� on le d�sac
        {
            _settingsOnButton.SetActive(true);
            _settingsMenu.SetActive(false);
            Time.timeScale = 1f;

        }

    }
}
