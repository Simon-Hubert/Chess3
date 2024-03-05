using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movements))]
public class Piece : MonoBehaviour
{
    [SerializeField] PieceData _data;
    Grid grid;
    Movements movement;
    Eating eatS;

    public Vector3Int Coords { get => grid.WorldToCell(transform.position); }
    public PieceData Data { get => _data; set => _data = value; }
    public Movements Movement { get => movement; set => movement = value; }
    public Eating EatS { get => eatS; set => eatS = value; }

    private void Awake()
    {
        eatS = GetComponentInChildren<Eating>();
        movement = transform.GetComponent<Movements>();
        grid = FindObjectOfType<Grid>();
    }

}
