using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {

    public InputField inputField;

    GameController controller;

    private void Awake()
    {
        controller = GetComponent<GameController>();
        inputField.onEndEdit.AddListener(AcceptStringInput);    //function will be called whenever user hits return or clicks on another element
    }

    void AcceptStringInput(string userInput)

    {
        userInput = userInput.ToLower();    //sets all text to lower case
        controller.LogStringWithReturn(userInput);  //take input and display it back to player

        char[] delimiterCharacters = { ' ' };
        string[] separatedInputWords = userInput.Split(delimiterCharacters);

        for(int i = 0; i < controller.inputActions.Length; i++)
        {
            InputAction inputAction = controller.inputActions[i];
            if(inputAction.keyWord == separatedInputWords[0])
            {
                inputAction.RespondToInput(controller, separatedInputWords);
            }
        }

        InputComplete();
    }

    void InputComplete()
    {
        controller.DisplayLoggedText();
        inputField.ActivateInputField();
        inputField.text = null;
    }
}
