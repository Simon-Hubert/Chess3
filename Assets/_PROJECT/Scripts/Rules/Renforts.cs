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
            Debug.LogWarning("He ! Tu peux mettre le gameobject PIECE dans pieceParent stp :,) sinon ça peut pas fonctionner haha");
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager == null )
            Debug.LogWarning("Il n'y a pas de GridManager dans la scène");
        pieces.Clear();
        foreach (RenfortPiece renfort in gridManager.PiecesParent.GetComponentsInChildren<RenfortPiece>())
        {
            pieces.Add(renfort.gameObject);
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
        // active les pièces et play animtion
        foreach(GameObject renfort in pieces)
        {
            if(!renfort.gameObject.activeSelf)
            renfort.gameObject.SetActive(true);
        }
    }
}
