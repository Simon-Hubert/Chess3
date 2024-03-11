using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Teleporter teleporter;
    [SerializeField] float timeLimit = 2.0f;
    [SerializeField] AnimationCurve speed = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [SerializeField] Tile thisTile;
    [SerializeField] UnityEvent onTeleport;
    [SerializeField] Animator portal;
    [SerializeField] GameObject Portal;

    public Teleporter TP { get => teleporter; private set => teleporter = value; }

    private void Awake()
    {
        Portal = GameObject.Find("Teleporteur");
        portal = Portal.GetComponent<Animator>();
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

    void Teleport(GameObject g)
    {
        GameObject visual = g.transform.Find("Visual").gameObject;
        StartCoroutine(Teleportation(visual));
    }
    IEnumerator Teleportation(GameObject g)
    {
        bool a = false;
        bool b = false;
        float elapsedTime = 0f;
        float e2 = 0f;
        Vector2 pos = g.transform.position;
        Portal.transform.position = new Vector2(transform.position.x, Portal.transform.position.y);
        portal.SetTrigger("Opening");
        while(elapsedTime < timeLimit)
        {
            if (pos.y > 8 && !b)
            {
                Debug.Log("if");
                portal.SetTrigger("Closing");
                g.transform.position = new Vector2(teleporter.transform.position.x, g.transform.position.y);
                Portal.transform.position = new Vector2(teleporter.transform.position.x, Portal.transform.position.y);
                a = true;
                b = true;
            }
            if (a && b)
            {
                Debug.Log("else");
                pos.y = Mathf.Lerp(g.transform.position.y, teleporter.transform.position.y, speed.Evaluate(e2));
                g.transform.position = new Vector2(g.transform.position.x, pos.y);
                e2 += Time.deltaTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            else 
            {
                Debug.Log("if2");
                pos.y = Mathf.Lerp(thisTile.transform.position.y, 8.5f, speed.Evaluate(elapsedTime));
                g.transform.position = new Vector2(thisTile.transform.position.x, pos.y);
                elapsedTime += Time.deltaTime;
                yield return null;
            }


        }
    }
}
