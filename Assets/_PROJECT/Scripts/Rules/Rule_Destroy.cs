using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Rule_Destroy : IRules
{
    List<Piece> allPieces = new List<Piece>();
    
    

    public bool IsLost(GameObject King)
    {
        return !King.GetComponent<Piece>().IsAlive; // || RoiBlanc.Data.ECHEC;
    }

    public bool IsWon()
    {
        return allPieces.Count < 1;
    }

    public void UpdateList(Piece target = null)
    {
        Piece[] allObj;
        allObj = UnityEngine.Object.FindObjectsOfType<Piece>();
        allPieces.Clear();
        foreach (Piece piece in allObj)
        {
            if (!piece.Data.IsWhite && piece.IsAlive) allPieces.Add(piece);
        }
    }

}
