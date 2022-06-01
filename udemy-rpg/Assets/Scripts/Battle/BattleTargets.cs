using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class BattleTargets : MonoBehaviour
{
    [SerializeField] Text targetName = null;

    [SerializeField] bool isItem = false;
    [SerializeField] int activeUnitTarget = 0;
    [SerializeField] string moveName = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeAction()
    {
        FindObjectOfType<BattleManager>().PlayerAttack(isItem, moveName, activeUnitTarget);
    }

    public void SetTargetName(string s)
    {
        if(targetName == null)
        {
            return;
        }

        targetName.text = s;
    }

    public Text GetTargetName()
    {
        return targetName != null ? targetName : null;
    }

    public void SetActiveUnitTarget(int i)
    {
        activeUnitTarget = i;
    }

    public int GetActiveUnitTarget()
    {
        return activeUnitTarget;
    }

    public void SetMoveName(string s)
    {
        moveName = s;
    }

    public string GetMoveName()
    {
        return moveName;
    }

    public void SetIsItem(bool b)
    {
        isItem = b;
    }

    public bool GetIsItem()
    {
        return isItem;
    }
}
