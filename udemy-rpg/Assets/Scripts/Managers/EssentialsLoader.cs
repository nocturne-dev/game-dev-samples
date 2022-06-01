using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EssentialsLoader : MonoBehaviour
{
    // Note: All variables in EssentialsLoader should be
    // considered as a singleton
    [SerializeField] GameObject
        gameManager     = null,
        player          = null,
        uiManager       = null,
        uiEventSystem   = null,
        questManager    = null,
        audioManager    = null,
        battleManager   = null;


    // Start is called before the first frame update
    void Awake()
    {
        LoadUIManager();
        LoadUIEventSystem();
        LoadPlayer();
        LoadGameManager();
        LoadQuestManager();
        LoadAudioManager();
        LoadBattleManager();
    }

    private void LoadUIManager()
    {
        if(FindObjectOfType<UIManager>() == null)
        {
            Instantiate(uiManager);
        }
    }

    private void LoadUIEventSystem()
    {
        if(FindObjectOfType<EventSystem>() == null)
        {
            Instantiate(uiEventSystem);
        }
    }

    private void LoadPlayer()
    {
        if(player == null)
        {
            return;
        }

        if (FindObjectOfType<Player>() == null)
        {
            Debug.Log("Player not found. Instantiating new Player...");
            FindObjectOfType<CameraController>().SetCinemachineVirtualCameraFollow(Instantiate(player));
        }

        else
        {
            Debug.Log("Player has been found!");
            FindObjectOfType<CameraController>().SetCinemachineVirtualCameraFollow(FindObjectOfType<Player>().gameObject);
        }
    }

    private void LoadGameManager()
    {
        if(FindObjectOfType<GameManager>() == null)
        {
            Instantiate(gameManager);
        }
    }

    private void LoadQuestManager()
    {
        if (FindObjectOfType<QuestManager>() == null)
        {
            Instantiate(questManager);
        }
    }

    private void LoadAudioManager()
    {
        if(FindObjectOfType<AudioManager>() == null)
        {
            Instantiate(audioManager);
        }
    }

    private void LoadBattleManager()
    {
        if(FindObjectOfType<BattleManager>() == null)
        {
            Instantiate(battleManager);
        }
    }
}
