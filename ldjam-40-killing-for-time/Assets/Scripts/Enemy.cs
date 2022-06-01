using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //amount of time enemy will disappear from
    //the hierarchy when the gameObject is disabled
    static public float disappear = 3.0f;

    public EnemyHealth enemyhealth;
    public float speed; //speed of the enemy
    public GameObject shot;
    public int startFireChance;

    bool changePosition;
    float fireChanceTimer;
    int fireChance;
    Rigidbody2D rb2d;   //the rigidbody of the enemy
    Vector2 movement;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        changePosition = false;
        fireChance = startFireChance;
	}
	
	// Update is called once per frame
	void Update () {

        //set up when the enemy can fire a projectile
        fireChanceTimer += Time.deltaTime;
        fireChance = startFireChance - (int)fireChanceTimer;

        int fireProb = Random.Range(0, fireChance);

        if (fireProb == 50)
        {
            //Debug.Log("The enemy has fired!");

            //get distance between player and mouse position
            float dirX = Random.Range(-13, 14);
            float dirY = Random.Range(-13, 14);

            float posX = dirX - transform.position.x;
            float posY = dirY - transform.position.y;

            //get the angle between the two
            float angle = Mathf.Atan(posY / posX) * Mathf.Rad2Deg;

            if (dirX < transform.position.x)
            {
                angle += 180;
            }

            //instantiate where the projectile will be
            Vector2 shotPos = new Vector2(transform.position.x,
                transform.position.y);
            Quaternion shotRot = Quaternion.Euler(0f, 0f, angle - 90f);

            GameObject projectile = Instantiate(shot, shotPos, shotRot);
            projectile.tag = "Enemy";

            fireChanceTimer = 0.0f;
        }

    }

    private void FixedUpdate()
    {
        if(Timer.secondsPassed % 2 == 0 && !changePosition)
        {
            changePosition = true;
            //Debug.Log("Am I here yet?");
        }
        
        //moving the enemy
        if(changePosition && Timer.secondsPassed % 2 != 0)
        {
            //Debug.Log("Do I get here?");
            movement = Vector2.zero;
            float H = Random.Range(-1, 2);
            float V = Random.Range(-1, 2);
            movement = new Vector2(H, V);
            changePosition = false;
        }

        rb2d.velocity = movement * speed;
    }

    //if the enemy collides with something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //needed to differentiate between characters and weapons
        SpriteRenderer sr = collision.gameObject.
            GetComponent<SpriteRenderer>();

        //if the object is of weapon
        //do a secondary check to see if it's a player tag
        //another enemy's tag
        if (sr.sortingLayerName.Contains("Weapon")
            && collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.enemyHit = true;
            enemyhealth.HURT = true;
            Destroy(collision.gameObject);

        }
    }

    //Destroy this this object when it has been disabled
    private void OnDisable()
    {
        Destroy(gameObject, disappear);
    }
}
