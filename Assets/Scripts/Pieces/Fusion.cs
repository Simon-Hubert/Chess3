using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion : MonoBehaviour
{
    Piece piece;
    private void Awake()
    {
        piece = GetComponent<Piece>();
    }

    void Fuse (Piece pieceTarget)
    {
        piece.Data.Pattern += pieceTarget.Data.Pattern;
    }
}
