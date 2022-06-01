using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour {

    private BoxCollider2D groundCollider;
    private float groundHorizontalLength;

	// Use this for initialization
	void Start () {
        groundCollider = GetComponent<BoxCollider2D>(); //get component reference
        groundHorizontalLength = groundCollider.size.x; //get the x-axis component of groundCollider
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -groundHorizontalLength) //if one of the objects is scrolled to its full length to the left
        {
            RepositionBackground();
        }
	}

    private void RepositionBackground()
    {
        Vector2 groundOffset = new Vector2(groundHorizontalLength * 2f, 0);   //how far to move our ground forward to reposition it
        transform.position = (Vector2)transform.position + groundOffset;    //actually move the game object by groundOffset
    }
}
