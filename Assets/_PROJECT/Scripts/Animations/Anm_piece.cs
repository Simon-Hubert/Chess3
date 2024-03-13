using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Anm_piece : MonoBehaviour
{
    [SerializeField] AnimationCurve elevation;
    [SerializeField] AnimationCurve anim;
    [SerializeField] float duration;
    [SerializeField] float maxHeight;
    static Anm_piece instance;

    private void Awake() {
        instance = this;
    }

    public static void Move(Vector3 from, Vector3 to, GameObject go){
        GameObject g = go.transform.Find("Visual").gameObject;
        instance.StartCoroutine(Moving(from, to, g));
    } 

    static IEnumerator Moving(Vector3 from, Vector3 to, GameObject go){
        go.transform.position = from;
        float t = 0;
        while(t<instance.duration){
            go.transform.position = Vector3.Lerp(from,to, instance.anim.Evaluate(t/instance.duration));
            go.transform.position += new Vector3(0, instance.maxHeight * instance.elevation.Evaluate(t/instance.duration),0);
            t += Time.fixedDeltaTime;
            yield return null;
        }
        go.transform.position = to;
    }
}
