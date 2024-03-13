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


    public void LoadLevel(string nameScene)
    {
        AudioManager.Instance.GetComponent<AudioSource>();

        switch (nameScene)
        {
            case "MainMenu":
                Debug.Log("Mainmenu music");
                AudioManager.Instance.PlayMusic("MainMenuMusic");
                break;
            case "Level":
                Debug.Log("Level music");
                AudioManager.Instance.PlayMusic("LevelMusic");
                break;
            case "LevelSelection":
                AudioManager.Instance.PlayMusic("LevelSelectionMusic");
                break;
            case "Victory":
                AudioManager.Instance.PlayMusic("Victory");
                break;
            case "Defeat":
                AudioManager.Instance.PlayMusic("Defeat");
                break;
            default:
                break;
        }
        StartCoroutine(LoadLevelRoutine(nameScene));
    }

    IEnumerator LoadLevelRoutine(string levelName)
    {
        _transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(levelName);
        _transitionAnim.SetTrigger("End");
    }

   

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.PlayMusic("MainMenuMusic");

    }

    

}