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

    public event Action<bool> OnTurnEnd;
    public event Action<bool> OnTurnBegin;

    public UnityEvent m_OnTurnEnd;
    public UnityEvent m_OnTurnBegin;

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
            OnTurnEnd?.Invoke(playerTurn);
            m_OnTurnEnd?.Invoke();
            if (playerTurn) PlayerCounter++;
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
        blackPlayOrder[counterClassic%blackPlayOrder.Count].GetComponent<Movements>().Myturn = true;
        counterClassic ++;
    }

    void TousEnMMtempsTurn(){
        foreach (Piece piece in blackPlayOrder)
        {
            piece.GetComponent<Movements>().Myturn = true;
        }
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
