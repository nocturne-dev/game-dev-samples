using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectManager : MonoBehaviour
{
    [SerializeField] GameObject questObject;
    [SerializeField] string questToCheck;
    [SerializeField] bool activeIfComplete;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckQuestCompletion()
    {
        if (FindObjectOfType<QuestManager>().CheckQuestCompletion(questToCheck))
        {
            questObject.SetActive(activeIfComplete);
        }
    }
}
