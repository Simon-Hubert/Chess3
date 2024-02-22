using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Tile[] tiles;

    public Tile[] Tiles { get => tiles;}

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

    public void ClearHighlights(){
        foreach (Tile tile in tiles)
        {
            tile.Highlighted = false;
        }
    }
}
