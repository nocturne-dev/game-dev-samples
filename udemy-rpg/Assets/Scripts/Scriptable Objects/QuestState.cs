using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuestState")]
public class QuestState : ScriptableObject
{
    [SerializeField] string[] questNames;

    public string[] GetQuestNames()
    {
        return questNames;
    }
}
