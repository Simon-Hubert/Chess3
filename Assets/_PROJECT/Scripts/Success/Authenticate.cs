using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UIElements;

public class Authenticate : MonoBehaviour
{
    [SerializeField] SpriteRenderer square;
    public static bool Connected { get; private set; }
    private void Start()
    {
        square.color = Color.black;
        //PlayGamesPlatform.Activate();
        //PlayGamesPlatform.Instance.Authenticate(Connect);
        if (!Connected)
            SignIn();
    }

    public void SignIn()
    {
        if (!PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.Authenticate((result) =>
            {
                if (square.color != null)
                    square.color = Color.yellow;

                if (result == SignInStatus.Success)
                {
                    Connected = true;
                    square.color = Color.green;
                }
            });
        }
        else
        {
            square.color = Color.red;
        }
    }
    void Connect(SignInStatus state)
    {
        square.color = Color.yellow;
        if(state == SignInStatus.Success)
        {
            Debug.Log("connecté");
            square.color = Color.green;
            //SceneManager.LoadScene(1);
        }
        else
        {
            PlayGamesPlatform.Instance.ManuallyAuthenticate(Connect);
            square.color = Color.red;
        }
    }
}
