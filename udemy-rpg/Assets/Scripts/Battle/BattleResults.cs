using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BattleResults : MonoBehaviour
{
    [SerializeField] Text 
        expText     = null, 
        itemsText   = null;

    [SerializeField] List<string> rewardItems = new List<string>();

    [SerializeField] int expEarned = 0;

    private bool questComplete      = false;
    private string questToComplete  = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenRewardsScreen(int exp, List<string> rewards, bool isQuest, string questName)
    {
        expEarned   = exp;
        rewardItems = rewards;

        expText.text = expEarned + " EXP Earned!";
        itemsText.text = "";

        foreach(string item in rewardItems)
        {
            itemsText.text += item + " \n";
        }

        questComplete = isQuest;
        questToComplete = questName;
    }

    public void CloseRewardsScreen()
    {
        GameManager gm = FindObjectOfType<GameManager>();

        for(int i = 0; i < gm.GetStatsList().Length; i++)
        {
            if (gm.GetStatsList()[i].gameObject.activeInHierarchy)
            {
                gm.GetStatsList()[i].AddEXP(expEarned);
            }
        }

        for(int j = 0; j < rewardItems.Count; j++)
        {
            gm.AddItem(rewardItems[j]);
        }

        if (questComplete)
        {
            FindObjectOfType<QuestManager>().MarkQuestComplete(questToComplete);
        }

        FindObjectOfType<GameManager>().SetBattleActive(false);
        gameObject.SetActive(false);
    }

    public void SetQuestComplete(bool b)
    {
        questComplete = b;
    }

    public void SetQuestToComplete(string s)
    {
        questToComplete = s;
    }
}
