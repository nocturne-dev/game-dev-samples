using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    static public bool enemyHit;    //if the player hits the enemy, gain life
    static public bool playerHit;   //if the player is hit by the enemy, lose life
    static public bool isDead;

    public float healthFloat;
    public PlayerController Player;
    public Text healthText;

    float level;
    int healthInt;

    private void Start()
    {
        healthText.text = "";
        enemyHit = false;
        playerHit = false;
        isDead = false;
        level = 0.0f;

        InvokeRepeating("LevelUp", 0.0f, 10.0f);
    }

    // Update is called once per frame
    void Update () {

        if(healthFloat > 0.0f)
        {
            healthFloat -= Time.deltaTime;
        }
        else
        {
            healthFloat = 0.0f;
        }

        if (enemyHit)
        {
            Debug.Log("Enemy Hit!");
            healthFloat += 1.0f;
            enemyHit = false;
        }

        else if (playerHit)
        {
            Debug.Log("Player has been hit!");
            healthFloat -= (level * 2);
            playerHit = false;
        }

        healthInt = (int)healthFloat;

        string healthString = healthInt.ToString();

        healthText.text = healthString;

        Vector2 timePointsText = Camera.main.WorldToScreenPoint
            (new Vector2(transform.position.x, 
            transform.position.y + 1.0f));
        healthText.transform.position = timePointsText;

        //if the player's health reaches zero, play death animation,
        //disable it, and then destroy it
        if(healthFloat == 0f && !isDead)
        {
            healthText.gameObject.SetActive(false);
            isDead = true;
        }
	}

    void LevelUp()
    {
        level++;
        Debug.Log("Current Level: " + level);
    }
}
