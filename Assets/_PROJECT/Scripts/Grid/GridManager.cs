using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridManager : MonoBehaviour
{
    [SerializeField] Tile[] tiles;
    Grid grid;

    public Tile[] Tiles { get => tiles;}

    private void Reset() {
        grid = GetComponent<Grid>();
    }

    private void Awake() {
        tiles = GetComponentsInChildren<Tile>();
    }

    public Tile GetTileAt(Vector2Int coordinates){
        foreach (Tile tile in tiles)
        {
            if((Vector2Int)tile.Coords == coordinates) return tile;
        }
        return null;
    }

    public Tile GetTileAt(Vector3 pos){
        Vector2Int selfPos = (Vector2Int)grid.WorldToCell(transform.position);
        return GetTileAt(selfPos);
    }

    public void ClearHighlights(){
        foreach (Tile tile in tiles)
        {
            tile.Highlighted = false;
        }
    }
}
