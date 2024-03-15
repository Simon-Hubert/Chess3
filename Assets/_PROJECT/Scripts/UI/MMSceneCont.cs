using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMSceneCont : MonoBehaviour
{
    [SerializeField] private Animator _transitionAnim;
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
}
