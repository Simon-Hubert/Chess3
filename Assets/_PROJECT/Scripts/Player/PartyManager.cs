using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    TurnManager tm;
    RuleController ruleController;
    Scores score;
    [SerializeField] GameObject panelVictory, panelLose;
    private void Awake()
    {
        tm = GetComponent<TurnManager>();
        ruleController = GetComponent<RuleController>();
        score = GetComponent<Scores>();
        if(tm == null) Debug.LogWarning("il n'y a pas de TurnManager sur le MANAGER");
        if (ruleController == null) Debug.LogWarning("il n'y a pas de RuleController sur le MANAGER");
        if (ruleController == null) Debug.LogWarning("il n'y a pas de Scores sur le MANAGER");
        tm.OnTurnEnd += IsGameFinished;
    }
    void IsGameFinished(bool b)
    {
        if (ruleController.GetCurrentRule().IsWon())
        {
            panelVictory.SetActive(true);
            switch (score.SetStars(tm.PlayerCounter))
            {
                case Scores.STARS.One:
                    break;
                case Scores.STARS.Two:
                    break;
                case Scores.STARS.Three:
                    break;
            }
        }
        else if(ruleController.GetCurrentRule().IsLost())
        {
            panelLose.SetActive(true);
        }
    }
}
