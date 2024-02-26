using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovementBrain
{
    public enum MovementName{
        Stay,
        Patrol,
        Closest,
    }

    [SerializeField] MovementName movement;
    [SerializeField, ShowIf("movement", MovementName.Patrol)] Tile[] patrols;
    int counter = 0;

    public Tile GetTargetMovement(GridManager gridManager, Tile pos)
    {
        switch (movement)
        {
            default:
            case MovementName.Stay:
                return Stay(gridManager, pos);
            case MovementName.Patrol:
                return Patrol(gridManager,pos);
            case MovementName.Closest:
                return Closest(gridManager,pos);
        }
    }

    public Tile Stay(GridManager gridManager, Tile pos){
        return pos;
    }

    public Tile Patrol(GridManager gridManager, Tile pos){
        if(patrols.Length == 0){
            Debug.LogWarning("Patrouille non setup !!!");
            return pos;
        }
        else{
            counter ++;
            return patrols[counter%patrols.Length];
        }
    }

    public Tile Closest(GridManager gridManager, Tile pos){
        throw new NotImplementedException();
    }
}
