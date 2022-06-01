using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text displayText;
    public InputAction[] inputActions;
    [HideInInspector]   public RoomNavigation roomNavigation;
    [HideInInspector]   public List<string> interactionDescriptionsInRoom = new List<string>();
    [HideInInspector]   public InteractableItems interactableItems;

    List<string> actionLog = new List<string>();

	// Use this for initialization
	void Awake () {
        roomNavigation = GetComponent<RoomNavigation>();
        interactableItems = GetComponent<InteractableItems>();
	}

    private void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());  //taking whole list of actions and turning them into one long string with new lines in it
        displayText.text = logAsText;
    }

    public void DisplayRoomText()
    {
        ClearCollectionsForNewRoom();
        UnpackRoom();
        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());
        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;
        LogStringWithReturn(combinedText);
    }

    void UnpackRoom()
    {
        roomNavigation.UnpackExitsInRoom();
        PrepareObjectToTakeOrExamine(roomNavigation.currentRoom);
    }

    void PrepareObjectToTakeOrExamine(Room currentRoom)
    {
        for(int i = 0; i < currentRoom.interactableObjectsInRoom.Length; i++)
        {
            string descriptionsNotInInventory = interactableItems.GetObjectNotInInventory(currentRoom, i);
            if(descriptionsNotInInventory != null)
            {
                interactionDescriptionsInRoom.Add(descriptionsNotInInventory);
            }

            InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom[i];

            for(int j = 0; j < interactableInRoom.interactions.Length; j++)
            {
                Interaction interaction = interactableInRoom.interactions[j];
                if(interaction.inputAction.keyWord == "examine")
                {
                    interactableItems.examineDictionary.Add(interactableInRoom.noun, interaction.textResponse);
                }
                if (interaction.inputAction.keyWord == "take")
                {
                    interactableItems.takeDictionary.Add(interactableInRoom.noun, interaction.textResponse);
                }
            }
        }
    }

    public string TextVerbDictionaryWithNoun(Dictionary<string, string> verbDictionary, string verb, string noun)
    {
        if (verbDictionary.ContainsKey(noun))
        {
            return verbDictionary[noun];
        }

        return "You can't " + verb + " " + noun;
    }

    void ClearCollectionsForNewRoom()
    {
        interactableItems.ClearCollections();
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();

    }

    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
