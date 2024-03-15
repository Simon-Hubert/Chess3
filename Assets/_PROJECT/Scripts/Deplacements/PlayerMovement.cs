using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovementBrain
{

    List<PieceData.deplacement> deplacements;
    //bool initTargetting = true;
    GridManager tileGrid;
    PieceSelection pSelect;

    private void Awake() {
        deplacements = GetComponentInParent<Piece>().Data.Pattern;
        pSelect = GetComponent<PieceSelection>();
        tileGrid = FindObjectOfType<GridManager>();
    }

    public Tile GetTargetMovement(GridManager gridManager, Tile pos)
    {
        //Request Input et output la tile
        Tile targetTile = pSelect.Selected ? RequestTile() : null;

        if(targetTile){
            tileGrid.ClearMoveToAble();
        }
        return targetTile;
    }

    private Tile RequestTile(){
        if(Input.GetMouseButtonDown(0)){
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Tile tile = tileGrid.GetTileAt(pos);
            //Debug.Log(tile.Coords);
            if(tile!=null && tile.MoveToAble){
                //Debug.Log("Deplacement demandé vers :" + tile.Coords);
                return tile;
            }
            AudioManager.Instance?.PlaySfx("SelectionPion");
        }
        return null;
    }  
}
