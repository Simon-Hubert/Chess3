using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieceSelection : MonoBehaviour
{
    GridManager gridManager;

    bool selected = false;
    public bool Selected {get => selected; private set => selected = value;}
    Piece thisPiece;

    public UnityEvent m_OnSelected;
    public UnityEvent m_OnDeSelected;

    public event Action OnSelected;
    public event Action OnDeSelected;

    private void OnEnable() {
        thisPiece = GetComponentInParent<Piece>();
        gridManager = FindObjectOfType<GridManager>();
        thisPiece.GetComponent<Movements>().OnMove += UnSelect;
    }
    private void OnDisable() {
        thisPiece.GetComponent<Movements>().OnMove -= UnSelect;
    }

    private void LateUpdate() {
        if(Input.GetMouseButtonDown(0)){
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Piece piece = gridManager.GetPieceAt(pos);
            if (piece == thisPiece){
                Select();
            }
            else{
                if(Selected) UnSelect();
            }
        }
    }

    void Select(){
        Selected = true;
        Highlights.HighlightTiles((Vector2Int)thisPiece.Coords, thisPiece.Data.Pattern, gridManager);
        OnSelected?.Invoke();
        m_OnSelected?.Invoke();
    }

    void UnSelect(){
        Selected = false;
        gridManager.ClearHighlights();
        OnDeSelected?.Invoke();
        m_OnDeSelected?.Invoke();
    }
}
