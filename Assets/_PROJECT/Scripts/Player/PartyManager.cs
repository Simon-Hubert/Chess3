using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyManager : MonoBehaviour
{
    VictoryScreen vS;
    TurnManager tm;
    [SerializeField] RuleController ruleController;
    [SerializeField] Scores score;
    [SerializeField] GameObject panelVictory, panelLose;
    [SerializeField] GameObject parentPiece;
    [SerializeField] GameObject king;
    public event Action onWin;

    public GameObject PanelVictory { get => panelVictory; }

    private void Start()
    {
        parentPiece = GameObject.Find("PIECES");
        tm = GetComponent<TurnManager>();
        ruleController = GetComponent<RuleController>();
        score = GetComponent<Scores>();
        vS = GetComponent<VictoryScreen>();
        if(tm == null) Debug.LogWarning("il n'y a pas de TurnManager sur le MANAGER");
        if (ruleController == null) Debug.LogWarning("il n'y a pas de RuleController sur le MANAGER");
        if (ruleController == null) Debug.LogWarning("il n'y a pas de Scores sur le MANAGER");
        if (vS == null) Debug.LogWarning("il n'y a pas de VictoryScreen sur le MANAGER");
        king = parentPiece.transform.Find("Roi blanc").gameObject;

        TurnManager.OnTurnEnd += IsGameFinished;
    }

    private void OnDisable()
    {
        TurnManager.OnTurnEnd -= IsGameFinished;
    }
    void IsGameFinished(bool b)
    {
        if (ruleController.GetCurrentRule().IsWon())
        {
            onWin?.Invoke();
            PanelVictory.SetActive(true);
            vS.SetScreen(score.SetStars(tm.PlayerCounter));
            Scene currentScene = SceneManager.GetActiveScene();
            int index = currentScene.buildIndex - 3;
            Debug.Log(SaveData.instance);
            SaveData.instance.UpdateLEVEL(index, score.SetStars(tm.PlayerCounter));
        }

        else if (ruleController.GetCurrentRule().IsLost(king))
        {
            panelLose.SetActive(true);
        }
    }
}
