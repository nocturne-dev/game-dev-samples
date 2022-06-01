using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] Image btnImage;
    [SerializeField] Text amountText;
    
    [SerializeField] int btnValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemSelected()
    {
        if (FindObjectOfType<UIManager>().GetGameMenu().activeInHierarchy)
        {
            if(FindObjectOfType<GameManager>().GetItemsHeld()[btnValue] != "")
            {
                FindObjectOfType<GameMenu>()
                    .SetItemSelectedInfo(FindObjectOfType<GameManager>()
                        .GetItem(FindObjectOfType<GameManager>()
                            .GetItemsHeld()[btnValue]));
            }
            return;
        }

        else if (FindObjectOfType<UIManager>().GetShopMenu().activeInHierarchy)
        {
            if (FindObjectOfType<ShopMenu>().GetBuyWindow().activeInHierarchy)
            {
                FindObjectOfType<ShopMenu>()
                    .SetBuyItemInfo(FindObjectOfType<GameManager>()
                        .GetItem(FindObjectOfType<ShopKeeper>()
                            .GetShopInventory()[btnValue]));
            }
            else if (FindObjectOfType<ShopMenu>().GetSellWindow().activeInHierarchy)
            {
                FindObjectOfType<ShopMenu>()
                    .SetSellItemInfo(FindObjectOfType<GameManager>()
                        .GetItem(FindObjectOfType<GameManager>()
                            .GetItemsHeld()[btnValue]));
            }
            return;
        }
    }

    public Image GetBtnImage() { return btnImage; }
    public void SetAmountText(string s) { amountText.text = s; }
    public void SetBtnValue(int i) { btnValue = i; }
}
