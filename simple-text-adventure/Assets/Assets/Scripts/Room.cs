using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ScriptableObjects are like Monobehavior but never attached to GameObjects
//can be used to create assets which store data or execute code
[CreateAssetMenu(menuName = "TextAdventure/Room")]  //create asset instances of this object
public class Room : ScriptableObject    
{

    [TextArea]  public string description;  //gives description a bigger text entry box
                public string roomName;
                public Exit[] exits;
                public InteractableObject[] interactableObjectsInRoom;

}
