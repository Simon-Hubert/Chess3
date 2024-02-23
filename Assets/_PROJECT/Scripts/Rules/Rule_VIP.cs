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

    public bool IsWon()
    {
        return BlackList.Count < 1;
    } 

}
