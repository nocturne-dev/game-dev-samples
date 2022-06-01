using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int speed;
    public int xMove;

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMove, 0));
        rb2d.velocity = new Vector2(xMove, 0) * speed;
        if(hit.distance < 0.2f)
        {
            Flip();
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
	}

    void Flip()
    {
        if(xMove > 0)
        {
            xMove = -1;
        }
        else
        {
            xMove = 1;
        }
    }
}
