using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementBrain 
{
    public Tile GetTargetMovement(GridManager gridManager, Tile pos);
}
