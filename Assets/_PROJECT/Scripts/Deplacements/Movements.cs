using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Movements : MonoBehaviour
{
    [SerializeField] GridManager gridManager;
    [SerializeField] Tile pos;
    [SerializeField] bool myturn;
    IMovementBrain brain;
    TurnManager turnManager;
    Piece thisPiece;

    Tile target;

    public static event Action OnMove;
    public event Action OnTeleport;
    public UnityEvent m_OnMove;

    public bool Myturn { get => myturn; set => myturn = value; }

    private void Awake() {
        thisPiece = GetComponent<Piece>();
        brain = GetComponentInChildren<IMovementBrain>();

        // Setup Managers
        gridManager = FindObjectOfType<GridManager>();
        turnManager = FindObjectOfType<TurnManager>();
        

    }

    private void Start() {
        if(gridManager){
            pos = gridManager.GetTileAt(transform.position);
        }

        MoveToTarget(pos);
    }

    private void Update() {
        if(Myturn && (brain != null) && (target==null)){
            target = brain.GetTargetMovement(gridManager, pos);
        } 

        if(target){
            Piece piece = gridManager.GetPieceAt(target.Coords);
            bool eatOrFuse = false;
            if (piece) eatOrFuse = thisPiece.EatS.EatinG(piece);
            MoveToTarget(target);
            

            if (!piece || (piece && eatOrFuse))
            {
                turnManager.EndTurn();
                Myturn = false;
            }
            target = null;
        }
    }

    public void MoveToTarget(Tile tile)
    {
        if(tile != pos)
        {
            tile.OnMovedTo(gameObject);
            Anm_piece.Move(transform.position, tile.transform.position, this.gameObject); // Play Animation
        }
        Teleporter tp = tile.GetComponent<Teleporter>();
        if(tp) tile = tp.TP.GetComponent<Tile>();
        transform.position = tile.transform.position;
        pos = tile;
        OnMove?.Invoke();
        m_OnMove?.Invoke();
    }
}
