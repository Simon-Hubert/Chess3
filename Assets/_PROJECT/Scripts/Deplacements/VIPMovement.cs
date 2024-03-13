using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIPMovement : MonoBehaviour, IMovementBrain
{
    [SerializeField] Tile[] patrol;
    [SerializeField] Piece roiBlanc;
    int step = 0;
    Piece thisPiece;

    private void OnEnable() {
        thisPiece = GetComponentInParent<Piece>();
    }

    public Tile GetTargetMovement(GridManager gridManager, Tile pos)
    {
        Tile target = null;
        target = TakeIfKingInRange(gridManager);
        
        if(!target){
            target = patrol[step%patrol.Length];
            step++;
        } 
        return target;
    }

    private Tile TakeIfKingInRange(GridManager gm)
    {
        List<Tile> highlights = CheckMovements.CheckMove((Vector2Int)thisPiece.Coords, thisPiece.Data.Pattern, gm);
        foreach (Tile tile in highlights)
        {
            Piece p = gm.GetPieceAt(tile.Coords);
            if (p!=null && p== roiBlanc)
            {
                Debug.Log("trouvé le roi");
                return tile;
            }
        }
        return null;
    }
}
