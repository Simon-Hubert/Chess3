using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class CheckMovements
{
    public static List<Tile> CheckMove(Vector2Int coords, List<PieceData.deplacement> deplacements, GridManager tileGrid)
    {
        List<Tile> allTiles = new List<Tile>();
        foreach (PieceData.deplacement deplacement in deplacements)
        {
            switch (deplacement.moves)
            {
                case MOVES.I:
                    Add(allTiles, Move_I(coords, deplacement.distance, tileGrid));
                    break;
                case MOVES.X:
                    Add(allTiles, Move_X(coords, deplacement.distance, tileGrid));
                    break;
                case MOVES.L:
                    Add(allTiles, Move_L(coords, deplacement.distance, deplacement.distance2, tileGrid));
                    break;
                case MOVES._:
                    Add(allTiles,  Move__(coords, deplacement.distance, tileGrid));
                    break;
                case MOVES.P:
                    Add(allTiles, Move_P(coords, deplacement.distance, tileGrid));
                    break;
                case MOVES.I_:
                    Add(allTiles, Move_I(coords, deplacement.distance, tileGrid));
                    Add(allTiles, Move__(coords, deplacement.distance, tileGrid));
                    break;
            }
        }
        return allTiles;
    }

    private static List<Tile> Add(List<Tile> t1, List<Tile> t2){
        foreach (Tile tile in t2)
        {
            t1.Add(tile);
        }
        return t1;

    }

    public static List<Tile> Move_P(Vector2Int coords, int distance, GridManager tileGrid)
    {
        List<Tile> tiles = new List<Tile>();
        tiles = Add(tiles, Move_I(coords, distance, tileGrid));
        tiles = Add(tiles, Move__(coords, distance, tileGrid));
        List<Tile> checkDiag = new List<Tile>();
        checkDiag = Add(checkDiag, Move_X(coords, distance, tileGrid));
        foreach (Tile tile in checkDiag)
        {
            if(tileGrid.GetPieceAt(tile.Coords)) tiles.Add(tile);
        }
        return tiles;
    }

    public static List<Tile> Move__(Vector2Int coords, int distance, GridManager tileGrid)
    {
        List<Tile> tiles = new List<Tile>();
        Piece thisPiece = tileGrid.GetPieceAt(coords);

        for(int i=1; i<=distance; i++){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(i,0));
            Piece p = tileGrid.GetPieceAt(coords + new Vector2Int(i,0));
            if(p){
                if(p.IsWall && thisPiece.Data.CanBreak){
                    tiles.Add(t);
                    break;
                }
                if(p.IsWall) break;
                if(p.Data.CanFuse || thisPiece.Data.CanFuse){
                    tiles.Add(t);
                    break;
                }
                if(p.Data.IsWhite == thisPiece.Data.IsWhite) break;
                else{
                    tiles.Add(t);
                    break;
                }
            }
            if(t) tiles.Add(t);
            else break;
        }
        for(int i=-1; i>=-distance; i--){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(i,0));
            Piece p = tileGrid.GetPieceAt(coords + new Vector2Int(i,0));
            if (p)
            {
                if (p.IsWall && thisPiece.Data.CanBreak)
                {
                    tiles.Add(t);
                    break;
                }
                if (p.IsWall) break;
                if (p.Data.CanFuse || thisPiece.Data.CanFuse)
                {
                    tiles.Add(t);
                    break;
                }
                if (p.Data.IsWhite == thisPiece.Data.IsWhite) break;
                else
                {
                    tiles.Add(t);
                    break;
                }
            }
            if (t) tiles.Add(t);
            else break;
        }
        return tiles;
    }

    public static List<Tile> Move_L(Vector2Int coords, int distance, int distance2, GridManager tileGrid)
    {
        List<Tile> tilesTemp = new List<Tile>();
        Piece thisPiece = tileGrid.GetPieceAt(coords);

        foreach (Tile tile in tileGrid.Tiles)
        {
            bool a = Mathf.Abs(tile.Coords.x - coords.x) == distance;
            bool b = Mathf.Abs(tile.Coords.y - coords.y) == distance;
            bool c = Mathf.Abs(tile.Coords.x - coords.x) == distance2;
            bool d = Mathf.Abs(tile.Coords.y - coords.y) == distance2;
            if((a&&d)||(b&&c)) tilesTemp.Add(tile);
        }
        List<Tile> tiles = new List<Tile>();

        foreach (Tile tile in tilesTemp)
        {
            Piece p = tileGrid.GetPieceAt(tile.Coords);

            if (p)
            {
                if (p.IsWall && thisPiece.Data.CanBreak)
                {
                    tiles.Add(tile);
                    break;
                }
                if (p.IsWall) break;
                if (p.Data.CanFuse || thisPiece.Data.CanFuse)
                {
                    tiles.Add(tile);
                    break;
                }
                if (p.Data.IsWhite == thisPiece.Data.IsWhite) break;
                else
                {
                    tiles.Add(tile);
                    break;
                }
            }
        }

        return tiles;
    }

    public static List<Tile> Move_X(Vector2Int coords, int distance, GridManager tileGrid)
    {
        List<Tile> tiles = new List<Tile>();
        Piece thisPiece = tileGrid.GetPieceAt(coords);

        for(int i=1, j=1; i<=distance; i++,j++){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(i,j));
            Piece p = tileGrid.GetPieceAt(coords + new Vector2Int(i,j));
            if (p)
            {
                if (p.IsWall && thisPiece.Data.CanBreak)
                {
                    tiles.Add(t);
                    break;
                }
                if (p.IsWall) break;
                if (p.Data.CanFuse || thisPiece.Data.CanFuse)
                {
                    tiles.Add(t);
                    break;
                }
                if (p.Data.IsWhite == thisPiece.Data.IsWhite) break;
                else
                {
                    tiles.Add(t);
                    break;
                }
            }
            if (t) tiles.Add(t);
            else break;
        }
        for(int i=1, j=-1; i<=distance; i++,j--){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(i,j));
            Piece p = tileGrid.GetPieceAt(coords + new Vector2Int(i,j));
            if (p)
            {
                if (p.IsWall && thisPiece.Data.CanBreak)
                {
                    tiles.Add(t);
                    break;
                }
                if (p.IsWall) break;
                if (p.Data.CanFuse || thisPiece.Data.CanFuse)
                {
                    tiles.Add(t);
                    break;
                }
                if (p.Data.IsWhite == thisPiece.Data.IsWhite) break;
                else
                {
                    tiles.Add(t);
                    break;
                }
            }
            if (t) tiles.Add(t);
            else break;
        }
        for(int i=-1, j=1; i>=-distance; i--,j++){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(i,j));
            Piece p = tileGrid.GetPieceAt(coords + new Vector2Int(i,j));
            if (p)
            {
                if (p.IsWall && thisPiece.Data.CanBreak)
                {
                    tiles.Add(t);
                    break;
                }
                if (p.IsWall) break;
                if (p.Data.CanFuse || thisPiece.Data.CanFuse)
                {
                    tiles.Add(t);
                    break;
                }
                if (p.Data.IsWhite == thisPiece.Data.IsWhite) break;
                else
                {
                    tiles.Add(t);
                    break;
                }
            }
            if (t) tiles.Add(t);
            else break;
        }
        for(int i=-1, j=-1; i>=-distance; i--,j--){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(i,j));
            Piece p = tileGrid.GetPieceAt(coords + new Vector2Int(i,j));
            if (p)
            {
                if (p.IsWall && thisPiece.Data.CanBreak)
                {
                    tiles.Add(t);
                    break;
                }
                if (p.IsWall) break;
                if (p.Data.CanFuse || thisPiece.Data.CanFuse)
                {
                    tiles.Add(t);
                    break;
                }
                if (p.Data.IsWhite == thisPiece.Data.IsWhite) break;
                else
                {
                    tiles.Add(t);
                    break;
                }
            }
            if (t) tiles.Add(t);
            else break;
        }
        return tiles;
    }

    public static List<Tile> Move_I(Vector2Int coords, int distance, GridManager tileGrid)
    {
        List<Tile> tiles = new List<Tile>();
        Piece thisPiece = tileGrid.GetPieceAt(coords);
        for(int i=1; i<=distance; i++){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(0,i));
            Piece p = tileGrid.GetPieceAt(coords + new Vector2Int(0,i));
            if (p)
            {
                if (p.IsWall && thisPiece.Data.CanBreak)
                {
                    tiles.Add(t);
                    break;
                }
                if (p.IsWall) break;
                if (p.Data.CanFuse || thisPiece.Data.CanFuse)
                {
                    tiles.Add(t);
                    break;
                }
                if (p.Data.IsWhite == thisPiece.Data.IsWhite) break;
                else
                {
                    tiles.Add(t);
                    break;
                }
            }
            if (t) tiles.Add(t);
            else break;
        }
        for(int i=-1; i>=-distance; i--){
            Tile t = tileGrid.GetTileAt(coords + new Vector2Int(0,i));
            Piece p = tileGrid.GetPieceAt(coords + new Vector2Int(0,i));
            if (p)
            {
                if (p.IsWall && thisPiece.Data.CanBreak)
                {
                    tiles.Add(t);
                    break;
                }
                if (p.IsWall) break;
                if (p.Data.CanFuse || thisPiece.Data.CanFuse)
                {
                    tiles.Add(t);
                    break;
                }
                if (p.Data.IsWhite == thisPiece.Data.IsWhite) break;
                else
                {
                    tiles.Add(t);
                    break;
                }
            }
            if (t) tiles.Add(t);
            else break;
        }
        return tiles;
    }
}
