using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovementBrain
{
    Piece thisPiece;

    private void OnEnable() {
        thisPiece = GetComponentInParent<Piece>();
    }

    public Tile GetTargetMovement(GridManager gridManager, Tile pos)
    {

        //se déplace si enemi trouvé
        Tile target = CheckForPiece(gridManager, pos);

        return target;
    }

    private Tile CheckForPiece(GridManager gm, Tile positionActuelle = null){
        List<Tile> highlights = CheckMovements.CheckMove((Vector2Int)thisPiece.Coords, thisPiece.Data.Pattern, gm);
        foreach (Tile tile in highlights)
        {
            Piece p = gm.GetPieceAt(tile.Coords);
            if(p) return tile;
        }
        return positionActuelle;
    }


}

// public enum MovementName{
//         Stay,
//         Patrol,
//         Closest,
//     }

//     [SerializeField] MovementName movement;
//     [SerializeField, ShowIf("movement", MovementName.Patrol)] Tile[] patrols;
//     int counter = 0;

//     public Tile GetTargetMovement(GridManager gridManager, Tile pos)
//     {
//         switch (movement)
//         {
//             default:
//             case MovementName.Stay:
//                 return Stay(gridManager, pos);
//             case MovementName.Patrol:
//                 return Patrol(gridManager,pos);
//             case MovementName.Closest:
//                 return Closest(gridManager,pos);
//         }
//     }

//     public Tile Stay(GridManager gridManager, Tile pos){
//         return pos;
//     }

//     public Tile Patrol(GridManager gridManager, Tile pos){
//         if(patrols.Length == 0){
//             Debug.LogWarning("Patrouille non setup !!!");
//             return pos;
//         }
//         else{
//             Tile t = patrols[counter%patrols.Length];
//             counter ++;
//             return t;
//         }
//     }

//     public Tile Closest(GridManager gridManager, Tile pos){
//         throw new NotImplementedException();
//     }