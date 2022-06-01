using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour {

    public static int levelN = 0;

    private Vector3 startPos;
    private Quaternion startRot;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
        startRot = transform.rotation;
	}

    void NextLevel()
    {
        levelN++;

        if(levelN > 1)
        {
            levelN = 0;
        }

        SceneManager.LoadScene(levelN);
    }

    //detect collision with trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Death")
        {
            transform.position = startPos;
            transform.rotation = startRot;
            GetComponent<Animator>().Play("LOSE00", -1, 0f);
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
        }

        else if(other.tag == "Checkpoint")
        {
            startPos = other.transform.position;
            startRot = other.transform.rotation;

            Destroy(other.gameObject);
        }

        else if(other.tag == "Goal")
        {
            Destroy(other.gameObject);
            GetComponent<Animator>().Play("WIN00", -1, 0f);

            Invoke("NextLevel", 2.0f);
        }
    }
}
