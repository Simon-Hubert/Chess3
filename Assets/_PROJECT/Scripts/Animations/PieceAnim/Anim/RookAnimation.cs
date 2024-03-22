using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;


public class RookAnimation : PieceAnimation
{

    Vector2 xp, y, yd;
    [SerializeField] float f, zeta, r, duration;
    float k1, k2, k3;

    Piece thisPiece;

    public override void PlayMoveAnim(Vector2 from, Vector2 to)
    {
        RaiseOnAnimBegin();
        StartCoroutine(RookMovement());
    }
    private void Start() {
        thisPiece = GetComponentInParent<Piece>();
        InitMovement(thisPiece.transform.position);
    }
    
    private void FixedUpdate() {
        
        transform.position = UpdateMovements(thisPiece.transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0,0,(yd.x/20)*30));
    }

    IEnumerator RookMovement(){
        yield return new WaitForSeconds(duration);
        RaiseOnAnimEnd();
    }

    private void InitMovement(Vector2 from){
        k1 = zeta/(PI * f);
        k2 = 1/(2*PI*f * 2*PI*f);
        k3 = r*zeta / (2*PI*f);

        xp = from;
        y = from;
        yd = new Vector2(0f,0f);
    }

    private Vector2 UpdateMovements(Vector2 x){
        float T = Time.fixedDeltaTime;

        Vector2 xd = (x-xp)/T;
        xp = x;
        y += T*yd;
        yd += T*(x + k3*xd - y - k1*yd)/k2;
        return y;
    }
}
