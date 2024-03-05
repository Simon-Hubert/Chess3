using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject piecesParent;
    [SerializeField] List<Piece> pieces;
    [SerializeField] Tile[] tiles;
    Grid grid;

    public Tile[] Tiles { get => tiles;}
    public List<Piece> Pieces { get => pieces; private set => pieces = value; }
    public List<Piece> BlackPieces{ get; private set;}
    public List<Piece> WhitePieces{ get; private set;}

    private void Awake() {
        grid = GetComponent<Grid>();
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
        foreach (Tile tile in tiles)
        {
            if((Vector2Int)tile.Coords == coordinates) return tile;
        }
        return null;
    }

    public Tile GetTileAt(Vector3 pos){
        Vector2Int selfPos = (Vector2Int)grid.WorldToCell(pos);
        return GetTileAt(selfPos);
    }

    public Piece GetPieceAt(Vector2Int coordinates)
    {
        foreach (Piece piece in Pieces)
        {
            if ((Vector2Int)piece.Coords == coordinates && piece.gameObject.activeSelf) return piece;
        }
        return null;
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
}
