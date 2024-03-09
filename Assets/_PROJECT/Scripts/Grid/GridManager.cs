using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject piecesParent;
    [SerializeField] List<Piece> pieces;
    [SerializeField] Tile[] tiles;
    [SerializeField] Grid grid;
    [SerializeField] GridSettings gs;
    Tile[,] tileGrid;
    Piece[,] pieceGrid;
    Vector2Int centralVector;
    Vector2Int sizeGrid;


    public Tile[] Tiles { get => tiles;}
    public List<Piece> Pieces { get => pieces; private set => pieces = value; }
    public List<Piece> BlackPieces{ get; private set;}
    public List<Piece> WhitePieces{ get; private set;}

    private void Awake() {
        grid = GetComponent<Grid>();
        gs = GetComponent<GridSettings>();
        tiles = GetComponentsInChildren<Tile>();
        Pieces = new List<Piece>();
        BlackPieces = new List<Piece>();
        WhitePieces = new List<Piece>();

        foreach (Piece piece in piecesParent.GetComponentsInChildren<Piece>())
        {
            Pieces.Add(piece);
            if(piece.Data.IsWhite) WhitePieces.Add(piece);
            else BlackPieces.Add(piece);
        }

        sizeGrid = MinMaxTile(Tiles);
        tileGrid = new Tile[sizeGrid.x,sizeGrid.y];
        pieceGrid = new Piece[sizeGrid.x,sizeGrid.y];
        UpdateTileGrid();
        UpdatePieceGrid();

    }
    private void OnEnable() {
        TurnManager.OnTurnBegin += UpdateTileGrid;
        TurnManager.OnTurnBegin += UpdatePieceGrid;
        TurnManager.OnTurnEnd += UpdateTileGrid;
        TurnManager.OnTurnEnd += UpdatePieceGrid;
        Movements.OnMove += UpdateTileGrid_M;
        Movements.OnMove += UpdatePieceGrid_M;
    }
    private void OnDisable() {
        TurnManager.OnTurnBegin -= UpdateTileGrid;
        TurnManager.OnTurnBegin -= UpdatePieceGrid;
        TurnManager.OnTurnEnd -= UpdateTileGrid;
        TurnManager.OnTurnEnd -= UpdatePieceGrid;
        Movements.OnMove -= UpdateTileGrid_M;
        Movements.OnMove -= UpdatePieceGrid_M;
    }

    private List<Piece> ActivePieces(List<Piece> listPieces){
        List<Piece> pieces = new List<Piece>();
        foreach (Piece piece in listPieces)
        {
            if (piece.gameObject.activeSelf) pieces.Add(piece);
        }
        return pieces;
    }

    public List<Piece> GetAllActivePieces(){
        return ActivePieces(Pieces);
    }

    public List<Piece> GetAllActiveBlackPieces(){
        return ActivePieces(BlackPieces);
    }

    public List<Piece> GetAllActiveWhitePieces(){
        return ActivePieces(WhitePieces);
    }


    public Tile GetTileAt(Vector2Int coordinates){
        int a = coordinates.x-centralVector.x;
        int b = coordinates.y-centralVector.y;
        if(a >= 0 && a < sizeGrid.x && b >= 0 && b < sizeGrid.y) return tileGrid[coordinates.x-centralVector.x,coordinates.y-centralVector.y];
        return null;
        // foreach (Tile tile in tiles)
        // {
        //     if((Vector2Int)tile.Coords == coordinates) return tile;
        // }
        //return null;
    }

    public Tile GetTileAt(Vector3 pos){
        Vector2Int selfPos = (Vector2Int)grid.WorldToCell(pos);
        return GetTileAt(selfPos);
    }

    public Piece GetPieceAt(Vector2Int coordinates)
    {
        int a = coordinates.x-centralVector.x;
        int b = coordinates.y-centralVector.y;
        if(a >= 0 && a < sizeGrid.x && b >= 0 && b < sizeGrid.y) return pieceGrid[coordinates.x-centralVector.x,coordinates.y-centralVector.y];
        return null;
        // foreach (Piece piece in Pieces)
        // {
        //     if ((Vector2Int)piece.Coords == coordinates && piece.gameObject.activeSelf) return piece;
        // }
        // return null;
    }

    public Piece GetPieceAt(Vector3 pos)
    {
        Vector2Int selfPos = (Vector2Int)grid.WorldToCell(pos);
        return GetPieceAt(selfPos);
    }


    public void ClearMoveToAble(){
        foreach (Tile tile in tiles)
        {
            tile.UnMoveToAble();
        }
    }

    public void ClearHighlights(){
        foreach (Tile tile in tiles)
        {
            tile.Highlighted = false;
        }
    }

    public Vector2Int GetCoords(Piece piece){
        return (Vector2Int)GetTileAt(piece.transform.position).Coords;
    }

    public void InstanciateInGrid(GameObject gameObject, Vector3Int position){
        GameObject go = GameObject.Instantiate(gameObject, grid.CellToWorld(position), Quaternion.identity, transform);
        go.transform.position += new Vector3(0.5f,0.5f,0f);

        Tile tile = go.GetComponent<Tile>();
        if(tile){
            tile.Grid = grid;
            tile.GridSettings = gs;
            tile.onPaint();
        }
        tiles = GetComponentsInChildren<Tile>();
    }

    public void ClearGrid(){
        if(Tiles.Length >= 1){
            foreach (Tile tile in Tiles)
            {
                DestroyImmediate(tile.gameObject);
            }
        }
    }

    private Vector2Int MinMaxTile(Tile[] tileList){
        Vector2Int minMaxX = Vector2Int.zero;
        Vector2Int minMaxY = Vector2Int.zero;
        foreach (Tile tile in tileList){
            minMaxX.y = tile.Coords.x > minMaxX.y ? tile.Coords.x : minMaxX.y;
            minMaxX.x = tile.Coords.x < minMaxX.x ? tile.Coords.x : minMaxX.x;
            minMaxY.y = tile.Coords.y > minMaxY.y ? tile.Coords.y : minMaxY.y;
            minMaxY.x = tile.Coords.y < minMaxY.x ? tile.Coords.y : minMaxY.x;
        }
        centralVector = new Vector2Int(minMaxX.x, minMaxY.x);
        return new Vector2Int(Mathf.Abs(minMaxX.x - minMaxX.y)+1, Mathf.Abs(minMaxY.x - minMaxY.y)+1);
    }

    private void UpdatePieceGrid(bool b = true){
        for (int i = 0; i < sizeGrid.x; i++)
        {
            for (int j = 0; j < sizeGrid.y; j++)
            {
                pieceGrid[i,j] = null;
            }
        }
        foreach (Piece piece in Pieces)
        {
            Vector2Int c = (Vector2Int)grid.WorldToCell(piece.transform.position) - centralVector;
            pieceGrid[c.x,c.y] = piece;
        }
    }

    private void UpdateTileGrid(bool b = true){
        for (int i = 0; i < sizeGrid.x; i++)
        {
            for (int j = 0; j < sizeGrid.y; j++)
            {
                tileGrid[i,j] = null;
            }
        }
        foreach (Tile tile in Tiles)
        {
            Vector2Int c = (Vector2Int)grid.WorldToCell(tile.transform.position) - centralVector;
            tileGrid[c.x,c.y] = tile;
        }
    }

    
    private void UpdateTileGrid_M(){
        UpdateTileGrid();
    }

    
    private void UpdatePieceGrid_M(){
        UpdatePieceGrid();
    }
}
