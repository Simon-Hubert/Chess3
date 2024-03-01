using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    Piece piece;
    PieceData pieceSave;
    public static event Action<Piece> OnEat;
    private void Awake()
    {
        piece = GetComponentInParent<Piece>();
        pieceSave = piece.Data;
    }

    public bool EatinG(Piece pieceTarget)
    {
        if (piece.Data.IsWhite != pieceTarget.Data.IsWhite)
        {
            OnEat?.Invoke(pieceTarget);
            pieceTarget.gameObject.SetActive(false);
            return true;
        }
        else
        {
            Fuse.Fusing(pieceTarget, piece, pieceSave);
            return false;
        }
        // CHANGE LE SPRITE
    }
}
