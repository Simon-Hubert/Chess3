using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renforts : MonoBehaviour
{
    [SerializeField] GameObject piecesParent;
    Rule_Escape ruleController;
    [SerializeField]List<GameObject> pieces = new List<GameObject>();
    GridManager gridManager;

    private void OnValidate()
    {
        if (piecesParent == null)
            Debug.LogWarning("He ! Tu peux mettre le gameobject PIECE dans pieceParent stp :,) sinon �a peut pas fonctionner haha");
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager == null )
            Debug.LogWarning("Il n'y a pas de GridManager dans la sc�ne");
        pieces.Clear();
        foreach (Piece piece in gridManager.Pieces)
        {
            if(piece.GetComponentInChildren<RenfortPiece>()) pieces.Add(piece.gameObject);
        }
    }
    private void Awake()
    {
        ruleController = GetComponent<RuleController>().RuleEscape;
        ruleController.OnInaction += CallRenfort;
    }
    [Button]
    void CallRenfort()
    {
        // active les pi�ces et play animtion
        foreach(GameObject renfort in pieces)
        {
            if(!renfort.gameObject.activeSelf)
            renfort.gameObject.SetActive(true);
        }
    }
}
