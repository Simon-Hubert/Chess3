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
        return !RoiBlanc.gameObject.activeSelf; // || RoiBlanc.Data.ECHEC;
    }

    public void UpdateBlackList(Piece target)
    {
        BlackList.Remove(target);
    }
    public bool IsWon()
    {
        return BlackList.Count < 1;
    } 

}
