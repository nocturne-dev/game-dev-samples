using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] ShopKeeperState currentState;

    [Tooltip("Read-Only"), SerializeField] string[] shopInventory = new string[40];

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FindObjectOfType<UIManager>().ActivateShopMenu(true);
            FindObjectOfType<ShopMenu>().SetShopKeeper(this);
            SetShopInventory(currentState);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FindObjectOfType<UIManager>().ActivateShopMenu(false);
        }
    }

    void SetShopInventory(ShopKeeperState sks)
    {
        if(sks == null)
        {
            return;
        }

        currentState = sks;
        shopInventory = currentState.GetShopInventory();
        SortInventory();
    }

    void SortInventory()
    {
        for(int i = 0; i < shopInventory.Length; i++)
        {
            for(int j = i + 1; j < shopInventory.Length - 1; j++)
            {
                if(shopInventory[i] == "")
                {
                    shopInventory[i] = shopInventory[j];
                    shopInventory[j] = "";
                }
                else
                {
                    break;
                }
            }
        }
    }

    public string[] GetShopInventory()
    {
        return shopInventory;
    }
}
