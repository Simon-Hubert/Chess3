using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMovement : MonoBehaviour, IMovementBrain
{
    bool playTurn = true;
    Piece thisPiece;

    private void OnEnable() {
        thisPiece = GetComponentInParent<Piece>();
    }

    public Tile GetTargetMovement(GridManager gridManager, Tile pos)
    {
        if(playTurn){
            //return Pathfinding.Path()
        }
        else{
            return pos;
        }
        return pos;
    }
}
