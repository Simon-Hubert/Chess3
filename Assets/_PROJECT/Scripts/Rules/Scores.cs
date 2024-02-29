using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scores : MonoBehaviour
{
    enum STARS
    {
        One, Two, Three
    }
    [SerializeField, Range(1, 20)] int turnPlayedFor1Star = 1;
    [SerializeField, Range(1, 20)] int turnPlayedFor2Stars = 1;
    [SerializeField, Range(1, 20)] int turnPlayedFor3Stars = 1;
    STARS currentScore;
    TurnManager tm;
    private void Awake()
    {
        tm = GetComponent<TurnManager>();
        if (tm == null) Debug.LogWarning("il n'y a pas de TurnManager sur le MANAGER");

    }

    STARS SetStars()
    {
        return currentScore;
    }
}