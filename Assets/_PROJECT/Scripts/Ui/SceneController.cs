using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Animator _transitionAnim;
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #region MAINMENU
    /*public void Play()
    {
        SceneManager.LoadScene("Level");

    }*/

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
    #endregion

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    }

    IEnumerator LoadLevel(int levelIndex)
    {
        _transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(levelIndex);
        _transitionAnim.SetTrigger("End");
    }

}