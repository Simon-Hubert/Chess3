using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMSceneCont : MonoBehaviour
{
    [SerializeField] private Animator _transitionAnim;

    [SerializeField] private Animator _animPlay;
    [SerializeField] private Animator _animSelect;
    [SerializeField] private Animator _animQuit;

    public void Play()
    {
        AudioManager.Instance.PlaySfx("Confirmer");
        StartCoroutine(LoadLevelRoutine(SceneManager.GetActiveScene().buildIndex + 2));//Loads level1
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

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevelRoutine());
    }

    IEnumerator LoadLevelRoutine()
    {
        AudioManager.Instance.PlaySfx("Confirmer");
        _transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        _transitionAnim.SetTrigger("End");
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
