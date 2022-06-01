using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Animator anim;
    public Rigidbody rbody;

    private bool run;
    private float inputH;
    private float inputV;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        run = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("1"))
        {
            //animation WAIT01, at base layer, playing at the start of the animation
            anim.Play("WAIT01", -1, 0f);
        }

        if (Input.GetKeyDown("2"))
        {
            //animation WAIT02, at base layer, playing at the start of the animation
            anim.Play("WAIT02", -1, 0f);
        }

        if (Input.GetKeyDown("3"))
        {
            //animation WAIT03, at base layer, playing at the start of the animation
            anim.Play("WAIT03", -1, 0f);
        }

        if (Input.GetKeyDown("4"))
        {
            //animation WAIT04, at base layer, playing at the start of the animation
            anim.Play("WAIT04", -1, 0f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            int n = Random.Range(0, 2);

            if(n == 0)
            {
                anim.Play("DAMAGED00", -1, 0f);
            }

            else
            {
                anim.Play("DAMAGED01", -1, 0f);
            }
            
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        anim.SetBool("Run", run);
        anim.SetFloat("InputH", inputH);
        anim.SetFloat("InputV", inputV);

        float moveX = inputH * 20f * Time.deltaTime;
        float moveZ = inputV * 50f * Time.deltaTime;

        if(moveZ <= 0f)
        {
            moveX = 0f;
        }
        else if(run)
        {
            moveX *= 3f;
            moveZ *= 3f;
        }

        rbody.velocity = new Vector3(moveX, 0f, moveZ);
    }
}
