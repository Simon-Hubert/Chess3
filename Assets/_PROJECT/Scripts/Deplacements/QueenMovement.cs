using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMovement : MonoBehaviour, IMovementBrain
{
    bool playTurn = false;
    int turnWaiting = 0;
    Piece thisPiece;
    [SerializeField] Piece king;

    private void OnEnable() {
        thisPiece = GetComponentInParent<Piece>();
    }

    public void WaitForXturns(int x)
    {
        turnWaiting = x;
    }

    public Tile GetTargetMovement(GridManager gridManager, Tile pos)
    {
        if(turnWaiting <= 0)
        {
            playTurn = !playTurn;
            if(playTurn){
                return PathFinding.Path(gridManager, thisPiece, king);
            }
            else{
                return pos;
            }
        }
        else
        {
            turnWaiting--;
            return pos;
        }
    }
}
