using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anm_Grid : MonoBehaviour
{
    Grid grid;
    [SerializeField] AnimationCurve speed;
    private void OnValidate()
    {
        grid = FindAnyObjectByType<Grid>();
        if(grid == null )
        {
            Debug.LogWarning("oh tié fada, y'a pas de grid dans la scène mon gaté");
        }
    }
    private void Start()
    {
        foreach(Tile tile in grid.GetComponentsInChildren<Tile>())
        {
            GameObject visual = tile.GetComponentInChildren<SpriteRenderer>().gameObject;
            visual.transform.position = new Vector2(visual.transform.position.x, visual.transform.position.y + 10.5f);
        }
        StartCoroutine(AnimateTiles());
    }

    private IEnumerator AnimateTiles()
    {
        float t = 1;
        foreach (Tile tile in grid.GetComponentsInChildren<Tile>())
        {
            GameObject visual = tile.GetComponentInChildren<SpriteRenderer>().gameObject;
            Vector2 endPos = new Vector2(visual.transform.position.x, visual.transform.position.y - 10.5f);
            Vector2 startPos = new Vector2(visual.transform.position.x, visual.transform.position.y + 10.5f);
            //visual.transform.position = new Vector2(visual.transform.position.x, 5.5f);
            yield return StartCoroutine(AnimTile(startPos.y, endPos.y, visual.transform));
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

        // Ajoutez un petit délai entre chaque animation
        //yield return new WaitForSeconds(0.1f);
    }
}
