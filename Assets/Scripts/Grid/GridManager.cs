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
}
