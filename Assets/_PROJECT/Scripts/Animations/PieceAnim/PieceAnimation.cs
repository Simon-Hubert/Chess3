using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PieceAnimation : MonoBehaviour
{   
    Movements move;

    public event Action onAnimBegin;
    public event Action onAnimEnd;

    public UnityEvent m_onAnimBegin;
    public UnityEvent m_onAnimEnd;

    private void Awake() {
        move = GetComponentInParent<Movements>();
    }

    private void OnEnable() {
        move.OnMoveThis += MoveTo;
    }

    private void OnDisable() {
        move.OnMoveThis -= MoveTo;
    }

    private void MoveTo(Vector2 to){
        PlayMoveAnim(transform.position, to);
    }

    public abstract void PlayMoveAnim(Vector2 from, Vector2 to);

    protected void RaiseOnAnimBegin(){
        onAnimBegin?.Invoke();
        m_onAnimBegin.Invoke();
    }

    protected void RaiseOnAnimEnd(){
        onAnimEnd?.Invoke();
        m_onAnimEnd.Invoke();
    }
}
