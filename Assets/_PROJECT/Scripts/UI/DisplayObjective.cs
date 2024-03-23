using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayObjective : MonoBehaviour
{

    public static DisplayObjective instance;
    [SerializeField] GameObject _panel;
    [Range(0,5)] public float _displayTime = 3f;
    private float elapsedTime = 0f;
    [SerializeField] TextMeshProUGUI _objectiveText;
    [SerializeField] TextMeshProUGUI _pauseText;

    private void Start()
    {
        StartCoroutine(ShowObjective());
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ChangeText;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ChangeText;
    }

    public IEnumerator ShowObjective()
    {
        Debug.Log("start coroutine");
        _panel.SetActive(true);
        yield return new WaitForSeconds(_displayTime);
        _panel.SetActive(false);

    }

    private void OnValidate()
    {
        if (_displayTime == 0)
        {
            Debug.LogWarning("Display time cannot be 0.");
            _displayTime = 3;
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        //Debug.Log("Secondes écoulées: " + elapsedTime);
    }

    public void ChangeText(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "level1":
            case "level2":
            case "level3":
            case "level6":
            case "level7":
            case "level8":
            case "level11":
            case "level12":
            case "level13":
            case "level16":
            case "level17":
            case "level18":
                _objectiveText.text = "Vaincre tout les ennemis";
                _pauseText.text = "Vaincre tout les ennemis";
                break;

            case "level5":
            case "level10":
            case "level15":
                _objectiveText.text = "Trouver la sortie";
                _pauseText.text = "Trouver la sortie";
                break;

            case "level4":
            case "level9":
            case "level14":
            case "level19":
                _objectiveText.text = "Eliminer le Roi ennemi";
                _pauseText.text = "Eliminer le Roi ennemi";
                break;

            case "level20":
                _objectiveText.text = "Eliminer la Reine";
                _pauseText.text = "Eliminer la Reine";
                break;

        }
    }

   

}
