using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anm_Grid : MonoBehaviour
{
    int count = 0;
    GridManager gm;
    [SerializeField] AnimationCurve speed = AnimationCurve.Linear(0f,0f,1f,1f);
    [SerializeField, Range(0.01f, 0.1f)] float timeBetweenTiles = 0.01f;
    [SerializeField] GameObject piecesParent;
    List<Tile> tiles = new List<Tile>();

    private void OnValidate()
    {
        gm = FindObjectOfType<GridManager>();
        if(gm == null )
        {
            Debug.LogWarning("oh ti� fada, y'a pas de gridmanager dans la sc�ne mon gat�");
        }
    }
    private void Start()
    {
        gm = FindObjectOfType<GridManager>(); // je pense que le onValidate s'execute pas en build
        foreach(Piece piece in gm.Pieces)
        {
            GameObject visual = piece.transform.Find("Visual").GetComponent<SpriteRenderer>().gameObject;
            visual.transform.position = new Vector2(visual.transform.position.x, visual.transform.position.y + 15.5f);
        }
        foreach(Tile tile in gm.Tiles)
        {
            GameObject visual = tile.transform.Find("Visuel").GetComponent<SpriteRenderer>().gameObject;
            visual.transform.position = new Vector2(visual.transform.position.x, visual.transform.position.y + 15.5f);
            tiles.Add(tile);
        }
        count = tiles.Count;
        AnimateGrid();
    }

    public void AnimateGrid()
    {
        StartCoroutine(AnimateTiles());
    }
    private IEnumerator AnimateTiles()
    {
        for(int i = 0; i < count; i++)
        {
            Tile tile = tiles[UnityEngine.Random.Range(0,tiles.Count - 1)];
            GameObject visual = tile.transform.Find("Visuel").GetComponent<SpriteRenderer>().gameObject;
            Vector2 endPos = new Vector2(tile.transform.position.x, tile.transform.position.y);
            Vector2 startPos = new Vector2(visual.transform.position.x, visual.transform.position.y + UnityEngine.Random.Range(10.5f, 20.5f));
            StartCoroutine(AnimTile(startPos.y, endPos.y, visual.transform));
            tiles.Remove(tile);
            yield return new WaitForSeconds(timeBetweenTiles);
        }
        foreach (Piece piece in gm.Pieces)
        {
            GameObject visual = piece.transform.Find("Visual").GetComponent<SpriteRenderer>().gameObject;
            Vector2 endPos = new Vector2(visual.transform.position.x, visual.transform.position.y - 15.5f);
            Vector2 startPos = new Vector2(visual.transform.position.x, visual.transform.position.y + 15.5f);
            StartCoroutine(AnimTile(startPos.y, endPos.y, visual.transform));
            yield return new WaitForSeconds(timeBetweenTiles);
        }
    }

    IEnumerator AnimTile(float startPos, float targetPos, Transform obj)
    {
        float elapsedTime = 0;
        Vector2 pos = transform.position;
        while (elapsedTime <= 1f)
        {
            pos.y = Mathf.Lerp(startPos, targetPos, speed.Evaluate(elapsedTime));
            obj.position = new Vector2(obj.position.x, pos.y);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.position = obj.GetComponentInParent<Piece>().transform.position;
        obj.GetComponent<RookAnimation>()?.startAnimation();
    }
}
