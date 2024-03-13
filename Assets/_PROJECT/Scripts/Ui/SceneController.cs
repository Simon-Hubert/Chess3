using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Animator _transitionAnim;
    [SerializeField] GameObject childToPreserve;
    [SerializeField] private GameObject parent;
    public static SceneController instance;
    public event Action<GameObject> OnLoadSelect;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += Set;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= Set;
    }
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

    }

    #region MAINMENU
    /*public void Play()
    {
        SceneManager.LoadScene("Level");

    }*/
    void Set(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "LevelSelection") return;
        parent = FindObjectOfType<HorizontalLayoutGroup>().gameObject;
        OnLoadSelect?.Invoke(parent);
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
    #endregion

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    }

    IEnumerator LoadLevel(int levelIndex)
    {
        _transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
        _transitionAnim.SetTrigger("End");
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