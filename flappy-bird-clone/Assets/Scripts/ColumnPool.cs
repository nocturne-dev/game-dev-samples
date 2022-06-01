using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour {

    private GameObject[] columns;
    private Vector2 objectPoolPosition = new Vector2(-15f, -25f);   //position of where to spawn next
    private float timeSinceLastSpawn;
    private float spawnXPosition = 10f;
    private int currentColumn = 0;

    public int columnPoolSize = 5;
    public GameObject columnPrefab; //instantiate column prefabs
    public float spawnRate = 4f;    //rate of spawning columns
    public float columnMin = -1f;
    public float columnMax = 3.5f;

	// Use this for initialization
	void Start () {
        columns = new GameObject[columnPoolSize];
        for(int i = 0; i < columnPoolSize; i++)
        {
            columns[i] = (GameObject)Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);    //instantiating an object, casting it to game object, and storing it in array
        }
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceLastSpawn += Time.deltaTime; //time it takes to render last frame, and add that to variable

        if (GameControl.instance.gameOver == false && timeSinceLastSpawn >= spawnRate)  //if the game is still going, and checks to see if time passed has gone over spawn rate
        {
            timeSinceLastSpawn = 0;
            float spawnYPosition = Random.Range(columnMin, columnMax);  //set Y position of column between these values
            columns[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);    //spawn new column at updated location
            currentColumn++;    //update current column
            if(currentColumn >= columnPoolSize) //reset currentColumn if it grows larger than columnPoolSize
            {
                currentColumn = 0;
            }
        }
	}
}
