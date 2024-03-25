using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayObjective : MonoBehaviour
{

    public static DisplayObjective instance;
    [SerializeField] GameObject _panel;
    [Range(0,5)] public float _displayTime = 3f;
    [SerializeField] TextMeshProUGUI objectif;
    private float elapsedTime = 0f;

    /*private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }*/

    private void Start()
    {
        StartCoroutine(ShowObjective());
        objectif.text = _panel.transform.Find("Text (TMP) (1)").GetComponent<TextMeshProUGUI>().text;
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

   

}
