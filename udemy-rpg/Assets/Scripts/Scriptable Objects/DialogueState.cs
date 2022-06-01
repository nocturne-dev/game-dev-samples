using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueState")]
public class DialogueState : ScriptableObject
{
    [TextArea(3, 5), SerializeField] string[] dialogText;

    public string[] GetDialogue()
    {
        return dialogText;
    }
}
