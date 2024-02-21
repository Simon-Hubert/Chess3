using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MOVES
{
    I,
    X,
    L,
    _,
    P,
    
    IX,
    IL,
    I_,
    II,

    XL,
    X_,
    XX,

    L_,
    LL
}
enum Names
{
    Roi,
    Dame,
    Fou,
    Cavalier,
    Tour,
    Pion
}
[Serializable]
public struct Piece
{
    [SerializeField] string _name;
    [SerializeField] int _level;
    [SerializeField] MOVES[] _patern;
    [SerializeField] Sprite _sprite;
    [SerializeField] int _distance;
    [SerializeField] bool _isWhite;
}
