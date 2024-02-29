using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion : MonoBehaviour
{
    Piece piece;
    PieceData pieceSave;
    private void Awake()
    {
        piece = GetComponentInParent<Piece>();
        pieceSave = piece.Data;
    }

    public void Fuse (Piece pieceTarget)
    {
        for (int i = 0; i< pieceTarget.Data.Pattern.Count; i++)
        {
            var m = pieceTarget.Data.Pattern[i];
            piece.Data.Pattern.Add(m);
        }
        Debug.Log("Fusion with " + pieceTarget.Data.Name);
        pieceTarget.gameObject.SetActive(false);

        // CHANGE LE SPRITE
    }

    public void Defuse ()
    {
        piece.Data.Pattern.Clear();
        for (int i = 0; i < pieceSave.Pattern.Count; i++)
        {
            var m = pieceSave.Pattern[i];
            piece.Data.Pattern.Add(m);
        }


        // CHANGE LE SPRITE
    }
}
