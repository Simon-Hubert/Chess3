using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] PieceSelection ps;
    [SerializeField] Piece thisPiece;
    SpriteRenderer sr;
    [SerializeField]PieceAnimation pa;

    public PieceSelection Ps { get => ps; set => ps = value; }
    public Material Material { get => material; set => material = value; }
    public Piece ThisPiece { get => thisPiece; set => thisPiece = value; }

    private void Awake() {
        material = new Material(material);
        sr = GetComponent<SpriteRenderer>();
        pa = GetComponent<PieceAnimation>();
    }

    private void OnEnable() {
        Ps.OnSelected += ShowOutline;
        Ps.OnDeSelected += HideOutline;
        pa.onAnimBegin +=ShowOutline;
        pa.onAnimEnd += HideOutline;
    }

    private void OnDisable() {
        Ps.OnSelected -= ShowOutline;
        Ps.OnDeSelected -= HideOutline;
        pa.onAnimBegin -=ShowOutline;
        pa.onAnimEnd -= HideOutline;
    }

    

    private void Start() {
        sr.material = Material;
    }



    public void ShowOutline(){
        Material.SetFloat("_Outline",1);
    }

    public void HideOutline(){
        Material.SetFloat("_Outline",0);
    }
}
