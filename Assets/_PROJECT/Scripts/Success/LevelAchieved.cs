using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAchieved : MonoBehaviour
{
    [SerializeField] string _successID;
    [SerializeField] PartyManager _pM;
    private void OnEnable()
    {
        _pM.onWin += SetSuccess;
    }
    private void OnDisable()
    {
        _pM.onWin -= SetSuccess;
    }
    void SetSuccess()
    {
        PlayGamesPlatform.Instance.ReportProgress(_successID, 100.0, (bool success) => { if (success) Debug.Log("succès débloqué !"); });
    }
}
