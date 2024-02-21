using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] PieceData _data;

    public PieceData Data { get => _data; set => _data = value; }
}
