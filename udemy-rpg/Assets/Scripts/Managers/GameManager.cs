using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour
{
    [SerializeField] CharacterStats[] statsList;
    [SerializeField] Item[] referenceItems;
    
    [SerializeField] int[] numOfItems;
    [SerializeField] string[] itemsHeld;
    
    [Tooltip("Read-Only"), SerializeField] bool gameMenuActive, shopMenuActive, dialogueActive, sceneTransitionActive, battleActive;
    [SerializeField] int currentGold;

    private void Awake()
    {
        int numGameManager = FindObjectsOfType<GameManager>().Length;

        if (numGameManager > 1) 
        { 
            Destroy(gameObject); 
        }
        else
        { 
            DontDestroyOnLoad(gameObject); 
        }
    }

    private void Start()
    {
        SortItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuActive)
        {
            FindObjectOfType<Player>().SetIsInteracting(true);
            FindObjectOfType<Player>().SetCanMenu(true);
        }

        else if (dialogueActive || 
            sceneTransitionActive ||
            shopMenuActive ||
            battleActive) 
        { 
            FindObjectOfType<Player>().SetIsInteracting(true);
            FindObjectOfType<Player>().SetCanMenu(false);
        }

        else
        {
            FindObjectOfType<Player>().SetIsInteracting(false);
            FindObjectOfType<Player>().SetCanMenu(true); 
        }

        // TEMPORARY (START)
        if (Input.GetKeyDown(KeyCode.J))
        {
            AddItem("Iron Armor");
            AddItem("Leather Armor");
            AddItem("Random");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadData();
        }
        // TEMPORARY (END)
    }

    public void IncrementCurrentGold(int value)
    {
        currentGold += value;
    }

    public void DecrementCurrentGold(int value)
    {
        currentGold -= value;
    }

    public CharacterStats[] GetStatsList() 
    { 
        return statsList; 
    }

    public Item GetItem(string itemToReturn) 
    {
        foreach(Item item in referenceItems)
        {
            if (item.GetItemName().ToUpper().Equals(itemToReturn.ToUpper())) 
            { 
                return item; 
            }
        }
        return null; 
    }

    public void SortItems()
    {
        for(int i = 0; i < itemsHeld.Length - 1; i++)
        {
            for(int j = i + 1; j < itemsHeld.Length - 1; j++)
            {
                if(itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[j];
                    itemsHeld[j] = "";
                    numOfItems[i] = numOfItems[j];
                    numOfItems[j] = 0;
                }

                else 
                { 
                    break; 
                }
            }
        }
    }

    public void AddItem(string itemToAdd)
    {
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == itemToAdd)
            {
                numOfItems[i]++;
                break;
            }

            else if(itemsHeld[i] == "")
            {
                for(int j = 0; j < referenceItems.Length; j++)
                {
                    if(referenceItems[j].GetItemName() == itemToAdd)
                    {
                        itemsHeld[i] = itemToAdd;
                        numOfItems[i]++;
                        break;
                    }
                }
                break;
            }
        }
    }

    public void RemoveItem(string itemToRemove)
    {
        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == itemToRemove)
            {
                numOfItems[i]--;
                if(numOfItems[i] <= 0)
                {
                    itemsHeld[i] = "";
                }
                break;
            }
        }
        if (FindObjectOfType<UIManager>().GetGameMenu().activeInHierarchy)
        {
            FindObjectOfType<GameMenu>().SetItemsWindow();
        }
    }

    public string[] GetItemsHeld() 
    { 
        return itemsHeld; 
    }

    public int[] GetNumOfItems() 
    {
        return numOfItems; 
    }

    public int GetCurrentGold()
    { 
        return currentGold; 
    }

    public void SetGameMenuActive(bool b) 
    { 
        gameMenuActive = b; 
    }

    public void SetShopMenuActive(bool b)
    {
        shopMenuActive = b;
    }

    public void SetDialogueActive(bool b) 
    { 
        dialogueActive = b; 
    }

    public bool GetDialogueActive()
    { 
        return dialogueActive; 
    }

    public void SetSceneTransitionActive(bool b) 
    { 
        sceneTransitionActive = b; 
    }

    public void SetBattleActive(bool b)
    {
        battleActive = b;
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_X", FindObjectOfType<Player>().transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_Y", FindObjectOfType<Player>().transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_Z", FindObjectOfType<Player>().transform.position.z);
    
        // Save character info
        foreach(CharacterStats cs in statsList)
        {
            if (cs.gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + cs.GetCharName() + "_Active", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Player_" + cs.GetCharName() + "_Active", 0);
            }

            PlayerPrefs.SetInt("Player_" + cs.GetCharName() + "_Level", cs.GetPlayerLvl());
            PlayerPrefs.SetInt("Player_" + cs.GetCharName() + "_CurrentEXP", cs.GetCurrentEXP());
            PlayerPrefs.SetInt("Player_" + cs.GetCharName() + "_CurrentHP", cs.GetCurrentHP());
            PlayerPrefs.SetInt("Player_" + cs.GetCharName() + "_CurrentMP", cs.GetCurrentMP());
            PlayerPrefs.SetInt("Player_" + cs.GetCharName() + "_MaxHP", cs.GetMaxHP());
            PlayerPrefs.SetInt("Player_" + cs.GetCharName() + "_MaxMP", cs.GetMaxMP());
            PlayerPrefs.SetInt("Player_" + cs.GetCharName() + "_Strength", cs.GetStrength());
            PlayerPrefs.SetInt("Player_" + cs.GetCharName() + "_Defense", cs.GetDefense());
            PlayerPrefs.SetInt("Player_" + cs.GetCharName() + "_WpnPwr", cs.GetWpnPwr());
            PlayerPrefs.SetInt("Player_" + cs.GetCharName() + "_ArmrPwr", cs.GetArmrPwr());
            PlayerPrefs.SetString("Player_" + cs.GetCharName() + "_EquippedWpn", cs.GetEquippedWpn());
            PlayerPrefs.SetString("Player_" + cs.GetCharName() + "_EquippedArmr", cs.GetEquippedArmr());
        }

        // Save inventory
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefs.SetString("Item_In_Inventory_" + i, itemsHeld[i]);
            PlayerPrefs.SetInt("Item_Amount_" + i, numOfItems[i]);
        }
    }

    public void LoadData()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));

        FindObjectOfType<Player>().transform.position = new Vector3(
            PlayerPrefs.GetFloat("Player_Position_X"),
            PlayerPrefs.GetFloat("Player_Position_Y"),
            PlayerPrefs.GetFloat("Player_Position_Z"));

        foreach(CharacterStats cs in statsList)
        {
            if(PlayerPrefs.GetInt("Player_" + cs.GetCharName() + "_Active") == 0)
            {
                cs.gameObject.SetActive(false);
            }
            else
            {
                cs.gameObject.SetActive(true);
            }

            cs.SetPlayerLvl(PlayerPrefs.GetInt("Player_" + cs.GetCharName() + "_Level"));
            cs.SetCurrentEXP(PlayerPrefs.GetInt("Player_" + cs.GetCharName() + "_CurrentEXP"));
            cs.SetCurrentHP(PlayerPrefs.GetInt("Player_" + cs.GetCharName() + "_CurrentHP"));
            cs.SetCurrentMP(PlayerPrefs.GetInt("Player_" + cs.GetCharName() + "_CurrentMP"));
            cs.SetMaxHP(PlayerPrefs.GetInt("Player_" + cs.GetCharName() + "_MaxHP"));
            cs.SetMaxMP(PlayerPrefs.GetInt("Player_" + cs.GetCharName() + "_MaxMP"));
            cs.SetStrength(PlayerPrefs.GetInt("Player_" + cs.GetCharName() + "_Strength"));
            cs.SetDefense(PlayerPrefs.GetInt("Player_" + cs.GetCharName() + "_Defense"));
            cs.SetWpnPwr(PlayerPrefs.GetInt("Player_" + cs.GetCharName() + "_WpnPwr"));
            cs.SetArmrPwr(PlayerPrefs.GetInt("Player_" + cs.GetCharName() + "_ArmrPwr"));
            cs.SetEquippedWpn(PlayerPrefs.GetString("Player_" + cs.GetCharName() + "_EquippedWpn"));
            cs.SetEquippedArmr(PlayerPrefs.GetString("Player_" + cs.GetCharName() + "_EquippedArmr"));
        }

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString("Item_In_Inventory_" + i);
            numOfItems[i] = PlayerPrefs.GetInt("Item_Amount_" + i);
        }
    }
}
