using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    Dictionary<Tile, Vector3> Map = new Dictionary<Tile, Vector3>();
    GridManager gm;
    List<Tile> tilesChecked = new List<Tile>();
    List<Tile> toCheck = new List<Tile>();
    Piece queen;
    [SerializeField] Piece king;
    int n = 10;


    private void Awake()
    {
        gm = FindObjectOfType<GridManager>();
        queen = GetComponent<Piece>();
        foreach(Tile tile in gm.Tiles)
        {
            Map.Add(tile, Vector3.zero);
        }
    }

    public void Path()
    {
        Tile start = gm.GetTileAt(transform.position);
        Tile end = gm.GetTileAt(king.transform.position);
        List<Tile> list = new List<Tile>();
        list.Add(start);
        while(list.Count < 1)
        {
            List<Tile> newList = new List<Tile>();
            foreach(Tile tile in list)
            {
                foreach(Tile newtile in CheckTile(tile, n, end)){
                    newList.Add(newtile);
                }
            }
            list = newList;
            n += 10;
        }
    }

    List<Tile> CheckTile(Tile start, int n, Tile end)
    {
        List<Tile> newTiles = new List<Tile>();
        foreach (Tile tile in CheckMovements.CheckMove((Vector2Int)start.Coords, queen.Data.Pattern, gm))
        {
            if(!tilesChecked.Contains(tile))
            {
                tilesChecked.Add(tile);
                newTiles.Add(tile);
                float H  = Vector2Int.Distance((Vector2Int)end.Coords, (Vector2Int)start.Coords);
                Map[tile] = new Vector3(n,H,n+H);
                if (gm.GetPieceAt(tile.Coords)) Map[tile] = new Vector3(Mathf.Infinity, 0,0);
            }
        }
        return newTiles;
    }
}
