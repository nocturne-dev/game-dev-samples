using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Interactables : MonoBehaviour
{
    [SerializeField] DialogueState dialogue;
    [Tooltip("TESTING"), SerializeField] QuestState quest;
    [SerializeField] bool markComplete;
    [SerializeField] string questToMark;
    [SerializeField] string targetName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<UIManager>().ActivateDialogueManager(true);
            FindObjectOfType<DialogueManager>().SetNameText(targetName);
            FindObjectOfType<DialogueManager>().SetDialogueState(dialogue);

            // TESTING
            if(quest != null)
            {
                FindObjectOfType<DialogueManager>().ShouldActivateQuestAtEnd(questToMark, markComplete);
                FindObjectOfType<QuestManager>().SetQuestNames(quest);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FindObjectOfType<UIManager>().ActivateDialogueManager(false);
        }
    }
}
