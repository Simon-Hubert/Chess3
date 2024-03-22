using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimation : PieceAnimation
{
    [SerializeField] float duration;
    [SerializeField] AnimationCurve elevation;
    [SerializeField] AnimationCurve anim;
    [SerializeField] float maxHeight;

    public override void PlayMoveAnim(Vector2 from, Vector2 to)
    {
        StartCoroutine(KnightMove(from, to));
        IEnumerator KnightMove(Vector2 from, Vector2 to){
            RaiseOnAnimBegin();
            float elapsedTime = 0;
            transform.position = from;
            while(elapsedTime <=duration){
                transform.position = Vector3.Lerp(from,to, anim.Evaluate(elapsedTime/duration));
                transform.position += new Vector3(0, maxHeight * elevation.Evaluate(elapsedTime/duration),0);
                elapsedTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            transform.position = to;
            RaiseOnAnimEnd();
        }
    }
}
