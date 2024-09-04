using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CombatResources : MonoBehaviour
{
    public static CombatResources instance;

    [Header("Default Battle Actions")]
    public BattleAction idleBattleAction;
    public BattleAction damagedBattleAction;
    public BattleAction deadBattleAction;
    public BattleAction dodgeVerticalBattleAction;
    public BattleAction dodgeLeftBattleAction;
    public BattleAction dodgeRightBattleAction;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
