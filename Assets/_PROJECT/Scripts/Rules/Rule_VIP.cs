using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Rule_VIP : IRules
{
    [SerializeField] List<Piece> BlackList = new List<Piece>();
    [SerializeField] Piece RoiBlanc;

    public bool IsLost()
    {
        return RoiBlanc == null; // || RoiBlanc.Data.ECHEC;
    }

    public void UpdateBlackList()
    {
        Piece[] allObj;
        allObj = UnityEngine.Object.FindObjectsOfType<Piece>();
        BlackList.Clear();
        foreach (Piece piece in allObj)
        {
            if (!piece.Data.IsWhite && piece.enabled) BlackList.Add(piece);
        }
    }
    public bool IsWon()
    {
        return BlackList.Count < 1;
    } 

}
