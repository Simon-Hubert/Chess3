using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Rule_Escape : IRules
{
    [SerializeField] Piece RoiBlanc;

    [SerializeField] Tile Objectif;
    [SerializeField] GridManager tileGrid;

    public bool IsLost(GameObject King)
    {
        return !King.GetComponent<Piece>().IsAlive; // || RoiBlanc.Data.ECHEC;
    }

    public bool IsWon()
    {
        Vector2Int pos = (Vector2Int) tileGrid.transform.GetComponent<Grid>().WorldToCell(RoiBlanc.transform.position);
        return tileGrid.GetTileAt(pos) == Objectif;
    }
}
