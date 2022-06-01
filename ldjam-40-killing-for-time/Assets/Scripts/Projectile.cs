using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    static public float speed = 10.0f;

    bool ready; //ready to speed up

    Rigidbody2D rb2d;
    Vector2 startPos;

	// Use this for initialization
	void Start () {

        ready = false;

        rb2d = GetComponent<Rigidbody2D>(); //rigidbody of the projectile

        if (CompareTag("Player"))
        {
            //Debug.Log("Rotation: " + transform.rotation.z * 180);

            //get mouse position
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //get distance between projectile and mouse position
            float posX = mousePos.x - transform.position.x;
            float posY = mousePos.y - transform.position.y;

            //get the angle between the two
            float angle = Mathf.Atan(posY / posX) * Mathf.Rad2Deg;

            if (mousePos.x < transform.position.x)
            {
                angle += 180;
            }

            //Debug.Log("Firing Angle: " + angle);
            startPos = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            //Debug.Log("Firing Direction: " + startPos);
        }

        else if (CompareTag("Enemy"))
        {

            float rotZ = transform.eulerAngles.z + 90;
            if(rotZ > 180)
            {
                rotZ -= 360;
            }

            //Debug.Log("Rotation: " + rotZ);

            startPos = new Vector2(Mathf.Cos(rotZ * Mathf.Deg2Rad), 
                Mathf.Sin(rotZ * Mathf.Deg2Rad));
        }
    }
	
	// Update is called once per frame
	void Update () {
        rb2d.velocity = startPos * speed;

        if(Timer.secondsPassed %10 == 0 && !ready)
        {
            //speed up projectile every few seconds
            speed += 0.1f;
            ready = true;
        }

        //reset so that it won't repeat itself too much
        if(Timer.secondsPassed % 10 != 0 && ready)
        {
            ready = false;
        }
	}



}
