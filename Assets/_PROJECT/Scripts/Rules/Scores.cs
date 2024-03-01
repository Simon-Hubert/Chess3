using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scores : MonoBehaviour
{
    [SerializeField, Range(1, 20)] int turnPlayedFor2Stars = 1;
    [SerializeField, Range(1, 20)] int turnPlayedFor3Stars = 1;
    public int SetStars(int playerCounter)
    {
        if(playerCounter > turnPlayedFor2Stars) return 1;
        if(playerCounter <= turnPlayedFor3Stars)  return 3;
        return 2;
    }
}