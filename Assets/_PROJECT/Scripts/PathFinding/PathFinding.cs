using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    GridManager gm;
    List<Vector3> GHF = new List<Vector3>();
    List<Tile> tilesChecked = new List<Tile>();
    Piece queen;
    private void Awake()
    {
        gm = FindObjectOfType<GridManager>();
        queen = GetComponent<Piece>();
    }

    public void Path()
    {
        Tile start = gm.GetTileAt(transform.position);
        CheckMovements.CheckMove((Vector2Int)start.Coords, queen.Data.Pattern, gm);
    }
}
