using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    public float upForce = 200; //variable that will cause the bird to move vertically up

    private bool isDead = false;    //check to see if player is dead
    private Rigidbody2D rb2d;
    private Animator anim;  //allows animations to play

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>(); //sets the component's RigidBody2D to this variable of RigidBody2D
        anim = GetComponent<Animator>();    //sets the component's Animator to this variable of Animator

	}
	
	// Update is called once per frame
	void Update () {
		
        if(isDead == false) //if the player is still alive
        {
            if (Input.GetMouseButtonDown(0))    //if the player has clicked left
            {
                rb2d.velocity = Vector2.zero;   //resets velocity
                rb2d.AddForce(new Vector2(0,upForce));  //causes the player to move vertically only
                anim.SetTrigger("Flap");    //plays the animation "Flap"
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isDead = true;  //when bird collides with something, it will die
        anim.SetTrigger("Die"); //plays the animation "Die"
        GameControl.instance.BirdDied();    //calls on BirdDied() from GameControl.cs
        rb2d.velocity = Vector2.zero;   //prevents any additional movement after death
    }
}
