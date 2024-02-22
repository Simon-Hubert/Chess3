using System.ComponentModel;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool whiteTile;
    [SerializeField] Grid grid;
    [SerializeField] GridSettings gridSettings;
    [SerializeField] bool highlighted;
    [SerializeField] SpriteRenderer HighlightRenderer;
    
    public Vector3Int Coords {get => grid.LocalToCell(transform.position);}
    public bool Highlighted { get => highlighted; set => highlighted = value; }


    public void onPaint(){
        
        if((Coords.x + Coords.y)%2 == 0){
            whiteTile = gridSettings.Inverted;
        }
        else{
            whiteTile = !gridSettings.Inverted;
        }

        //Set le Sprite
        if(!whiteTile){
            transform.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else{
            transform.GetComponent<SpriteRenderer>().color = Color.white;
        }
        HighlightRenderer.enabled = false;
    }

    private void Awake() {
        Highlighted = false;
    }

    private void FixedUpdate() {
        HighlightRenderer.enabled = Highlighted;
    }
}
