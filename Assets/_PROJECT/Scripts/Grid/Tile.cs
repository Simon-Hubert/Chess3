using System.ComponentModel;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool whiteTile;
    [SerializeField] Grid grid;
    [SerializeField] GridSettings gridSettings;
    [SerializeField] bool highlighted;
    [SerializeField] SpriteRenderer HighlightRenderer;
    bool moveToAble;
    
    public Vector3Int Coords {get => grid.WorldToCell(transform.position);}
    public bool Highlighted { get => highlighted; set => highlighted = value; }
    public bool MoveToAble { get => moveToAble;}

    public void onPaint(){
        if((Coords.x + Coords.y)%2 == 0){
            whiteTile = gridSettings.Inverted;
        }
        else{
            whiteTile = !gridSettings.Inverted;
        }

        //Set le Sprite
        if(!whiteTile){
            transform.Find("Visuel").GetComponentInChildren<SpriteRenderer>().color = Color.black;
        }
        else{
            transform.Find("Visuel").GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
        HighlightRenderer.enabled = false;
    }

    private void Awake() {
        Highlighted = false;
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
}
