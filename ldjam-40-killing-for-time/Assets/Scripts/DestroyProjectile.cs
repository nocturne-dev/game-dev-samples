using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour {

    SpriteRenderer sr;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        sr = collider.gameObject.GetComponent<SpriteRenderer>();

        if (sr.sortingLayerName.Contains("Weapon"))
        {
            Destroy(collider.gameObject);
        }
    }
}
