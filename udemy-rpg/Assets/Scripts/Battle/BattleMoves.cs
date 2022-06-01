using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleMoves
{
    [SerializeField] BattleEffects moveEffects = null;
    
    [SerializeField] int 
        movePwr = 0, 
        moveCost = 0;
    
    [SerializeField] string moveName = "";

    public string GetMoveName()
    {
        return moveName;
    }

    public int GetMovePwr()
    {
        return movePwr;
    }

    public int GetMoveCost()
    {
        return moveCost;
    }

    public BattleEffects GetMoveEffects()
    {
        return moveEffects;
    }
}
