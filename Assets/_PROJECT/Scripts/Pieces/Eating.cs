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
            pieceTarget.Destroy();
            if(piece.Data.IsWhite && !piece.Data.CanFuse)
            {
                this.piece.Destroy();
            }
            OnEat?.Invoke(pieceTarget);
            AudioManager.Instance.PlaySfx("Eat");
            return true;
        }
        else
        {
            if (piece.Data.CanFuse || pieceTarget.Data.CanFuse)
            {
                Fuse.Fusing(pieceTarget, piece, pieceSave);
                AudioManager.Instance?.PlaySfx("Fusion2");
            }
            return false;
        }
        // CHANGE LE SPRITE
    }
}
