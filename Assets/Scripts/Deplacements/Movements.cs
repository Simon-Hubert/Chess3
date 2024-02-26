using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movements : MonoBehaviour
{
    [SerializeField] GridManager gridManager;
    IMovementBrain brain;
    [SerializeField] Tile pos;
    Tile target;
    [SerializeField] bool myturn;

    public bool Myturn { get => myturn; set => myturn = value; }

    private void Awake() {
        brain = GetComponentInChildren<IMovementBrain>();
        MoveToTarget(pos);
    }

    private void OnValidate() {
        // Setup GridManager
        gridManager = FindObjectOfType<GridManager>();

        // Setup pos
        if(gridManager){
            pos = gridManager.GetTileAt(transform.position);
        }
    }

    private void Update() {
        if(Myturn && (brain != null) && (target==null)){
            target = brain.GetTargetMovement(gridManager, pos);
        }

        if(target){
            Debug.Log("Moving to :"+target.Coords);
            MoveToTarget(target);
            Myturn = false;
            target = null;
        }
    }

    private void MoveToTarget(Tile tile)
    {
        transform.position = tile.transform.position;
        pos = tile;
    }
}
