using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renforts : MonoBehaviour
{
    [SerializeField] GameObject piecesParent;
    Rule_Escape ruleController;
    private void OnValidate()
    {
        if (piecesParent == null)
            Debug.LogWarning("He ! Tu peux mettre le gameobject PIECE dans pieceParent stp :,) sinon ça peut pas fonctionner haha");
    }
    private void Awake()
    {
        ruleController = GetComponent<Rule_Escape>();
        ruleController.OnInaction += CallRenfort;
        foreach (RenfortPiece renfort in piecesParent.GetComponents<RenfortPiece>())
        {
            renfort.gameObject.SetActive(false);
        }
    }
    void CallRenfort()
    {
        // active les pièces et play animtion
        foreach(RenfortPiece renfort in piecesParent.GetComponents<RenfortPiece>())
        {
            renfort.gameObject.SetActive(true);
        }
    }
}
