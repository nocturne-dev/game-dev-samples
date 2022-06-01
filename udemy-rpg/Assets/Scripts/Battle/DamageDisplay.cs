using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplay : MonoBehaviour
{
    [SerializeField] Text damageText = null;

    [SerializeField] float lifeTime = 1f;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float displayOffset = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
        gameObject.transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
    }

    public void SetDamage(int dmgAmount)
    {
        damageText.text = dmgAmount.ToString();
        gameObject.transform.position += new Vector3(Random.Range(-displayOffset, displayOffset), Random.Range(-displayOffset, displayOffset), 0f);
    }
}
