using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMovement : MonoBehaviour, IMovementBrain
{
    bool playTurn = false;
    Piece thisPiece;
    [SerializeField] Piece king;

    private void OnEnable() {
        thisPiece = GetComponentInParent<Piece>();
    }

    public Tile GetTargetMovement(GridManager gridManager, Tile pos)
    {
        playTurn = !playTurn;
        if(playTurn){
            return PathFinding.Path(gridManager, thisPiece, king);
        }
        else{
            return pos;
        }
    }
}
