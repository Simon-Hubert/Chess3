using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject piecesParent;
    [SerializeField] Piece[] pieces;
    [SerializeField] Tile[] tiles;
    Grid grid;

    public Tile[] Tiles { get => tiles;}
    public GameObject PiecesParent { get => piecesParent; set => piecesParent = value; }

    private void OnValidate() {
        grid = GetComponent<Grid>();
        tiles = GetComponentsInChildren<Tile>();
        pieces = PiecesParent.GetComponentsInChildren<Piece>();
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
        foreach (Piece piece in pieces)
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

    public Vector2Int GetCoords(Piece piece){
        return (Vector2Int)GetTileAt(piece.transform.position).Coords;
    }

    public void InstanciateInGrid(GameObject gameObject, Transform position){
        GameObject go = GameObject.Instantiate(gameObject, transform);
        go.transform.position += new Vector3(0.5f,0.5f,0.5f);
        go.GetComponent<Tile>()?.onPaint();
        //WIP
    }
}
