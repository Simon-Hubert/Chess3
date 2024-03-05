using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    public enum turnbase{
        classicChess,
        tousEnMmTemps
    }

    [SerializeField] GameObject parentPieces;
    [SerializeField] turnbase _turnbase;

    [SerializeField, ShowIf("_turnbase", turnbase.classicChess)] List<Piece> blackPlayOrder;

    List<Piece> whitePieces = new List<Piece>();
    int counterClassic = 0;

    int playerCounter = 0;

    bool playerTurn = true;
    bool turnEnded = false;

    public static event Action<bool> OnTurnEnd;
    public static event Action<bool> OnTurnBegin;

    public static UnityEvent m_OnTurnEnd;
    public static UnityEvent m_OnTurnBegin;

    public int PlayerCounter { get => playerCounter; set => playerCounter = value; }

    private void Start() {
        if(_turnbase == turnbase.tousEnMmTemps){
            MettreToutesLesPieces();
        }

        Piece[] pieces = parentPieces.GetComponentsInChildren<Piece>();
        foreach (Piece piece in pieces){
            if(piece.Data.IsWhite){
                whitePieces.Add(piece);
            }
        }
        BeginTurn();
    }

    private void LateUpdate() {
        if(turnEnded) BeginTurn();
    }

    void BeginTurn(){
        Debug.Log("Beginning turn");
        OnTurnBegin?.Invoke(playerTurn);
        m_OnTurnBegin?.Invoke();
        turnEnded = false;
        if(playerTurn){
            InitPlayerTurn();
        }
        else{
            switch (_turnbase) //Au cas ou on rajoute
            {
                case turnbase.classicChess:
                    ClassiChessTurn();
                    break;
                case turnbase.tousEnMmTemps:
                    TousEnMMtempsTurn();
                    break;
            }
        }
    }

    public void EndTurn(){
        if(!turnEnded){
            foreach (Piece piece in whitePieces)
            {
                piece.GetComponent<Movements>().Myturn = false;
            }
            if (playerTurn) PlayerCounter++;
            OnTurnEnd?.Invoke(playerTurn);
            m_OnTurnEnd?.Invoke();
            playerTurn = !playerTurn;
            turnEnded = true;
        }
    }

    void InitPlayerTurn(){
        foreach (Piece piece in whitePieces)
        {
            piece.GetComponent<Movements>().Myturn = true;
        }
    }

    void ClassiChessTurn(){
        if(CheckForActiveEnemies()){
            Piece next = blackPlayOrder[counterClassic%blackPlayOrder.Count];
            counterClassic ++;
            if(next.gameObject.activeSelf) next.GetComponent<Movements>().Myturn = true;
            else ClassiChessTurn();
        }
        else EndTurn();
    }

    void TousEnMMtempsTurn(){
        foreach (Piece piece in blackPlayOrder)
        {
            piece.GetComponent<Movements>().Myturn = true;
        }
    }

    private bool CheckForActiveEnemies(){
        foreach (Piece piece in blackPlayOrder)
        {
            if(piece.gameObject.activeSelf) return true;
        }
        return false;
    }


    [Button]
    void MettreToutesLesPieces(){
        Piece[] pieces = parentPieces.GetComponentsInChildren<Piece>();
        foreach (Piece piece in pieces){
            if(!piece.Data.IsWhite){
                blackPlayOrder.Add(piece);
            }
        }
    }

}
