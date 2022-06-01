using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   //displays exit inside inspector for ScriptableObjects
                        //System.Serializable lets you embed a class with sub properties in inspector
public class Exit {

    public string keyString;    //key words to look out for
    public string exitDescription;  //description of the player's actions
    public Room valueRoom;

}
