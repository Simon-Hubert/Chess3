using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] PieceData _data;
    Grid grid;
    public Vector3Int Coords { get => grid.WorldToCell(transform.position); }

    public PieceData Data { get => _data; set => _data = value; }

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

}
