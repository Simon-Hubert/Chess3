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

    AudioManager audioManager;

    public event Action OnMove;
    public UnityEvent m_OnMove;

    public bool Myturn { get => myturn; set => myturn = value; }

    private void Awake() {
        thisPiece = GetComponent<Piece>();
        brain = GetComponentInChildren<IMovementBrain>();

        // Setup Managers
        gridManager = FindObjectOfType<GridManager>();
        turnManager = FindObjectOfType<TurnManager>();
        audioManager = FindObjectOfType<AudioManager>();
        

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
            OnMove?.Invoke();
            m_OnMove?.Invoke();
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

    private void MoveToTarget(Tile tile)
    {
        transform.position = tile.transform.position;
        pos = tile;
        audioManager.PlaySfx("Move");
    }
}
