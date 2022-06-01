using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    static public bool stopLeveling;

    public GameObject Player;
    public GameObject Restart;
    
    Animator anim;
    bool flag;
    bool playerChoice;
    SpriteRenderer sr;
    Text quitGame;
    Text restart;

    private void Start()
    {
        stopLeveling = false;
    }

    // Use this for initialization
    private void OnEnable()
    {
        anim = Player.GetComponent<Animator>();
        sr = Player.GetComponent<SpriteRenderer>();
        restart = Restart.GetComponent<Text>();
        quitGame = GetComponent<Text>();
        Invoke("RestartGame", 5.0f);
        playerChoice = false;
        flag = false;
        stopLeveling = true;
    }

    // Update is called once per frame
    void Update () {

		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Game Over")
            && !flag)
        {
            sr.enabled = false;
            anim.enabled = false;
            flag = true;
        }

        //allow the player to either restart
        //or quit the game
        if (playerChoice)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
            
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                quitGame.text = "Thank you for playing! \nCreated by Frankii Tang";
                Invoke("QuitGame", 3.0f);
            }
        }
	}

    void RestartGame() {
        Restart.SetActive(true);
        restart.text = "Press 'R' to restart the game, \nOr 'ESC' to quit.";
        playerChoice = true;
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
     }
}
