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
    public Material outline;

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

        visual.transform.parent = piece.transform;
        brain.transform.parent = piece.transform;

        SpriteRenderer sr = visual.AddComponent<SpriteRenderer>();
        Piece p = piece.AddComponent<Piece>();
        PieceSelection ps = brain.AddComponent<PieceSelection>();
        Outline otl = visual.AddComponent<Outline>();
        
        otl.Ps = ps;
        otl.Material = outline;
        otl.ThisPiece = p;

        sr.sprite = pieceData.Sprite;
        sr.sortingOrder = 1;
        sr.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

        p.Data = pieceData;
        //------------------ADD BRAIN SCRIPTS -------------------------------
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
