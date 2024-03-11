using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Npc, Italkable
{
    [SerializeField] private DialoguePrompt _convo;
    [SerializeField] private DialogueController _dialogueController;
    public override void Interact()
    {
        Talk(_convo);
    }

    public void Talk(DialoguePrompt convo)
    {
        _dialogueController.DisplayNextParagraph(convo);
    }
}
