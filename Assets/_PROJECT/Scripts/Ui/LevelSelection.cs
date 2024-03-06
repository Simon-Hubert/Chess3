using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{


    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

}