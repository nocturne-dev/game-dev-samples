using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class GameMenu : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] GameObject[] menuWindows, statsButtons , statsList;
    [SerializeField] Image[] charImageList;
    [SerializeField] Slider[] expSliderList;
    [SerializeField] Text currentGoldText;
    [SerializeField] Text[] nameTextList, hpTextList, mpTextList, lvlTextList, expTextList;

    [Header("Items Menu")]
    [SerializeField] GameObject selectMenu;
    [SerializeField] Item activeItem;
    [SerializeField] ItemButton[] itemButtons;
    [SerializeField] string selectedItem;
    [SerializeField] Text itemName, itemDescription, useButtonText;
    [SerializeField] Text[] selectPlayerOption;

    [Header("Stats Menu")]
    [SerializeField] Image statsImage;
    [SerializeField] Text statsName, statsHP, statsMP, statsStr, statsDef, statsWpnE, statsWpnP, statsArmrE, statsArmrP, statsEXP;

    // Private variables
    CharacterStats[] charStats;

    private void Start()
    {
        UpdateCharStatsInMenu();
    }

    private void OnEnable()
    {
        FindObjectOfType<GameManager>().SetGameMenuActive(true);
        UpdateCharStatsInMenu();
    }

    public void UpdateCharStatsInMenu()
    {
        charStats = FindObjectOfType<GameManager>().GetStatsList();

        for(int i = 0; i < charStats.Length; i++)
        {
            if (charStats[i].gameObject.activeInHierarchy)
            {
                statsList[i].SetActive(true);

                nameTextList[i].text =  charStats[i].GetCharName();
                hpTextList[i].text =    "HP: " + charStats[i].GetCurrentHP() + " / " + charStats[i].GetMaxHP();
                mpTextList[i].text =    "MP: " + charStats[i].GetCurrentMP() + " / " + charStats[i].GetMaxMP();
                lvlTextList[i].text =   "Lvl: " + charStats[i].GetPlayerLvl();

                expTextList[i].text =   charStats[i].GetPlayerLvl() >= charStats[i].GetMaxLvl()
                    ? "MAX LEVEL" 
                    : charStats[i].GetCurrentEXP().ToString() + " / " + charStats[i].GetEXPToNextLvl()[charStats[i].GetPlayerLvl()].ToString();

                expSliderList[i].maxValue = charStats[i].GetPlayerLvl() >= charStats[i].GetMaxLvl()
                    ? charStats[i].GetEXPToNextLvl()[charStats[i].GetPlayerLvl()-1] 
                    : charStats[i].GetEXPToNextLvl()[charStats[i].GetPlayerLvl()];
                
                expSliderList[i].value = charStats[i].GetCurrentEXP();

                charImageList[i].sprite = charStats[i].GetCharImage();
            }
            else
            {
                statsList[i].SetActive(false);
            }
        }
        currentGoldText.text = FindObjectOfType<GameManager>().GetCurrentGold().ToString() + "g";
    }

    public void ToggleWindow(int windowNumber)
    {
        UpdateCharStatsInMenu();

        for (int i = 0; i < menuWindows.Length; i++)
        {
            if (windowNumber == i)  
            {
                menuWindows[i].SetActive(!menuWindows[i].activeInHierarchy); 
            }

            else 
            { 
                menuWindows[i].SetActive(false); 
            }
        }
        selectMenu.SetActive(false);
    }

    public void OpenStatsWindow()
    {
        UpdateCharStatsInMenu();

        // Update info shown
        SetStatsWindow(0);

        for(int i = 0; i < statsButtons.Length; i++)
        {
            statsButtons[i].SetActive(charStats[i].gameObject.activeInHierarchy);
            statsButtons[i].GetComponentInChildren<Text>().text = charStats[i].GetCharName();
        }
    }

    public void SetStatsWindow(int selected)
    {
        CharacterStats cs = charStats[selected];

        statsName.text =    cs.GetCharName();
        statsHP.text =      cs.GetCurrentHP().ToString() + " / " + cs.GetMaxHP().ToString();
        statsMP.text =      cs.GetCurrentMP().ToString() + " / " + cs.GetMaxMP().ToString();
        statsStr.text =     cs.GetStrength().ToString();
        statsDef.text =     cs.GetDefense().ToString();
        statsWpnE.text =    cs.GetEquippedWpn();
        statsWpnP.text =    cs.GetWpnPwr().ToString();
        statsArmrE.text =   cs.GetEquippedArmr();
        statsArmrP.text =   cs.GetArmrPwr().ToString();

        statsEXP.text =     cs.GetPlayerLvl() >= cs.GetMaxLvl() 
            ? "0" 
            : (cs.GetEXPToNextLvl()[cs.GetPlayerLvl()] - cs.GetCurrentEXP()).ToString();
        
        statsImage.sprite = charStats[selected].GetCharImage();
    }

    public void SetItemsWindow()
    {
        FindObjectOfType<GameManager>().SortItems();

        for(int i = 0; i < FindObjectOfType<GameManager>().GetItemsHeld().Length; i++)
        {
            itemButtons[i].SetBtnValue(i);

            if(FindObjectOfType<GameManager>().GetItemsHeld()[i] != "")
            {
                itemButtons[i].GetBtnImage().gameObject.SetActive(true);
                itemButtons[i].GetBtnImage().sprite = FindObjectOfType<GameManager>().GetItem(FindObjectOfType<GameManager>().GetItemsHeld()[i]).GetItemSprite();
                itemButtons[i].SetAmountText(FindObjectOfType<GameManager>().GetNumOfItems()[i].ToString());
            }
            else
            {
                itemButtons[i].GetBtnImage().gameObject.SetActive(false);
                itemButtons[i].SetAmountText("");
            }
        }
    }

    public void SetItemSelectedInfo(Item selected)
    {
        activeItem = selected;
        if (activeItem.GetIsItem()) { useButtonText.text = "Use"; }
        else if(activeItem.GetIsArmor() || 
            activeItem.GetIsWeapon()) { useButtonText.text = "Equip"; }
        
        itemName.text = activeItem.GetItemName();
        itemDescription.text = activeItem.GetItemDescription();
    }

    public void UseItem(int selectedPlayer)
    {
        activeItem.Use(selectedPlayer);
        CloseSelectWindow();
    }

    public void DiscardItem()
    {
        if(activeItem != null)
        { 
            FindObjectOfType<GameManager>().RemoveItem(activeItem.GetItemName());
        }
    }

    public void OpenSelectWindow()
    {
        selectMenu.SetActive(true);
        for(int i = 0; i < selectPlayerOption.Length; i++)
        {
            if(FindObjectOfType<GameManager>().GetStatsList()[i] != null)
            {
                CharacterStats stats = FindObjectOfType<GameManager>().GetStatsList()[i];
                selectPlayerOption[i].text = stats.GetCharName();
                selectPlayerOption[i].transform.parent.gameObject.SetActive(stats.gameObject.activeInHierarchy);
            }
        }
    }

    public void CloseSelectWindow()
    {
        selectMenu.SetActive(false);
    }

    public void CloseMenu()
    {
        foreach(GameObject mw in menuWindows)   
        {
            mw.SetActive(false);
        }

        FindObjectOfType<GameManager>().SetGameMenuActive(false);

        selectMenu.SetActive(false);
        gameObject.SetActive(false);
    }

    public void SaveGame()
    {
        FindObjectOfType<GameManager>().SaveData();
        FindObjectOfType<QuestManager>().SaveQuestData();
    }

    public ItemButton[] GetItemButtons()
    {
        return itemButtons;
    }

    // TESTING - ???
    public void PlayButtonSound()
    {
        FindObjectOfType<AudioManager>().PlaySFX(4);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(FindObjectOfType<AudioManager>().gameObject);
        Destroy(FindObjectOfType<QuestManager>().gameObject);
        Destroy(FindObjectOfType<UIManager>().gameObject);
        Destroy(FindObjectOfType<Player>().gameObject);
        Destroy(FindObjectOfType<GameManager>().gameObject);
        Destroy(gameObject);
    }
}
