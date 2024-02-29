using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public enum Turn { Player, AI }
    public Turn currentTurn;

    // Référence à la pièce actuellement sélectionnée
    private PieceData selectedPiece;

    void Start()
    {
        currentTurn = Turn.Player; // Le joueur commence toujours
    }

    void Update()
    {
        switch (currentTurn)
        {
            case Turn.Player:
                HandlePlayerTurn();
                break;
            case Turn.AI:
                HandleAITurn();
                break;
        }
    }

    void HandlePlayerTurn()
    {
    }

    void HandleAITurn()
    {
        
    }
}