using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse
{
    public static event Action<Piece> OnFuse;
    public static void Fusing(Piece pieceTarget, Piece piece, PieceData pieceSave)
    {
        if (piece.Data.Name != "Roi blanc") return;
        if(piece.Data.Level == 0)
        {
            for (int i = 0; i < pieceTarget.Data.Pattern.Count; i++)
            {
                var m = pieceTarget.Data.Pattern[i];
                piece.Data.Pattern.Add(m);
            }
            Debug.Log("Fusion with " + pieceTarget.Data.Name);
            pieceTarget.gameObject.SetActive(false);
            piece.Data.Level++;
            OnFuse?.Invoke(pieceTarget);
        }
        else
        {
            Defusing(piece, pieceSave);
            Fusing(pieceTarget, piece, pieceSave);
        }

    }
    public static void Defusing(Piece piece, PieceData pieceSave)
    {
        piece.Data.Pattern.Clear();
        for (int i = 0; i < pieceSave.Pattern.Count; i++)
        {
            var m = pieceSave.PatternSave[i];
            piece.Data.Pattern.Add(m);
        }

        piece.Data.Level--;
        // CHANGE LE SPRITE
    }
}
