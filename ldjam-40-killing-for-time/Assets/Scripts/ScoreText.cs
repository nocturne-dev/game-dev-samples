using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    public Text LevelText;

    int level;

	// Use this for initialization
	void Start () {
        level = 0;
        InvokeRepeating("LevelUP", 0.0f, 10.0f);
	}

    private void Update()
    {
        if (GameOver.stopLeveling)
        {
            CancelInvoke("LevelUP");
            GameOver.stopLeveling = false ;
        }
    }

    // Increase the level every few seconds
    void LevelUP()
    {
        level++;
        LevelText.text = "Level: " + level;
    }
}
