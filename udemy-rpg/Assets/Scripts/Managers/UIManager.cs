using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class UIManager : MonoBehaviour
{
    // Variables set in the editor
    // UI elements
    [SerializeField] GameObject DialogueManager;
    [SerializeField] GameObject GameMenu;
    [SerializeField] GameObject ShopMenu;
    [SerializeField] GameObject TransitionScreen;
    [SerializeField] GameObject ResultsPanel;

    // Start is called before the first frame update
    private void Awake()
    {
        int numOfUIManager = FindObjectsOfType<UIManager>().Length;
        if(numOfUIManager > 1)
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
        DialogueManager.SetActive(true);
        GameMenu.SetActive(false);
        ShopMenu.SetActive(false);
        TransitionScreen.SetActive(true);
        ResultsPanel.SetActive(false);
    }

    private void Update()
    {

    }

    public void ActivateDialogueManager(bool b)
    {
        DialogueManager.SetActive(b);
    }

    public void ActivateGameMenu()
    {
        if (!GameMenu.activeInHierarchy)
        {
            GameMenu.SetActive(true);
        }
        else
        {
            GameMenu.GetComponent<GameMenu>().CloseMenu();
        }
    }

    public void ActivateShopMenu(bool b)
    {
        ShopMenu.SetActive(b);
    }

    public void ActivateTransitionScreen(bool b)
    {
        TransitionScreen.SetActive(b);
    }

    public void ActivateRewardsScreen(bool b)
    {
        ResultsPanel.SetActive(b);
    }
    
    public GameObject GetGameMenu()
    {
        return GameMenu;
    }

    public GameObject GetShopMenu()
    {
        return ShopMenu;
    }
}
