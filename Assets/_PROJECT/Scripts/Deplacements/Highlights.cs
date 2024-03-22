using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlights
{
    public static void HighlightTiles(Vector2Int coords, List<PieceData.deplacement> deplacements, GridManager tileGrid)
    {
        foreach (PieceData.deplacement deplacement in deplacements)
        {
            switch (deplacement.moves)
            {
                case MOVES.I:
                    Highlight_I(coords, deplacement.distance, tileGrid);
                    break;
                case MOVES.X:
                    Highlight_X(coords, deplacement.distance, tileGrid);
                    break;
                case MOVES.L:
                    Highlight_L(coords, deplacement.distance, deplacement.distance2, tileGrid);
                    break;
                case MOVES._:
                    Highlight__(coords, deplacement.distance, tileGrid);
                    break;
                case MOVES.P:
                    Highlight_P(coords, deplacement.distance, tileGrid);
                    break;
                case MOVES.I_:
                    Highlight_I(coords, deplacement.distance, tileGrid);
                    Highlight__(coords, deplacement.distance, tileGrid);
                    break;
            }
        }
    }

    private static void Highlight_P(Vector2Int coords, int distance, GridManager tileGrid)
    {
        foreach (Tile tile in CheckMovements.Move_P(coords, distance, tileGrid))
        {
            tile.HighlightThis();
        }
    }

    private static void Highlight__(Vector2Int coords, int distance, GridManager tileGrid)
    {
        foreach (Tile tile in CheckMovements.Move__(coords, distance, tileGrid))
        {
            tile.HighlightThis();
        }
    }

    private static void Highlight_L(Vector2Int coords, int distance, int distance2, GridManager tileGrid)
    {
        foreach (Tile tile in tileGrid.Tiles)
        {
            bool a = Mathf.Abs(tile.Coords.x - coords.x) == distance;
            bool b = Mathf.Abs(tile.Coords.y - coords.y) == distance;
            bool c = Mathf.Abs(tile.Coords.x - coords.x) == distance2;
            bool d = Mathf.Abs(tile.Coords.y - coords.y) == distance2;
            if((a&&d)||(b&&c)) tile.HighlightThis();
        }
    }

    private static void Highlight_X(Vector2Int coords, int distance, GridManager tileGrid)
    {
        foreach (Tile tile in CheckMovements.Move_X(coords, distance, tileGrid))
        {
            tile.HighlightThis();
        }
    }

    private static void Highlight_I(Vector2Int coords, int distance, GridManager tileGrid)
    {
        foreach (Tile tile in CheckMovements.Move_I(coords, distance, tileGrid))
        {
            tile.HighlightThis();
        }
    }
}
