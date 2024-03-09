using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Animator _transitionAnim;
    [SerializeField] GameObject childToPreserve;
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            childToPreserve = GameObject.Find("CircleWipeTransition");
            childToPreserve.transform.SetParent(null); // Détache l'enfant
            DontDestroyOnLoad(childToPreserve);
            //Si ça marche pas c que le script getButton récupere pas le sceneController
        }

        //GetComponent<Button>().onClick.AddListener(instance.LoadNextLevel);

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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void LoadLevel(string nameScene)
    {
       
        SceneManager.LoadScene(nameScene);
    }

}