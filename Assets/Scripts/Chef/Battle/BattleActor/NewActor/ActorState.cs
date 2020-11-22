using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorState 
{
    public event System.Action<ActorState> OnActionPerformedEvent;
    public bool Performed { get; private set; }
    
    private BattleAction _action;
    private NewActor _actor;

    public ActorState(NewActor actor, BattleAction action)
    {
        _actor = actor;
        _action = action;
    }

    public void PerformAction()
    {
        _actor.Act(_action);
        _action.OnActionPerformed += OnActionPerformed;
    }
    private void OnActionPerformed(BattleAction action)
    {
        Performed = true;
        OnActionPerformedEvent?.Invoke(this);
    }
}
