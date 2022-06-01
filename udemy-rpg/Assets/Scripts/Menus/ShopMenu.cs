using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] GameObject shopButtons, goldCount, buyWindow, sellWindow;
    [SerializeField] ShopKeeper shopKeeper;
    [SerializeField] Text goldText;

    [SerializeField] ItemButton[] buyItemButtons;
    [SerializeField] ItemButton[] sellItemButtons;

    [SerializeField] Item activeItem;
    [SerializeField] Text buyItemName, buyItemDescription, buyItemValue;
    [SerializeField] Text sellItemName, sellItemDescription, sellItemValue;

    private void Start()
    {
        shopButtons.SetActive(false);
        goldCount.SetActive(false);
        buyWindow.SetActive(false);
        sellWindow.SetActive(false);
    }

    public void OpenShopMenu()
    {
        FindObjectOfType<GameManager>().SetShopMenuActive(true);
        shopButtons.SetActive(true);
        goldCount.SetActive(true);
        goldText.text = FindObjectOfType<GameManager>().GetCurrentGold().ToString() + "g";
    
        foreach(ItemButton buy in buyItemButtons)
        {
            buy.gameObject.SetActive(false);
        }

        foreach(ItemButton sell in sellItemButtons)
        {
            sell.gameObject.SetActive(false);
        }
    }

    public void CloseShopMenu()
    {
        FindObjectOfType<GameManager>().SetShopMenuActive(false);
        goldCount.SetActive(false);
        shopButtons.SetActive(false);
        buyWindow.SetActive(false);
        sellWindow.SetActive(false);
    }

    public void OpenBuyWindow()
    {
        buyWindow.SetActive(true);
        sellWindow.SetActive(false);

        buyItemName.text = "";
        buyItemDescription.text = "";
        buyItemValue.text = "";

        for(int i = 0; i < shopKeeper.GetShopInventory().Length; i++)
        {
            buyItemButtons[i].gameObject.SetActive(true);
            buyItemButtons[i].SetBtnValue(i);

            if(shopKeeper.GetShopInventory()[i] != "")
            {
                buyItemButtons[i].GetBtnImage().gameObject.SetActive(true);
                buyItemButtons[i].GetBtnImage().sprite = FindObjectOfType<GameManager>().GetItem(shopKeeper.GetShopInventory()[i]).GetItemSprite();
                buyItemButtons[i].SetAmountText("");
            }
            else
            {
                buyItemButtons[i].SetAmountText("");
                buyItemButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OpenSellWindow()
    {
        buyWindow.SetActive(false);
        sellWindow.SetActive(true);

        sellItemName.text = "";
        sellItemDescription.text = "";
        sellItemValue.text = "";

        ShowItemsToSell();
    }

    private void ShowItemsToSell()
    {
        FindObjectOfType<GameManager>().SortItems();

        for (int i = 0; i < FindObjectOfType<GameManager>().GetItemsHeld().Length; i++)
        {
            sellItemButtons[i].gameObject.SetActive(true);
            sellItemButtons[i].SetBtnValue(i);

            if (FindObjectOfType<GameManager>().GetItemsHeld()[i] != "")
            {
                sellItemButtons[i].GetBtnImage().gameObject.SetActive(true);
                sellItemButtons[i].GetBtnImage().sprite = FindObjectOfType<GameManager>().GetItem(FindObjectOfType<GameManager>().GetItemsHeld()[i]).GetItemSprite();
                sellItemButtons[i].SetAmountText(FindObjectOfType<GameManager>().GetNumOfItems()[i].ToString());
            }
            else
            {
                sellItemButtons[i].SetAmountText("");
                sellItemButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetBuyItemInfo(Item buyItem)
    {
        activeItem = buyItem;
        buyItemName.text = activeItem.GetItemName();
        buyItemDescription.text = activeItem.GetItemDescription();
        buyItemValue.text = "Value: " + activeItem.GetItemValue() + "g";
    }

    public void SetSellItemInfo(Item sellItem)
    {
        activeItem = sellItem;
        sellItemName.text = activeItem.GetItemName();
        sellItemDescription.text = activeItem.GetItemDescription();
        sellItemValue.text = "Value: " + Mathf.FloorToInt(activeItem.GetItemValue() * 0.5f).ToString() + "g";
    }

    public void PurchaseItem()
    {
        if(activeItem != null && 
            FindObjectOfType<GameManager>().GetCurrentGold() >= activeItem.GetItemValue())
        {
            FindObjectOfType<GameManager>().DecrementCurrentGold(activeItem.GetItemValue());
            FindObjectOfType<GameManager>().AddItem(activeItem.GetItemName());
        }

        goldText.text = FindObjectOfType<GameManager>().GetCurrentGold().ToString() + "g";
    }

    public void SellItem()
    {
        if(activeItem != null)
        {
            FindObjectOfType<GameManager>().IncrementCurrentGold(Mathf.FloorToInt(activeItem.GetItemValue() * 0.5f));
            FindObjectOfType<GameManager>().RemoveItem(activeItem.GetItemName());
        }

        goldText.text = FindObjectOfType<GameManager>().GetCurrentGold().ToString() + "g";
        ShowItemsToSell();
    }

    public GameObject GetBuyWindow()
    {
        return buyWindow;
    }

    public GameObject GetSellWindow()
    {
        return sellWindow;
    }

    public void SetShopKeeper(ShopKeeper sk)
    {
        shopKeeper = sk;
    }
}
