using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyManager : MonoBehaviour
{
    VictoryScreen vS;
    TurnManager tm;
    RuleController ruleController;
    Scores score;
    [SerializeField] GameObject panelVictory, panelLose;
    private void Awake()
    {
        tm = GetComponent<TurnManager>();
        ruleController = GetComponent<RuleController>();
        score = GetComponent<Scores>();
        vS = GetComponent<VictoryScreen>();
        if(tm == null) Debug.LogWarning("il n'y a pas de TurnManager sur le MANAGER");
        if (ruleController == null) Debug.LogWarning("il n'y a pas de RuleController sur le MANAGER");
        if (ruleController == null) Debug.LogWarning("il n'y a pas de Scores sur le MANAGER");
        if (vS == null) Debug.LogWarning("il n'y a pas de VictoryScreen sur le MANAGER");
        TurnManager.OnTurnEnd += IsGameFinished;
    }
    void IsGameFinished(bool b)
    {
        if (ruleController.GetCurrentRule().IsWon())
        {
            panelVictory.SetActive(true);
            vS.SetScreen(score.SetStars(tm.PlayerCounter));
            Scene currentScene = SceneManager.GetActiveScene();
            int index = currentScene.buildIndex - 1;
            SaveData.instance.UpdateLEVEL(index, score.SetStars(tm.PlayerCounter));
        }

        else if (ruleController.GetCurrentRule().IsLost())
        {
            panelLose.SetActive(true);
        }
    }
}
