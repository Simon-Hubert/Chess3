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
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Set(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "LevelSelection") return;
        parent = FindObjectOfType<HorizontalLayoutGroup>().gameObject;
        OnLoadSelect?.Invoke(parent);
    }
    public void SelectLevel()
    {
        AudioManager.Instance.PlaySfx("Confirmer");
        SceneManager.LoadSceneAsync("LevelSelection");
        parent = FindObjectOfType<HorizontalLayoutGroup>().gameObject;
        OnLoadSelect?.Invoke(parent);
    }

    public static void LoadLevel(string nameScene)
    {
        Debug.Log(instance);
        instance.StartCoroutine(instance.LoadLevelRoutine(0, nameScene));

    }

    IEnumerator LoadLevelRoutine(int levelIndex = 0, string name = null)
    {
        _transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        if(levelIndex == 0)
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
    }
    public void Quit()
    {
        Application.Quit();
    }

}