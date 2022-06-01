using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleItem : MonoBehaviour
{
    [SerializeField] Image spriteImage = null;
    [SerializeField] Text nameText = null, amountText = null;

    [SerializeField] bool 
        isItem = false,
        affectsHP = false,
        affectsMP = false;
    [SerializeField] int 
        itemAmount = 0,
        itemValue = 0;
    [SerializeField] string itemName = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Item()
    {
        if(itemAmount > 0)
        {
            itemAmount--;
            FindObjectOfType<GameManager>().RemoveItem(itemName);
            FindObjectOfType<BattleManager>().CloseItemMenu();
            FindObjectOfType<BattleManager>().OpenPlayerTargetMenu(isItem, itemName);
        }

        else
        {
            FindObjectOfType<BattleManager>().GetBattleNotification().SetNotificationText("Out of " + itemName);
            FindObjectOfType<BattleManager>().GetBattleNotification().Activate();
            FindObjectOfType<BattleManager>().CloseItemMenu();
        }
    }

    public void SetIsItem(bool b)
    {
        isItem = b;
    }

    public bool GetIsItem()
    {
        return isItem;
    }

    public void SetAffectsHP(bool b)
    {
        affectsHP = b;
    }

    public bool GetAffectsHP()
    {
        return affectsHP;
    }

    public void SetAffectsMP(bool b)
    {
        affectsMP = b;
    }

    public bool GetAffectsMP()
    {
        return affectsMP;
    }

    public void SetItemImage(Sprite sp)
    {
        spriteImage.sprite = spriteImage != null ? sp : null;
    }

    public void SetItemName(string s)
    {
        itemName = s;
        nameText.text = nameText != null ? itemName : null;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public void SetItemAmount(int i)
    {
        itemAmount = i;
        amountText.text = amountText != null ? "x" + itemAmount.ToString() : null;
    }

    public void SetItemValue(int i)
    {
        itemValue = i;
    }

    public int GetItemValue()
    {
        return itemValue;
    }
}
