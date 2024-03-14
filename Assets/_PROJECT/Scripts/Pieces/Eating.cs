using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    Piece piece;
    PieceData pieceSave;

    AudioManager audioManager;
    public static event Action<Piece> OnEat;
    private void Awake()
    {
        piece = GetComponentInParent<Piece>();
        audioManager = FindObjectOfType<AudioManager>();
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
                this.Destroy();
            }
            OnEat?.Invoke(pieceTarget);
            audioManager?.PlaySfx("Eat");
            return true;
        }
        else
        {
            if (piece.Data.CanFuse || pieceTarget.Data.CanFuse)
            {
                Fuse.Fusing(pieceTarget, piece, pieceSave);
                audioManager?.PlaySfx("Fusion");
            }
            return false;
        }
        // CHANGE LE SPRITE
    }
}
