using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    public Enemy EnemyOne;
    public float healthFloat;
    public Text healthTextPrefab;

    bool didHit;    //if the enemy hits someone, gain life
    bool hasBeenHit;   //if the enemy is hit by someone, lose life
    int healthInt;
    Text healthText;

    private void Start()
    {
        healthText = Instantiate(healthTextPrefab) as Text;
        healthText.transform.SetParent(GameObject.Find("Canvas").transform, false);
        healthText.text = "";
        HIT = false;
        HURT = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (healthFloat > 0.0f)
        {
            healthFloat -= Time.deltaTime;
        }
        else
        {
            healthFloat = 0.0f;
        }

        //IN PROGRESS
        /*
        if (HIT)
        {
            healthFloat += 1.0f;
            HIT = false;
        }
        */

        /*else*/ if (HURT)
        {
            healthFloat -= 1.0f;
            HURT = false;
        }

        healthInt = (int)healthFloat;

        string healthString = healthInt.ToString();

        healthText.text = healthString;

        Vector2 timePointsText = Camera.main.WorldToScreenPoint
    (new Vector2(transform.position.x,
    transform.position.y + 1.0f));
        healthText.transform.position = timePointsText;

        //if the enemy's health reaches zero, disable it,
        //and then destroy it
        if (healthFloat == 0f)
        {
            EnemyOne.gameObject.SetActive(false);
            healthText.gameObject.SetActive(false);
        }
    }

    //get-set for when the enemy hits something
    public bool HIT
    {
        get
        {
            return didHit;
        }

        set
        {
            didHit = value;
        }
    }

    //get-set for when the enemy is hit
    public bool HURT
    {
        get
        {
            return hasBeenHit;
        }

        set
        {
            hasBeenHit = value;
        }
    }

    //destroy the health Text when this object is disabled
    private void OnDisable()
    {
        if(healthText.gameObject != null)
        {
            Destroy(healthText.gameObject, Enemy.disappear);
        }

        Destroy(gameObject, Enemy.disappear);
    }
}
