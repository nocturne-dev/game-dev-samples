using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    private int score = 0;

    public static GameControl instance; //allow "instance" to be accessible easily for any other class
    public GameObject gameOverText;
    public bool gameOver = false;
    public float scrollSpeed = -1.5f;   //speed of the background
    public Text scoreText;

	// Use this for initialization
	void Awake () { //precedes Start(); Awake() always starts first
		if(instance == null)    //if no other GameControl is active
        {
            instance = this;
        }
        else if(instance != this) //if "instance" exists, but not as "this"
        {
            Destroy(gameObject); //"gameObject" will be destroyed, since there exists another gameObject 
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (gameOver == true && Input.GetMouseButtonDown(0)) //if the game is over, and the player left clicks
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   //reloads the scene that is currently active, which happens to be Main
        }
	}

    public void BirdScored()    //whenever player passes through the columns without fail
    {
        if (gameOver)
        {
            return; //skip this function and don't do anything else
        }
        score++;    //update score
        scoreText.text = "Score: " + score.ToString();  //update display text of score
    }

    public void BirdDied() 
    {
        gameOverText.SetActive(true);   //enables game over message
        gameOver = true;
    }
}
