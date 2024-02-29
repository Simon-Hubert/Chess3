using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);


    }

    public void PauseOn()
    {
        if (!pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;

        }

    }

    public void PauseOff()
    {
        if (pauseMenu.activeSelf)// si le menu est activé on le désac
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;

        }

    }
}
