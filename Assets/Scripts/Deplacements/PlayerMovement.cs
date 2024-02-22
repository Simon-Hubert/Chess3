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
            HighlightTiles(coords);
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

    private void HighlightTiles(Vector2Int coords)
    {
        foreach (PieceData.deplacement deplacement in deplacements)
        {
            switch (deplacement.moves)
            {
                case MOVES.I:
                    Highlight_I(coords, deplacement.distance);
                    break;
                case MOVES.X:
                    Highlight_X(coords, deplacement.distance);
                    break;
                case MOVES.L:
                    Highlight_L(coords, deplacement.distance, deplacement.distance2);
                    break;
                case MOVES._:
                    Highlight__(coords, deplacement.distance);
                    break;
                case MOVES.P:
                    Highlight_P(coords, deplacement.distance);
                    break;
                case MOVES.I_:
                    Highlight_I(coords, deplacement.distance);
                    Highlight__(coords, deplacement.distance);
                    break;
            }
        }
    }

    private void Highlight_P(Vector2Int coords, int distance)
    {
        throw new NotImplementedException();
    }

    private void Highlight__(Vector2Int coords, int distance)
    {
        for(int i=1; i<=distance; i++){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(i,0));
            if(t) t.Highlighted = true;
            else break;
        }
        for(int i=-1; i>=-distance; i--){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(i,0));
            if(t) t.Highlighted = true;
            else break;
        }
    }

    private void Highlight_L(Vector2Int coords, int distance, int distance2)
    {
        foreach (Tile tile in tileGrid.Tiles)
        {
            bool a = Mathf.Abs(tile.Coords.x - coords.x) == distance;
            bool b = Mathf.Abs(tile.Coords.y - coords.y) == distance;
            bool c = Mathf.Abs(tile.Coords.x - coords.x) == distance2;
            bool d = Mathf.Abs(tile.Coords.y - coords.y) == distance2;
            if((a&&d)||(b&&c)) tile.Highlighted = true;
        }
    }

    private void Highlight_X(Vector2Int coords, int distance)
    {
        // foreach (Tile tile in tileGrid.Tiles)
        // {
        //     for (int i = -distance, j = -distance; i <= distance; i++, j++)
        //     {
        //         bool a = (tile.Coords.y-i) == coords.y && (tile.Coords.x-j) == coords.x;
        //         bool b = (tile.Coords.y-i) == coords.y && (tile.Coords.x+j) == coords.x;
        //         if(a||b) tile.Highlighted = true;
        //     }
        // }

        for(int i=1, j=1; i<=distance; i++,j++){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(i,j));
            if(t) t.Highlighted = true;
            else break;
        }
        for(int i=1, j=-1; i<=distance; i++,j--){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(i,j));
            if(t) t.Highlighted = true;
            else break;
        }
        for(int i=-1, j=1; i>=-distance; i--,j++){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(i,j));
            if(t) t.Highlighted = true;
            else break;
        }
        for(int i=-1, j=-1; i>=-distance; i--,j--){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(i,j));
            if(t) t.Highlighted = true;
            else break;
        }
    }

    private void Highlight_I(Vector2Int coords, int distance)
    {
        for(int i=1; i<=distance; i++){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(0,i));
            if(t) t.Highlighted = true;
            else break;
        }
        for(int i=-1; i>=-distance; i--){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(0,i));
            if(t) t.Highlighted = true;
            else break;
        }
    }
}
