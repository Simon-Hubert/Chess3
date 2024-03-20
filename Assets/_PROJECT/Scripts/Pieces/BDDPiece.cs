using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;

[CreateAssetMenu (menuName = "DatabasePions")]
public class BDDPiece: ScriptableObject
{
    public List<PieceData> pieces;
    public Material Outline;

    #if UNITY_EDITOR
    [Button]
    public void GeneratePrefabs()
    {
        foreach (PieceData piece in pieces)
        {
            GeneratePrefab(piece);
        }
        AssetDatabase.SaveAssets();
    }

    private void GeneratePrefab(PieceData pieceData)
    {
        GameObject piece = new GameObject(pieceData.Name);
        GameObject visual = new GameObject("Visual");
        GameObject brain = new GameObject("Brain");
        //GameObject highlight = new GameObject("Highlight");

        visual.transform.parent = piece.transform;
        brain.transform.parent = piece.transform;
        //highlight.transform.parent = piece.transform;
        
        visual.AddComponent<SpriteRenderer>().sprite = pieceData.Sprite;
        visual.GetComponent<SpriteRenderer>().sortingOrder = 1;
        visual.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        // highlight.AddComponent<SpriteRenderer>().sprite = pieceData.Sprite;
        // highlight.GetComponent<SpriteRenderer>().sortingOrder = 1;
        // highlight.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        // highlight.GetComponent<SpriteRenderer>().material = Outline;
        piece.AddComponent<Piece>().Data = pieceData;

        //highlight.SetActive(false);
        //------------------ADD BRAIN SCRIPTS -------------------------------
        brain.AddComponent<PieceSelection>();
        if(pieceData.IsWhite)
        {
            brain.AddComponent<PlayerMovement>();
        }
        else
        {
            brain.AddComponent<EnemyMovement>();
        }
            brain.AddComponent<Eating>();


        PrefabUtility.SaveAsPrefabAsset(piece, "Assets/_PROJECT/Prefabs/Pieces/" + piece.name + ".prefab");
        GameObject.DestroyImmediate(piece);

    }
    #endif
}
