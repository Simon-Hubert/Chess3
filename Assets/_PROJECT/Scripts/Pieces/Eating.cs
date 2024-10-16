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
        if(pieceTarget == piece) return true;
        if (piece.Data.IsWhite != pieceTarget.Data.IsWhite)
        {
            pieceTarget.gameObject.SetActive(false);
            if(piece.Data.IsWhite && !piece.Data.CanFuse)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
            }
            OnEat?.Invoke(pieceTarget);
            return true;
        }
        else
        {
            if(piece.Data.CanFuse) Fuse.Fusing(pieceTarget, piece, pieceSave);
            return false;
        }
        // CHANGE LE SPRITE
    }
}
