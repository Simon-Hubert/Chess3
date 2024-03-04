using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Teleporter teleporter;
    [SerializeField] float timeLimit = 2.0f;
    [SerializeField] AnimationCurve speed = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    Tile thisTile;
    [SerializeField] UnityEvent onTeleport;
    private void Awake()
    {
        thisTile = GetComponent<Tile>();
    }
    private void OnEnable()
    {
        thisTile.onMovedTo += Teleport;
    }
    private void OnDisable()
    {
        thisTile.onMovedTo -= Teleport;
    }

    public void Teleport(GameObject g)
    {
        
    }
    IEnumerator Teleportation(GameObject g)
    {

        float elapsedTime = 0f;
        Vector2 pos = g.transform.position;
        while(elapsedTime < timeLimit)
        {
            if(pos.y < 8)
            {
                pos.y = Mathf.Lerp(thisTile.transform.position.y, teleporter.transform.position.y, speed.Evaluate(elapsedTime));
                g.transform.position = new Vector2(g.transform.position.x, pos.y);
                elapsedTime += Time.deltaTime;
            }
        }
        return null;
    }
}
