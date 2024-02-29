using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RenfortPiece : MonoBehaviour
{
    enum DIRECTION
    {
        NORD,
        SUD,
        EST,
        OUEST
    }
    GameObject visuel;
    [SerializeField] DIRECTION dir;
    delegate void FromDir(GameObject visual, float l);
    FromDir[] dirArray = { FromNorth, FromSouth, FromEast, FromWest };
    
    private void Awake()
    {
        visuel = transform.Find("Visual").GetComponentInChildren<SpriteRenderer>().gameObject;
        GetDir(dir)(visuel, -1); // 1 étant la hauteur d'une tile
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StartCoroutine(Arriving());
    }

    IEnumerator Arriving()
    {
        float elapsedTime = 0;
        while(elapsedTime < 1f)
        {
            //visuel.transform.position = new Vector2(visuel.transform.position.x, Mathf.Lerp(startPos.y, endPos.y, elapsedTime));
            GetDir(dir)(visuel, Mathf.Lerp(-1f, 0f, elapsedTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }

    private FromDir GetDir(DIRECTION dir)
    {
        return dirArray[(int)dir];
    }

    private static void FromNorth(GameObject visual, float l)
    {
        visual.transform.localPosition = new Vector2(visual.transform.localPosition.x, - l) ; // 1 étant la hauteur d'une tile

    }
    private static void FromSouth(GameObject visual, float l)
    {
        visual.transform.localPosition = new Vector2(visual.transform.localPosition.x, l); // 1 étant la hauteur d'une tile

    }
    private static void FromEast(GameObject visual, float l)
    {
        visual.transform.localPosition = new Vector2(l, visual.transform.localPosition.y); // 1 étant la hauteur d'une tile


    }
    private static void FromWest(GameObject visual, float l)
    {
        visual.transform.localPosition = new Vector2( - l, visual.transform.localPosition.y); // 1 étant la hauteur d'une tile
    }
}
