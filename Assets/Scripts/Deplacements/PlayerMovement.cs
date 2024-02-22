using System;
using System.Collections;
using System.Collections.Generic;
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

        if(initTargetting){
            tileGrid = gridManager;
            HighlightTiles(coords);
            initTargetting = false;
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
        foreach (Tile tile in tileGrid.Tiles)
        {
            for (int i = -distance; i <= distance; i++)
            {
                if((tile.Coords.x-i) == coords.x && tile.Coords.y == coords.y) tile.Highlighted = true;
            }
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
        foreach (Tile tile in tileGrid.Tiles)
        {
            for (int i = -distance, j = -distance; i <= distance; i++, j++)
            {
                bool a = (tile.Coords.y-i) == coords.y && (tile.Coords.x-j) == coords.x;
                bool b = (tile.Coords.y-i) == coords.y && (tile.Coords.x+j) == coords.x;
                if(a||b) tile.Highlighted = true;
            }
        }
    }

    private void Highlight_I(Vector2Int coords, int distance)
    {
        foreach (Tile tile in tileGrid.Tiles)
        {
            for (int i = -distance; i <= distance; i++)
            {
                if((tile.Coords.y-i) == coords.y && tile.Coords.x == coords.x) tile.Highlighted = true;
            }
        }
    }
}
