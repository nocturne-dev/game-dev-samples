using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    [SerializeField] bool isItem;
    [SerializeField] bool isWeapon;
    [SerializeField] bool isArmor;

    [Header("Item Details")]
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] int    itemValue;
    [SerializeField] Sprite itemSprite;

    [Header("Stat Changes")]
    [SerializeField] int amountToChange;
    [SerializeField] bool affectHP, affectMP, affectStr, affectDef;

    [Header("Equipment Values")]
    [SerializeField] int wpnValue;
    [SerializeField] int armrValue;

    public void Use(int selectedPlayer)
    {
        CharacterStats playerToUseOn = FindObjectOfType<GameManager>().GetStatsList()[selectedPlayer];
        
        if(playerToUseOn == null)
        {
            return;
        }

        if (isItem)
        {
            if (affectHP) { playerToUseOn.SetCurrentHP(playerToUseOn.GetCurrentHP() + amountToChange); }
            if (affectMP) { playerToUseOn.SetCurrentMP(playerToUseOn.GetCurrentMP() + amountToChange); }
        }

        else if (isWeapon) 
        { 
            if(playerToUseOn.GetEquippedWpn() != "")
            {
                FindObjectOfType<GameManager>().AddItem(playerToUseOn.GetEquippedWpn());
            }

            playerToUseOn.SetEquippedWpn(itemName);
            playerToUseOn.SetWpnPwr(wpnValue);
        }

        else if (isArmor) 
        {
            if (playerToUseOn.GetEquippedArmr() != "")
            {
                FindObjectOfType<GameManager>().AddItem(playerToUseOn.GetEquippedArmr());
            }

            playerToUseOn.SetEquippedArmr(itemName);
            playerToUseOn.SetArmrPwr(armrValue);
        }

        FindObjectOfType<GameManager>().RemoveItem(itemName);
    }

    public bool GetIsArmor() { return isArmor; }
    public bool GetIsItem() { return isItem; }
    public bool GetIsWeapon() { return isWeapon; }
    public int GetItemValue() { return itemValue; }
    public string GetItemName() { return itemName; }
    public string GetItemDescription() { return itemDescription; }
    public Sprite GetItemSprite() { return itemSprite; }
    public bool GetAffectsHP() { return affectHP; }
    public bool GetAffectsMP() { return affectMP; }
    public int GetAmountToChange() { return amountToChange; }
}
