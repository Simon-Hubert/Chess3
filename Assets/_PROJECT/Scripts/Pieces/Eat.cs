using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    Piece piece;
    PieceData pieceSave;
    public static event Action<Piece> OnEat;
    private void Awake()
    {
        piece = GetComponentInParent<Piece>();
        pieceSave = piece.Data;
    }

    public bool Eating (Piece pieceTarget)
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
