using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour {

    public float spawnRate;
    public float spawnTime;
    public GameObject EnemyOne;

	// Use this for initialization
	void Awake () {

        //spawn a new enemy at spawnTime, then repeat
        //Invoke("SpawnNewEnemy", 0.0f);
        InvokeRepeating("SpawnNewEnemy", spawnTime, spawnRate);
    }

    void SpawnNewEnemy()
    {
        //have a new enemy arrive every spawnRate
        float xPos = Random.Range(-12, 12);
        float yPos = Random.Range(-12, 12);

        Vector2 setPos = new Vector2(xPos, yPos);

        Instantiate(EnemyOne, setPos, Quaternion.identity);
    }
}
