#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Movements))]
public class MovementsEditor : Editor
{
    GameObject obj;

    private void OnEnable() {
        obj = ((Movements)target).gameObject;
    }

    public override void OnInspectorGUI()
    {
        GridManager gm = FindObjectOfType<GridManager>();
        Tile pos = gm?.GetTileAt(obj.transform.position);
        IMovementBrain brain = obj.GetComponentInChildren<IMovementBrain>();

        if(!gm){
            GUI.color = Color.yellow;
            EditorGUILayout.HelpBox("La scene doit contenir une Grid avec GridManager (Utilisez le prefab jvous en supplie)", MessageType.Error, true);
        }

        if(!pos){
            GUI.color = Color.yellow;
            EditorGUILayout.HelpBox("Cet object doit se trouver sur la grille voyons !", MessageType.Error, true);
        }

        if(brain == null){
            GUI.color = Color.yellow;
            EditorGUILayout.HelpBox("Je n'ai pas de cerveau (faut mettre PlayerMovement ou EnemyMovement dans le Brain)", MessageType.Error, true);
        }
        GUI.color = Color.white;

        if(GUILayout.Button("Myturn")){
            obj.GetComponent<Movements>().Myturn = true;
        }
    }
}
#endif