using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bird>() != null) //check to see if object entering through trigger is a Bird
        {
            GameControl.instance.BirdScored();
        }
    }
}