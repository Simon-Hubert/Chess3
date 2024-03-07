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

    [SerializeField] turnbase _turnbase;

    [SerializeField, ShowIf("_turnbase", turnbase.classicChess)] List<Piece> blackPlayOrder;

    GridManager gm;
    List<Piece> whitePieces = new List<Piece>();
    int counterClassic = 0;

    int playerCounter = 0;

    bool playerTurn = true;
    bool turnEnded = false;

    public static event Action<bool> OnTurnEnd;
    public static event Action<bool> OnTurnBegin;

    public static UnityEvent m_OnTurnEnd;
    public static UnityEvent m_OnTurnBegin;
    public UnityEvent OnStartDialogue;

    public int PlayerCounter { get => playerCounter; set => playerCounter = value; }

    private void Start() {
        gm = FindObjectOfType<GridManager>();
        if(_turnbase == turnbase.tousEnMmTemps){
            MettreToutesLesPieces();
        }

        whitePieces = gm.GetAllActiveWhitePieces();
        OnStartDialogue?.Invoke();
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
                piece.Movement.Myturn = false;
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
            if(next.gameObject.activeSelf) next.Movement.Myturn = true;
            else ClassiChessTurn();
        }
        else EndTurn();
    }

    void TousEnMMtempsTurn(){
        foreach (Piece piece in blackPlayOrder)
        {
            piece.Movement.Myturn = true;
        }
    }

    private bool CheckForActiveEnemies(){
        return gm.GetAllActiveBlackPieces().Count > 0;
    }


    [Button]
    void MettreToutesLesPieces(){
        blackPlayOrder = gm.GetAllActiveBlackPieces();
    }

}
