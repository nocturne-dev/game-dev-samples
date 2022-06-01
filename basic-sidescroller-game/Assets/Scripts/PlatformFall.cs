using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour {

    public float fallDelay = 1f;

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) ;
        Invoke("Fall", fallDelay);
    }

    void Fall()
    {
        rb2d.isKinematic = false;
    }
}
