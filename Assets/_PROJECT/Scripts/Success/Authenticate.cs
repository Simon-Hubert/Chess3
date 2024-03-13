using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class Authenticate : MonoBehaviour
{
    private void Start()
    {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(Connect);
    }


    void Connect(SignInStatus state)
    {
        if(state == SignInStatus.Success)
        {
            Debug.Log("connecté");
            SceneManager.LoadScene(1);
        }
        else
        {
            PlayGamesPlatform.Instance.ManuallyAuthenticate(Connect);
        }
    }
}
