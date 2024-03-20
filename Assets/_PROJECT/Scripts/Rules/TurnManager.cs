using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{

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
    public UnityEvent OnStartDialogue;

    public int PlayerCounter { get => playerCounter; set => playerCounter = value; }

    private void Start() {
        gm = FindObjectOfType<GridManager>();
        blackPieces = gm.GetAllActiveBlackPieces();
        whitePieces = gm.GetAllActiveWhitePieces();
        OnStartDialogue?.Invoke();
        BeginTurn();
    }

    private void LateUpdate() {
        if(turnEnded) BeginTurn();
    }

    void BeginTurn(){
        turnEnded = false;
        if(playerTurn){
            InitPlayerTurn();
        }
        else{
            StartEnemyTurn();
        }
        OnTurnBegin?.Invoke(playerTurn);
        m_OnTurnBegin?.Invoke();
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

    void StartEnemyTurn(){
        StartCoroutine(bTurn());
        IEnumerator bTurn(){
            yield return new WaitForSeconds(5f);
            InitEnemyTurn();
        }
    }

    void InitEnemyTurn(){
        if(CheckForActiveEnemies()){
            foreach (Piece piece in gm.GetAllActiveBlackPieces())
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
            if(VipOuReine) VipOuReine.Movement.Myturn = true;
            else EndTurn();
        }
        else EndTurn();
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

    public void ChangeReine_VIP(Piece Reine)
    {
        VipOuReine = Reine;
    }
}