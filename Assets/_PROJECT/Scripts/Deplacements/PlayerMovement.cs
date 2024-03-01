using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovementBrain
{

    List<PieceData.deplacement> deplacements;
    bool initTargetting = true;
    GridManager tileGrid;

    private void Awake() {
        deplacements = GetComponentInParent<Piece>().Data.Pattern;
    }

    public Tile GetTargetMovement(GridManager gridManager, Tile pos)
    {

        Vector2Int coords = (Vector2Int)pos.Coords;

        //HIGHLIGHT les cases ou on peut se déplacer
        if(initTargetting){
            tileGrid = gridManager;
            Highlights.HighlightTiles(coords, deplacements, tileGrid);
            initTargetting = false;
        }

        //Request Input et output la tile
        Tile targetTile = RequestTile();

        if(targetTile){
            tileGrid.ClearHighlights();
            initTargetting = true;
        }
        return targetTile;
    }

    private Tile RequestTile(){
        if(Input.GetMouseButtonDown(0)){
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = tileGrid.transform.GetComponent<Grid>().WorldToCell(pos);
            Tile tile = tileGrid.GetTileAt((Vector2Int)gridPos);
            Debug.Log(tile.Coords);
            if(tile!=null && tile.Highlighted){
                Debug.Log("Deplacement demandé vers :" + tile.Coords);
                return tile;
            }
        }
        return null;
    }

  
}
