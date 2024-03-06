using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public void Play()
    {
        

    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("LevelSelection");
    }


    public void Quit()
    {
#if UNITY_EDITOR //dans l'editeur
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();// en jeu
#endif
    }
}
