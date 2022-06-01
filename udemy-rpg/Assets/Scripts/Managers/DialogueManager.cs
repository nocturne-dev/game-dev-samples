using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject nameBox;
    [SerializeField] Text dialogueText;
    [SerializeField] Text nameText;

    DialogueState currentState;
    bool dialogueInProgress;
    bool markQuestComplete;
    bool shouldMarkQuest;
    int dialogueIndex;
    string questToMark;

    string[] dialogueLines;

    private void Start()
    {
        dialogueIndex = 0;
        dialogueInProgress = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueBox.activeInHierarchy 
            && CrossPlatformInputManager.GetButtonUp("Fire1") 
            && FindObjectOfType<GameManager>().GetDialogueActive())
        { 
            ContinueDialogue(); 
        }
    }

    private void ContinueDialogue()
    {
        if (dialogueInProgress)
        {
            dialogueIndex++;

            if (dialogueIndex >= dialogueLines.Length)
            {
                dialogueIndex = 0;
                dialogueInProgress = false;
                dialogueBox.SetActive(false);
                nameBox.SetActive(false);
                FindObjectOfType<GameManager>().SetDialogueActive(false);

                if (shouldMarkQuest)
                {
                    shouldMarkQuest = false;
                    if (markQuestComplete)
                    {
                        FindObjectOfType<QuestManager>().MarkQuestComplete(questToMark);
                    }
                    else
                    {
                        FindObjectOfType<QuestManager>().MarkQuestIncomplete(questToMark);
                    }
                }
                return;
            }

            dialogueText.text = dialogueLines[dialogueIndex];
        }

        else 
        { 
            dialogueInProgress = true; 
        }
    }

    public void DialogueStart()
    {
        if (nameText.text.Length <= 0) 
        { 
            nameBox.SetActive(false); 
        }

        else { 
            nameBox.SetActive(true); 
        }

        dialogueBox.SetActive(true);
        dialogueText.text = dialogueLines[dialogueIndex];
        FindObjectOfType<GameManager>().SetDialogueActive(true);
    }
    public void SetNameText(string s) 
    { 
        nameText.text = s; 
    }

    public void SetDialogueState(DialogueState ds) 
    { 
        if(ds == null) 
        { 
            return; 
        }

        currentState = ds;
        dialogueIndex = 0;
        dialogueLines = currentState.GetDialogue();
    }

    public void ShouldActivateQuestAtEnd(string questName, bool markComplete)
    {
        questToMark = questName;
        markQuestComplete = markComplete;

        shouldMarkQuest = true;
    }
}
