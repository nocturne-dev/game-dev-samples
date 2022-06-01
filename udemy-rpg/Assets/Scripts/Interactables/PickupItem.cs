using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PickupItem : MonoBehaviour
{
    [SerializeField] bool canPickUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canPickUp 
            && CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            FindObjectOfType<GameManager>().AddItem(GetComponent<Item>().GetItemName());
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canPickUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canPickUp = false;
        }
    }
}
