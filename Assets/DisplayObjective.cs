using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayObjective : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [Range(0,5)] public int _displayTime = 3;
    private float elapsedTime = 0f;

    private void Awake()
    {
        StartCoroutine(ShowObjective());
    }

    private IEnumerator ShowObjective()
    {
        _panel.SetActive(true);
        yield return new WaitForSeconds(3);
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
        Debug.Log("Secondes écoulées: " + elapsedTime);
    }

}
