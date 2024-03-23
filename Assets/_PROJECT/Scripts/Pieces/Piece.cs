using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Movements))]
public class Piece : MonoBehaviour
{
    [SerializeField] bool isWall = false;
    [SerializeField] PieceData _data;
    List<PieceData.deplacement> _patternSave;
    Grid grid;
    Movements movement;
    Eating eatS;
    bool isAlive = true;
    public UnityEvent m_OnDeath; 

    public Vector3Int Coords { get => grid.WorldToCell(transform.position); }
    public PieceData Data { get => _data; set => _data = value; }
    public Movements Movement { get => movement; set => movement = value; }
    public Eating EatS { get => eatS; set => eatS = value; }
    public bool IsWall { get => isWall;}
    public List<PieceData.deplacement> PatternSave { get => _patternSave;}
    public bool IsAlive { get => isAlive; set => isAlive = value; }

    private void Awake()
    {
        _patternSave = _data.Pattern.ToList();
        eatS = GetComponentInChildren<Eating>();
        movement = transform.GetComponent<Movements>();
        grid = FindObjectOfType<Grid>();
    }

    public void Destroy(){
        m_OnDeath.Invoke();
        IsAlive = false;
        StartCoroutine(WaitForDestroy());
    }

    IEnumerator WaitForDestroy(){
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }

}
