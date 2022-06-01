using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnits : MonoBehaviour
{
    [SerializeField] Sprite
        deadSprite  = null,
        aliveSprite = null;

    [SerializeField] bool
        isPlayer = false,
        isDead = false;

    [SerializeField] float fadeSpeed = 2f;

    [SerializeField] int 
        currHP = 0, 
        maxHP = 0, 
        currMP = 0, 
        maxMP = 0, 
        str = 0, 
        def = 0, 
        wpnPwr = 0, 
        armrPwr = 0;

    [SerializeField] string[] movesAvailable;
    [SerializeField] string unitName = "";

    SpriteRenderer sr = null;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetIsPlayer()
    {
        return isPlayer;
    }

    public void SetIsDead(bool b)
    {
        isDead = b;
        if(isDead && isPlayer)
        {
            GetComponent<SpriteRenderer>().sprite = deadSprite;
        }

        else if (!isDead && isPlayer)
        {
            GetComponent<SpriteRenderer>().sprite = aliveSprite;
        }
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public string GetUnitName()
    {
        return unitName;
    }

    public void SetCurrHP(int hp)
    {
        currHP = hp;
    }

    public int GetCurrHP()
    {
        return currHP;
    }

    public void SetMaxHP(int hp)
    {
        maxHP = hp;
    }

    public int GetMaxHP()
    {
        return maxHP;
    }

    public void SetCurrMP(int mp)
    {
        currMP = mp;
    }

    public int GetCurrMP()
    {
        return currMP;
    }

    public void SetMaxMP(int mp)
    {
        maxMP = mp;
    }

    public int GetMaxMP()
    {
        return maxMP;
    }

    public void SetStr(int strength)
    {
        str = strength;
    }

    public int GetStr()
    {
        return str;
    }

    public void SetDef(int defense)
    {
        def = defense;
    }

    public int GetDef()
    {
        return def;
    }

    public void SetWpnPwr(int wp)
    {
        wpnPwr = wp;
    }

    public int GetWpnPwr()
    {
        return wpnPwr;
    }

    public void SetArmrPwr(int ap)
    {
        armrPwr = ap;
    }

    public int GetArmrPwr()
    {
        return armrPwr;
    }

    public string[] GetMovesAvailable()
    {
        return movesAvailable;
    }

    public void StartFadingWhenDead()
    {
        StartCoroutine("FadingWhenDead");
    }

    IEnumerator FadingWhenDead()
    {
        while(sr.color.a > 0f)
        {
            sr.color = new Color
                (Mathf.MoveTowards(sr.color.r, 1f, fadeSpeed * Time.deltaTime),
                Mathf.MoveTowards(sr.color.g, 0f, fadeSpeed * Time.deltaTime),
                Mathf.MoveTowards(sr.color.b, 0f, fadeSpeed * Time.deltaTime),
                Mathf.MoveTowards(sr.color.a, 0f, fadeSpeed * Time.deltaTime));
            yield return null;
        }

        if(sr.color.a == 0f)
        {
            Debug.Log(unitName + " has been defeated!");
            gameObject.SetActive(false);
        }
    }
}
