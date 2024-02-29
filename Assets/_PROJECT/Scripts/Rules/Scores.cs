using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scores : MonoBehaviour
{
    public enum STARS
    {
        One, Two, Three
    }
    [SerializeField, Range(1, 20)] int turnPlayedFor2Stars = 1;
    [SerializeField, Range(1, 20)] int turnPlayedFor3Stars = 1;
    STARS currentScore;
    public STARS SetStars(int playerCounter)
    {
        if(playerCounter < turnPlayedFor2Stars) currentScore = STARS.One;
        else if(playerCounter >= turnPlayedFor2Stars && playerCounter < turnPlayedFor3Stars) currentScore = STARS.Two;
        else if(playerCounter >= turnPlayedFor3Stars) currentScore= STARS.Three;
        return currentScore;
    }
}