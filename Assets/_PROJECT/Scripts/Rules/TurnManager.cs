using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    // public enum turnbase{
    //     classicChess,
    //     tousEnMmTemps
    // }

    // [SerializeField] turnbase _turnbase;

    [SerializeField] List<Piece> blackPieces;
    [SerializeField] Piece VipOuReine;

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

    public int PlayerCounter { get => playerCounter; set => playerCounter = value; }

    private void Start() {
        //RuleController rc = GetComponent<RuleController>();
        gm = FindObjectOfType<GridManager>();
        // if(_turnbase == turnbase.tousEnMmTemps){
        //     MettreToutesLesPieces();
        // }

        blackPieces = gm.GetAllActiveBlackPieces();

        whitePieces = gm.GetAllActiveWhitePieces();

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
            InitEnemyTurn();
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

    void InitEnemyTurn(){
        foreach (Piece piece in blackPieces)
        {
            foreach (Tile tile in CheckMovements.CheckMove((Vector2Int)piece.Coords, piece.Data.Pattern, gm))
            {
                Piece cible = gm.GetPieceAt(tile.Coords);
                if(cible!=null && cible!=piece){
                    piece.Movement.Myturn = true;
                    return;
                }
            }
        }
        VipOuReine.Movement.Myturn = true;
    }

    void InitPlayerTurn(){
        foreach (Piece piece in whitePieces)
        {
            piece.GetComponent<Movements>().Myturn = true;
        }
    }

    private bool CheckForActiveEnemies(){
        return gm.GetAllActiveBlackPieces().Count > 0;
    }

    // void ClassiChessTurn(){
    //     if(CheckForActiveEnemies()){
    //         Piece next = blackPlayOrder[counterClassic%blackPlayOrder.Count];
    //         counterClassic ++;
    //         if(next.gameObject.activeSelf) next.Movement.Myturn = true;
    //         else ClassiChessTurn();
    //     }
    //     else EndTurn();
    // }

    // void TousEnMMtempsTurn(){
    //     foreach (Piece piece in blackPlayOrder)
    //     {
    //         piece.Movement.Myturn = true;
    //     }
    // }



    //[Button]
    // void MettreToutesLesPieces(){
    //     blackPieces = gm.GetAllActiveBlackPieces();
    // }

}
