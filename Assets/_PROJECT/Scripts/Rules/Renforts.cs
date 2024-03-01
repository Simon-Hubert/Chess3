using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renforts : MonoBehaviour
{
    [SerializeField] GameObject piecesParent;
    [SerializeField]List<GameObject> pieces = new List<GameObject>();
    GridManager gridManager;

    [SerializeField, Range(0, 10)] int TourInactionMax;
    int currentInactions = 0;
    bool hasEatenOrFused = false;
    private void OnValidate()
    {
        if (piecesParent == null)
            Debug.LogWarning("He ! Tu peux mettre le gameobject PIECE dans pieceParent stp :,) sinon ça peut pas fonctionner haha");
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager == null )
            Debug.LogWarning("Il n'y a pas de GridManager dans la scène");
        pieces.Clear();
        foreach (RenfortPiece renfort in gridManager.PiecesParent.GetComponentsInChildren<RenfortPiece>())
        {
            pieces.Add(renfort.gameObject);
        }
    }
    private void OnEnable()
    {
        Eating.OnEat += OnEat;
        TurnManager.OnTurnEnd += OnEndTurn;
    }
    private void OnDisable()
    {
        Eating.OnEat -= OnEat;
        TurnManager.OnTurnEnd -= OnEndTurn;
    }
    [Button]
    void CallRenfort()
    {
        // active les pièces et play animtion
        foreach(GameObject renfort in pieces)
        {
            if(!renfort.gameObject.activeSelf)
            renfort.gameObject.SetActive(true);
        }
    }
    public void Inacting()
    {
        currentInactions++;
        if (currentInactions >= TourInactionMax)
        {
            CallRenfort();
        }
    }

    public void Acting()
    {
        currentInactions = 0;
    }

    public void OnEndTurn(bool isPlayerTurn)
    {
        if(isPlayerTurn && hasEatenOrFused)
        {
            Acting();
        }
        else
        {
            if(isPlayerTurn)
            {
                Inacting();
            }
        }
        hasEatenOrFused = false;
    }
    public void OnEat(Piece piece = null)
    {
        hasEatenOrFused = true;
    }
}
