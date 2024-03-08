using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
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

    public event Action OnMove;
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
        if(tile != pos)
        {
            tile.OnMovedTo(gameObject);
        }
        //StartCoroutine(GoToTile(tile));
        transform.position = tile.transform.position;
        pos = tile;
    }
    IEnumerator GoToTile(Tile tile)
    {
        float elapsedTime = 0;
        float e2 = 0;
        Vector2 pos = transform.position;
        float saveY = transform.position.y;
        bool canGo = false;
        while(elapsedTime < thisPiece.Data.TimeLimit)
        {
            if (pos.y < saveY + 0.2f)
            {
                pos.y = Mathf.Lerp(transform.position.y, saveY + 0.2f, thisPiece.Data.Speed.Evaluate(elapsedTime));
                transform.position = pos;
                elapsedTime += Time.deltaTime;
                if(pos.y >= saveY + 0.2f) canGo = true;
                yield return null;
            }
            if(canGo)
            {
                pos.x = Mathf.Lerp(transform.position.x, tile.transform.position.x, thisPiece.Data.Speed.Evaluate(e2));
                pos.y = Mathf.Lerp(transform.position.y, tile.transform.position.y + 0.2f, thisPiece.Data.Speed.Evaluate(e2));
                transform.position = pos;
                e2 += Time.deltaTime;
                elapsedTime += Time.deltaTime;
                if (transform.position.y >= tile.transform.position.y + 0.2f) canGo = false;
                yield return null;
            }
            if(!canGo && transform.position.y >= tile.transform.position.y + 0.1f)
            {
                pos.y = Mathf.Lerp(transform.position.y,tile.transform.position.y, thisPiece.Data.Speed.Evaluate(elapsedTime));
                transform.position = pos;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
        }

    }
}
