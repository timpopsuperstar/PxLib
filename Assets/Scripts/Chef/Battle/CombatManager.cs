using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    
    public BattleActor hero;
    public BattleActor enemy;

    private InputActions _inputActions;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        _inputActions = GetComponent<InputActions>();
    }

    private void Start()
    {
        hero.Controller.EnableControls(_inputActions);
        hero.Opponent = enemy;
        enemy.Opponent = hero;
        hero.Stats = BattleActorStatsData.Hero;
        enemy.Stats = BattleActorStatsData.Egg;
        //enemy.State = BattleActor.BattleState.JabLeft;
        enemy.Ai.RunController();
    }
    public event System.Action OnTestAction;
    private void Test()
    {

    }
}
