using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Rule_Escape : IRules
{
    [SerializeField] Piece RoiBlanc;
    [SerializeField, Range(0,10)] int TourInactionMax;
    int currentInactions = 0;

    public event Action OnInaction;
    [SerializeField] Tile Objectif;
    [SerializeField] GridManager tileGrid;

    public bool IsLost()
    {
        return RoiBlanc == null; // || RoiBlanc.Data.ECHEC;
    }

    public bool IsWon()
    {
        Vector2Int pos = (Vector2Int) tileGrid.transform.GetComponent<Grid>().WorldToCell(RoiBlanc.transform.position);
        return tileGrid.GetTileAt(pos) == Objectif;
    }
    public void Inacting()
    {
        currentInactions++;
        if (currentInactions >= TourInactionMax)
        {
            OnInaction?.Invoke();
        }
    }

    public void Acting()
    {
        currentInactions = 0;
    }
}
