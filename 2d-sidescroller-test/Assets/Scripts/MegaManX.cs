using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaManX : MonoBehaviour {

    public bool isGrounded;
    public float jumpPower = 1250f; //test variable
    public float speed = 10f;

    private bool facingRight = false;
    private float moveX;
    private Rigidbody2D rb2d;
    private Transform ts;
    

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        ts = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        PlayerMove();
        PlayerRaycast();
	}

    //test function
    void PlayerMove()
    {
        //controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            PlayerJump();
        }

        //player direction
        if(moveX < 0.0f && !facingRight)
        {
            PlayerFlip();
        }
        
        else if(moveX > 0.0f && facingRight)
        {
            PlayerFlip();
        }

        //physics
        rb2d.velocity = new Vector2(moveX * speed, rb2d.velocity.y);
    }

    //jumping code
    void PlayerJump()
    {
        rb2d.AddForce(Vector2.up * jumpPower);
        isGrounded = false;
    }
    
    //flips player horizontally
    void PlayerFlip()
    {
        facingRight = !facingRight;
        Vector2 localScale = ts.localScale;
        localScale.x *= -1; //literally flips the sprite horizontally
        ts.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    //throws a line between objects
    //tests to see if objects do collide with each other
    //draws a line from center of object to direction based on Raycast
    void PlayerRaycast()
    {
        //Ray Up
        RaycastHit2D rayUp = Physics2D.Raycast(transform.position, Vector2.up);
        if(rayUp.collider != null && rayUp.distance < 0.35f && rayUp.collider.CompareTag("Box"))
        {
            Destroy(rayUp.collider.gameObject);
        }

        //Ray Down
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
        if(rayDown.collider != null && rayDown.distance < 0.2f && rayDown.collider.CompareTag("Enemy"))
        {
            rb2d.AddForce(Vector2.up * 100);
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 200);
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 2;
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            rayDown.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rayDown.collider.gameObject.GetComponent<Enemy>().enabled = false;
            
            //Destroy(hit.collider.gameObject);
        }

        if (rayDown.collider != null && rayDown.distance < 0.2f && !rayDown.collider.CompareTag("Enemy"))
        {
            isGrounded = true;
        }

    }
}
