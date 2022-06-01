using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] string[] questMarkerNames;
    [SerializeField] bool[] questMarkerComplete;

    [SerializeField] QuestState currentState;

    private void Awake()
    {
        int numQuestManager = FindObjectsOfType<QuestManager>().Length;
        if(numQuestManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    MarkQuestComplete("Testing123!!!");
        //    MarkQuestIncomplete("Make pancakes!!!");
        //}

        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveQuestData();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadQuestData();
        }
    }

    int GetQuestNumber(string questToFind)
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            if (questMarkerNames[i].ToUpper().Equals(questToFind.ToUpper()))
            {
                return i;
            }
        }

        Debug.LogError("Quest '" + questToFind + "' does not exist");
        return 0;
    }

    public void SetQuestNames(QuestState qs)
    {
        if (qs == null)
        {
            return;
        }

        currentState = qs;
        questMarkerNames = currentState.GetQuestNames();
        questMarkerComplete = new bool[questMarkerNames.Length];
    }

    public void MarkQuestComplete(string questToMark)
    {
        if(questMarkerComplete.Length <= 0)
        {
            return;
        }

        questMarkerComplete[GetQuestNumber(questToMark)] = true;
        UpdateQuestObjects();
    }

    public void MarkQuestIncomplete(string questToMark)
    {
        if (questMarkerComplete.Length <= 0)
        {
            return;
        }

        questMarkerComplete[GetQuestNumber(questToMark)] = false;
        UpdateQuestObjects();
    }

    public void UpdateQuestObjects()
    {
        QuestObjectManager[] qom = FindObjectsOfType<QuestObjectManager>();
        if(qom.Length > 0)
        {
            for(int i = 0; i < qom.Length; i++)
            {
                qom[i].CheckQuestCompletion();
            }
        }
    }

    public bool CheckQuestCompletion(string questToCheck)
    {
        return GetQuestNumber(questToCheck) > 0 ? 
            questMarkerComplete[GetQuestNumber(questToCheck)] : 
            false;
    }

    public void SaveQuestData()
    {
        for(int i = 0; i < questMarkerNames.Length; i++)
        {
            if (questMarkerComplete[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 0);
            }
        }
    }

    public void LoadQuestData()
    {
        for(int i = 0; i < questMarkerNames.Length; i++)
        {
            int valueToSet = 0;
            if(PlayerPrefs.HasKey("QuestMarker_" + questMarkerNames[i]))
            {
                valueToSet = PlayerPrefs.GetInt("QuestMarker_" + questMarkerNames[i]);
            }

            questMarkerComplete[i] = valueToSet == 0 ? false : true;
        }
    }
}
