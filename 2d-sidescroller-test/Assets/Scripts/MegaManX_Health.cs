using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MegaManX_Health : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.position.y < -3)
        {
            Die();
        }
	}

    void Die()
    {
        //restarts level, or loads another scene
        SceneManager.LoadScene("Main");
    }
}
