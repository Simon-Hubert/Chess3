using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMovement : MonoBehaviour, IMovementBrain
{
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
            return PathFinding.Path(gridManager, thisPiece, king);
        }
        else
        {
            turnWaiting--;
            return pos;
        }
    }
}
