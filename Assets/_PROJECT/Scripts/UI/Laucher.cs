using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Laucher : MonoBehaviour
{
    [SerializeField] Slider _slider;


    private void Start()
    {
        LoadLauncher();
    }

    public void LoadLauncher()
    {
        StartCoroutine(LoadAsync(SceneManager.GetActiveScene().buildIndex + 1));
        
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        yield return new WaitForSeconds(5);
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);


        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            _slider.value = progress;

            yield return null;
            AudioManager.Instance.PlayMusic("MainMenuMusic");
        }
    }
}
