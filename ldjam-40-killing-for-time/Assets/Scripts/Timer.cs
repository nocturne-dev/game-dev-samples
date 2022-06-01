using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    static public int secondsPassed;

    float timeUpFloat; //the timer for all characters

	// Use this for initialization
	void Start () {
        timeUpFloat = 0.0f;
        secondsPassed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timeUpFloat += Time.deltaTime;
        secondsPassed = (int)timeUpFloat;
	}
}
