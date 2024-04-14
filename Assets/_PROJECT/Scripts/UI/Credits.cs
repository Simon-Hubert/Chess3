using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject _creditsPanel;
    [SerializeField] GameObject _creditsOffBtn;
    [SerializeField] Animator _creditAnim;

    private void Start()
    {
        _creditsPanel.SetActive(false);
        _creditsOffBtn.SetActive(false);
    }

    public void ShowCredits()
    {
        _creditsPanel.SetActive(true);
        _creditsOffBtn.SetActive(true);
        _creditAnim.SetTrigger("Start");
        AudioManager.Instance.PlaySfx("Confirmer");
    }

    public void HideCredits()
    {
        _creditsPanel.SetActive(false);
        _creditsOffBtn.SetActive(false);
        AudioManager.Instance.PlaySfx("Retour");
    }
}
