using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BattleStart : MonoBehaviour
{
    [SerializeField] BattleType[] potentialBattles = null;
    [SerializeField] QuestState questState = null;

    [SerializeField] bool 
        activateOnEnter = false, 
        activateOnStay  = false, 
        activateOnExit  = false,
        deactivate      = false,
        isBossBattle    = false,
        completeQuest   = false;

    [SerializeField] float timeBetweenBattles = 0f;

    [SerializeField] string questToComplete = "";

    private bool inArea = false;
    private float downtimeCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        downtimeCounter = Random.Range(timeBetweenBattles * 0.5f, timeBetweenBattles * 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (inArea &&
            !FindObjectOfType<Player>().GetIsInteracting())
        {
            if(CrossPlatformInputManager.GetAxisRaw("Horizontal") != 0 ||
                CrossPlatformInputManager.GetAxisRaw("Vertical") != 0)
            {
                downtimeCounter -= Time.deltaTime;
            }

            if(downtimeCounter <= 0)
            {
                downtimeCounter = Random.Range(timeBetweenBattles * 0.5f, timeBetweenBattles * 1.5f);
                StartCoroutine("StartBattle");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (activateOnEnter)
            {
                StartCoroutine("StartBattle");
            }
            
            else
            {
                inArea = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (activateOnExit)
            {
                StartCoroutine("StartBattle");
            }
            
            else
            {
                inArea = false;
            }
        }
    }

    IEnumerator StartBattle()
    {
        FindObjectOfType<UIManager>().ActivateTransitionScreen(true);
        FindObjectOfType<UIFadeTransition>().StartFadingToBlack();
        FindObjectOfType<GameManager>().SetBattleActive(true);

        int selectedBattle = Random.Range(0, potentialBattles.Length);
        FindObjectOfType<BattleManager>().SetRewardItems(potentialBattles[selectedBattle].GetRewardItems());
        FindObjectOfType<BattleManager>().SetRewardEXP(potentialBattles[selectedBattle].GetRewardEXP());

        yield return new WaitForSecondsRealtime(2.0f);

        FindObjectOfType<BattleManager>().BattleStart(potentialBattles[selectedBattle].GetEnemies(), isBossBattle);
        FindObjectOfType<UIFadeTransition>().StartFadingFromBlack();

        FindObjectOfType<BattleManager>().SetShouldCompleteQuest(completeQuest);
        FindObjectOfType<BattleManager>().SetQuestToComplete(questToComplete);
        
        if (completeQuest)
        {
            FindObjectOfType<QuestManager>().SetQuestNames(questState);
        }

        if (deactivate)
        {
            gameObject.SetActive(false);
        }
    }
}
