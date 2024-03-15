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

    public Animator _animatorPlay;
    public Animator _animatorSelect;
    public Animator _animatorQuit;
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
            //childToPreserve = GameObject.Find("CircleWipeTransition");
            childToPreserve.transform.SetParent(null); // Détache l'enfant
            DontDestroyOnLoad(childToPreserve);
            //Si ça marche pas c que le script getButton récupere pas le sceneController

            
        }

    }

    private void Start()
    {
        //_animator = GetComponent<Animator>();

    }
    #region MAINMENU
    void Set(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "LevelSelection") return;
        parent = FindObjectOfType<HorizontalLayoutGroup>().gameObject;
        OnLoadSelect?.Invoke(parent);
    }
    public void SelectLevel()
    {
        _animatorSelect.SetTrigger("Start");
        _animatorSelect.SetTrigger("End");
        AudioManager.Instance.PlaySfx("Confirmer");
        SceneManager.LoadScene("LevelSelection");
        parent = FindObjectOfType<HorizontalLayoutGroup>().gameObject;
        OnLoadSelect?.Invoke(parent);
    }

    public void Back()
    {
        AudioManager.Instance.PlaySfx("Confirmer");
        SceneManager.LoadScene("LevelSelection");
        AudioManager.Instance.PlayMusic("MainMenuMusic");
    }

    public void Quit()
    {
        _animatorQuit.SetTrigger("Start");
        _animatorQuit.SetTrigger("End");
        AudioManager.Instance.PlaySfx("Confirmer");
#if UNITY_EDITOR //dans l'editeur
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();// en jeu
#endif
    }
    #endregion

    public void Play()
    {
        _animatorPlay.SetTrigger("Start");
        _animatorPlay.SetTrigger("End");
        AudioManager.Instance.PlaySfx("Confirmer");
        StartCoroutine(LoadLevelRoutine(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevelRoutine(int levelIndex = 0, string name = null)
    {
        _transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        if (levelIndex == 0)
        {
            SceneManager.LoadScene(name);
        }
        else
        {
            SceneManager.LoadScene(levelIndex);
        }
        _transitionAnim.SetTrigger("End");
    }



    public void Return()
    {
        AudioManager.Instance.PlaySfx("Retour");
        SceneManager.LoadScene("MainMenu");
        //AudioManager.Instance.PlayMusic("MainMenuMusic");
    }

    public static void LoadLevel(string nameScene)
    {
        AudioManager.Instance.PlaySfx("Confirmer");
        Debug.Log(instance);
        SceneManager.LoadScene(nameScene);
        instance.StartCoroutine(instance.LoadLevelRoutine(0, nameScene));


    }
}