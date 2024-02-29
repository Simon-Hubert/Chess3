using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/NewDialogueContainer")]
public class DialoguePrompt : ScriptableObject
{
    public string _speakerName;

    [TextArea(5,10)]
    public string[] _paragraphs;
}
