using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float shotSpawn;
    public float speed; //speed of the player
    public GameObject shot;
    public GameObject gameOver;

    Animator anim;
    bool lowerSpeed;
    CircleCollider2D cc2d;
    float speedDown;  //decrease speed every level
    Rigidbody2D rb2d;   //the rigidbody of the player

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        cc2d = GetComponent<CircleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();

        speedDown = 0.0f;
        lowerSpeed = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(!PlayerHealth.isDead)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //get mouse position
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //get distance between player and mouse position
                float posX = mousePos.x - transform.position.x;
                float posY = mousePos.y - transform.position.y;

                //Debug.Log("Mouse Pos X: " + posX + " , Mouse Pos Y: " + posY);

                //get the angle between the two
                float angle = Mathf.Atan(posY / posX) * Mathf.Rad2Deg;

                if (mousePos.x < transform.position.x)
                {
                    angle += 180;
                }

                //Debug.Log("Angle is: " + angle + " degrees...");

                //instantiate where the projectile will be
                Vector2 shotPos = new Vector2(transform.position.x + shotSpawn * Mathf.Cos(angle * Mathf.Deg2Rad),
                    transform.position.y + shotSpawn * Mathf.Sin(angle * Mathf.Deg2Rad));
                Quaternion shotRot = Quaternion.Euler(0f, 0f, angle - 90f);

                GameObject projectile = Instantiate(shot, shotPos, shotRot);
                projectile.tag = "Player";
            }
        }

        else
        {
            //prevent player controls
            enabled = false;
        }
        
	}

    private void FixedUpdate()
    {
        if(Timer.secondsPassed % 10 != 0 && lowerSpeed)
        {
            lowerSpeed = false;
        }

        if(Timer.secondsPassed % 10 == 0 && !lowerSpeed)
        {
            speedDown += 0.01f;
            lowerSpeed = true;
        }

        if (!PlayerHealth.isDead)
        {
            //Moving the player
            Vector2 movement = Vector2.zero;
            float H = Input.GetAxis("Horizontal");
            float V = Input.GetAxis("Vertical");
            movement = new Vector2(H, V);

            rb2d.velocity = movement * (speed - speedDown);
        }

        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    //if the player collides with something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //needed to differentiate between characters and weapons
        SpriteRenderer sr = collision.gameObject.
            GetComponent<SpriteRenderer>();

        //if the object is of projectile
        //do a secondary check to see if it's an enemy tag
        if (sr.sortingLayerName.Contains("Weapon")
            && collision.gameObject.CompareTag("Enemy"))
        {
            
            PlayerHealth.playerHit = true;
            //EnemyHealth.didHit = true;

            Destroy(collision.gameObject);
        }

    }

    //if the player is dead
    private void OnDisable()
    {
        cc2d.enabled = false;
        anim.SetTrigger("Dead");
        gameOver.SetActive(true);
    }
}
