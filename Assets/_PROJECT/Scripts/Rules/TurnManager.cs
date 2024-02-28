using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    public enum turnbase{
        classiChess,
        tousEnMMtemps
    }
    [SerializeField] turnbase _turnbase;

    [SerializeField, ShowIf("_turnbase", turnbase.classiChess)] List<Piece> playOrder;

    bool playerTurn = true;

    public event Action OnTurnEnd;
    public event Action OnTurnBegin;

    public UnityEvent m_OnTurnEnd;
    public UnityEvent m_OnTurnBegin;

    [Button, ShowIf("_turnbase", turnbase.classiChess)]
    public void MettreToutesLesPieces(){
        
    }

    public void EndTurn(){
        playerTurn = !playerTurn;

        OnTurnEnd.Invoke();
        m_OnTurnEnd.Invoke();
    }




}
