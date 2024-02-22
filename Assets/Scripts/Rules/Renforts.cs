using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renforts : MonoBehaviour
{
    [SerializeField] List<Piece> renforts;
    Rule_Escape ruleController;
    private void Awake()
    {
        ruleController = GetComponent<Rule_Escape>();
        ruleController.OnInaction += CallRenfort;
    }
    void CallRenfort()
    {
        // active les pièces et play animtion
    }
}
