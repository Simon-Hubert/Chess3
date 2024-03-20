using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] PieceSelection ps;
    [SerializeField] Piece thisPiece;
    SpriteRenderer sr;

    public PieceSelection Ps { get => ps; set => ps = value; }
    public Material Material { get => material; set => material = value; }
    public Piece ThisPiece { get => thisPiece; set => thisPiece = value; }

    private void Awake() {
        material = new Material(material);
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        Ps.OnSelected += ShowOutline;
        Ps.OnDeSelected += HideOutline;
    }

    private void OnDisable() {
        Ps.OnSelected -= ShowOutline;
        Ps.OnDeSelected -= HideOutline;
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
