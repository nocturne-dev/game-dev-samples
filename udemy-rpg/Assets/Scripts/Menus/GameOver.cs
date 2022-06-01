using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] string
        mainMenuScene = "";

    // Start is called before the first frame update
    void Start()
    {
        //FindObjectOfType<AudioManager>().PlayBGM(4);
        //FindObjectOfType<Player>().gameObject.SetActive(false);
        //FindObjectOfType<GameMenu>().gameObject.SetActive(false);
        //FindObjectOfType<BattleManager>().gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainMenu()
    {
        Destroy(FindObjectOfType<GameManager>().gameObject);
        Destroy(FindObjectOfType<Player>().gameObject);
        Destroy(FindObjectOfType<UIManager>().gameObject);
        Destroy(FindObjectOfType<BattleManager>().gameObject);

        SceneManager.LoadScene(mainMenuScene);
    }

    public void LoadLastSave()
    {
        Destroy(FindObjectOfType<GameManager>().gameObject);
        Destroy(FindObjectOfType<Player>().gameObject);
        Destroy(FindObjectOfType<UIManager>().gameObject);
        Destroy(FindObjectOfType<BattleManager>().gameObject);

        SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));
    }
}
