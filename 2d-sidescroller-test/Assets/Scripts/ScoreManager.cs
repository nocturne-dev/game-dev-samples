using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private float timeLeft = 120;
    private int score = 0;
    private Text timeLeftText;
    private Text scoreText;

    public GameObject timeLeftUI;
    public GameObject scoreUI;
    
    
    

	// Use this for initialization
	void Start () {
        timeLeftText = timeLeftUI.GetComponent<Text>();
        scoreText = scoreUI.GetComponent<Text>();

        //just for testing
        DataManagement.data_management.LoadData();
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        timeLeftText.text = "Time Left: " + (int)timeLeft;
        scoreText.text = "Score: " + score;
        if (timeLeft < 0.1f)
        {
            SceneManager.LoadScene("Main");
        }
	}

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End"))
        {
            CountScore();
            DataManagement.data_management.SaveData();
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            score += 10;
            Destroy(collision.gameObject);
        }
    }

    void CountScore()
    {
        score += (int)timeLeft * 10;
        DataManagement.data_management.highScore = score;
        DataManagement.data_management.SaveData();
        Debug.Log("Now that we have added the score to DataManagement, Data says high score is currently " +  DataManagement.data_management.highScore);
    }
}
