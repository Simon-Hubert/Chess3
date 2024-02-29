using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Rule_Destroy : IRules
{
    List<Piece> allPieces = new List<Piece>();
    [SerializeField] Piece RoiBlanc;
    

    public bool IsLost()
    {
        return RoiBlanc == null; // || RoiBlanc.Data.ECHEC;
    }

    public bool IsWon()
    {
        return allPieces.Count < 1;
    }

    public void UpdateList()
    {
        Piece[] allObj;
        allObj = UnityEngine.Object.FindObjectsOfType<Piece>();
        allPieces.Clear();
        foreach (Piece piece in allObj)
        {
            if(!piece.Data.IsWhite && piece.enabled) allPieces.Add(piece);
        }
    }

}
