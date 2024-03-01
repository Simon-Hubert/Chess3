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
        
        visual.AddComponent<SpriteRenderer>().sprite = pieceData.Sprite;
        visual.GetComponent<SpriteRenderer>().sortingOrder = 1;
        piece.AddComponent<Piece>().Data = pieceData;
        brain.AddComponent<PieceSelection>();

        //------------------ADD BRAIN SCRIPTS -------------------------------
        piece.AddComponent<Movements>();
        if(pieceData.IsWhite)
        {
            brain.AddComponent<PlayerMovement>();
        }
        else
        {
            brain.AddComponent<EnemyMovement>();
        }
            brain.AddComponent<Eat>();


        PrefabUtility.SaveAsPrefabAsset(piece, "Assets/_PROJECT/Prefabs/Pieces/" + piece.name + ".prefab");
        GameObject.DestroyImmediate(piece);

    }
}
