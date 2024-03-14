using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Authenticate : MonoBehaviour
{
    [SerializeField] SpriteRenderer square;

    public Text info;
         
    public static bool Connected { get; private set; }
    private void Start()
    {
        square.color = Color.black;
        PlayGamesPlatform.Activate();
        //PlayGamesPlatform.Instance.Authenticate(Connect);

        //if (!Connected)
        PlayGamesPlatform.Instance.Authenticate(SignIn);
    }

    public void SignIn(SignInStatus state)
    {
        //if (!PlayGamesPlatform.Instance.IsAuthenticated())
        square.color = Color.yellow;
        info.text = state.ToString();

        if (state == SignInStatus.Success)
        {
            Connected = true;
            square.color = Color.green;
            SceneManager.LoadScene(1);
        }
        else if (state == SignInStatus.InternalError)
        {
            square.color = Color.red;
        }
        else if (state == SignInStatus.Canceled)
        {
            square.color = Color.cyan;

            PlayGamesPlatform.Instance.ManuallyAuthenticate(SignIn);
        }
    }
    //void Connect(SignInStatus state)
    //{
    //    square.color = Color.yellow;
    //    if(state == SignInStatus.Success)
    //    {
    //        Debug.Log("connecté");
    //        square.color = Color.green;
    //        //SceneManager.LoadScene(1);
    //    }
    //    else
    //    {
    //        PlayGamesPlatform.Instance.ManuallyAuthenticate(Connect);
    //        square.color = Color.red;
    //    }
    //}
}
