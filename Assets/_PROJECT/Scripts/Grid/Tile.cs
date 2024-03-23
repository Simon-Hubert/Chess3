using System;
using System.ComponentModel;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    [SerializeField] bool whiteTile;
    [SerializeField] Grid grid;
    [SerializeField] GridSettings gridSettings;
    [SerializeField] bool highlighted;
    [SerializeField] SpriteRenderer HighlightRenderer;
    [SerializeField] Sprite whiteRenderer, blackRenderer, whiteHighlight, blackHighlight;
    [SerializeField] UnityEvent m_OnMovedTo;

    Material hlMaterial;
    GridManager gm;
    bool moveToAble;

    Color neutral = new Color(0.63f, 0.40f, 0.17f, 0.5f);
    Color enemy = new Color(0.86f, 0.12f, 0.07f, 0.5f);
    Color ally = new Color(0.38f, 0.70f, 0.20f, 0.5f);

    public event Action<GameObject> onMovedTo;
    public Vector3Int Coords {get => grid.WorldToCell(transform.position);}
    public bool Highlighted { get => highlighted; set => highlighted = value; }
    public bool MoveToAble { get => moveToAble;}
    public Grid Grid { get => grid; set => grid = value; }
    public GridSettings GridSettings { get => gridSettings; set => gridSettings = value; }

    public void onPaint(){
        if((Coords.x + Coords.y)%2 == 0){
            whiteTile = GridSettings.Inverted;
        }
        else{
            whiteTile = !GridSettings.Inverted;
        }

        //Set le Sprite
        if(!whiteTile){
            transform.Find("Visuel").GetComponentInChildren<SpriteRenderer>().sprite = blackRenderer;
            HighlightRenderer.sprite = blackHighlight;
        }
        else{
            transform.Find("Visuel").GetComponentInChildren<SpriteRenderer>().sprite = whiteRenderer;
            HighlightRenderer.sprite = whiteHighlight;
        }
        HighlightRenderer.enabled = false;
    }

    private void Awake() {
        Highlighted = false;
        gm = FindObjectOfType<GridManager>();
        hlMaterial = transform.Find("Highlight").GetComponent<SpriteRenderer>().material;
    }


    private void Update() {
        if(Highlighted) moveToAble = true;
    }

    private void FixedUpdate() {
        HighlightRenderer.enabled = Highlighted;
    }

    public void UnMoveToAble(){
        moveToAble = false;
    }
    public void OnMovedTo(GameObject g)
    {
        onMovedTo?.Invoke(g);
        m_OnMovedTo?.Invoke();
    }

    public void HighlightThis(){
        highlighted = true;
        Piece piece = gm.GetActivePieceAt(transform.position);
        if(piece != null && !piece.Data.IsWhite){
            hlMaterial.SetColor("_Tint", enemy);
            return;
        }
        if(piece != null && piece.Data.IsWhite){
            hlMaterial.SetColor("_Tint", ally);
            return;
        }
        hlMaterial.SetColor("_Tint", neutral);
    }

    #if UNITY_EDITOR
    public void SetUpNewShader(Material goodShader, Material goodShader2, Sprite goodSprite){
        HighlightRenderer.sprite = goodSprite; 
        HighlightRenderer.material = whiteTile ? goodShader : goodShader2;
    }
    #endif
}
