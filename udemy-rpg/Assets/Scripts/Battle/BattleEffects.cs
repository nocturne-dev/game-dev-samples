using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEffects : MonoBehaviour
{
    [SerializeField] float effectDuration;
    [SerializeField] int soundEffect;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlaySFX(soundEffect);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, effectDuration);
    }
}
