using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleType
{
    [SerializeField] List<string> enemies       = new List<string>();
    [SerializeField] List<string> rewardItems   = new List<string>();

    [SerializeField] int rewardEXP              = 0;

    public List<string> GetEnemies()
    {
        return enemies;
    }

    public List<string> GetRewardItems()
    {
        return rewardItems;
    }

    public int GetRewardEXP()
    {
        return rewardEXP;
    }
}
