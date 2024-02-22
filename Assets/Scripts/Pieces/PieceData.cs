using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVES
{
    I,
    X,
    L,
    _,
    P,
    
    I_,
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
public class PieceData
{
    [Serializable]
    public struct deplacement
    {
        public MOVES moves;
        public int distance;
        [ShowIf("moves", MOVES.L), AllowNesting]public int distance2;

    }

    [SerializeField] string _name;
    [SerializeField] int _level;
    [SerializeField] List<deplacement> _pattern;
    [SerializeField] Sprite _sprite;
    [SerializeField] bool _isWhite, canFuse;

    public string Name { get => _name; }
    public int Level { get => _level; }
    public Sprite Sprite { get => _sprite; }
    public List<deplacement> Pattern { get => _pattern; }
    public bool IsWhite { get => _isWhite;}
    public bool CanFuse { get => canFuse; }
}