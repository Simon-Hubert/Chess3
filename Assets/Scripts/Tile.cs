using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool whiteTile;
    [SerializeField] Grid grid;
    [SerializeField] GridSettings gridSettings;

    public void onPaint(){
        var coord = grid.LocalToCell(transform.position);
        
        if((coord.x + coord.y)%2 == 0){
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
    }

}
