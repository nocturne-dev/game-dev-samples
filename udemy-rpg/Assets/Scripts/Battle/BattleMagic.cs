using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMagic : MonoBehaviour
{
    [SerializeField] Text nameText = null, costText = null;

    [SerializeField] int spellCost = 0;
    [SerializeField] string spellName = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Magic()
    {
        BattleUnits unit = FindObjectOfType<BattleManager>().GetCurrentActiveUnit();
        if (unit.GetCurrMP() >= spellCost)
        {
            FindObjectOfType<BattleManager>().CloseMagicMenu();
            FindObjectOfType<BattleManager>().OpenEnemyTargetMenu(spellName);
            unit.SetCurrMP(unit.GetCurrMP() - spellCost);
        }
        else{
            // Let player know there isn't enough MP
            FindObjectOfType<BattleManager>().GetBattleNotification().SetNotificationText("Not Enough MP!");
            FindObjectOfType<BattleManager>().GetBattleNotification().Activate();
            FindObjectOfType<BattleManager>().CloseMagicMenu();
        }
    }

    public void SetSpellName(string s)
    {
        spellName = s;
        nameText.text = nameText != null ? spellName : null;
    }

    public string GetSpellName()
    {
        return spellName;
    }

    public void SetSpellCost(int i)
    {
        spellCost = i;
        costText.text = costText != null ? spellCost.ToString() : null;
    }
}
