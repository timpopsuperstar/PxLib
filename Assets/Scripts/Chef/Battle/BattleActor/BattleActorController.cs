using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActorController : InputController
{
    private float _timeLastDodgePerformed;

    public BattleActor Actor
    {
        get
        {
            if(_actor == null)
            {
                _actor = GetComponent<BattleActor>();                
            }
            return _actor;
        }        
    } 
    private BattleActor _actor;
    public override void OnCancel()
    {
        throw new System.NotImplementedException();
    }

    public override void OnConfirm()
    {
        if (!Actor.Busy)
        {
            Actor.State = BattleActor.BattleState.JabLeft;
        }        
    }
    public override void OnMove(Vector2 v)
    {
        if(v == Vector2.down && !Actor.Busy && InputActions.LastTimeMovementZeroed > _timeLastDodgePerformed)
        {
            Actor.State = BattleActor.BattleState.DodgeVertical;
            _timeLastDodgePerformed = Time.time;
            return;
        }
        if (v == Vector2.left && !Actor.Busy && InputActions.LastTimeMovementZeroed > _timeLastDodgePerformed)
        {
            Actor.State = BattleActor.BattleState.DodgeLeft;
            _timeLastDodgePerformed = Time.time;
            return;
        }
        if (v == Vector2.right && !Actor.Busy && InputActions.LastTimeMovementZeroed > _timeLastDodgePerformed)
        {
            Actor.State = BattleActor.BattleState.DodgeRight;
            _timeLastDodgePerformed = Time.time;
            return;
        }

    }
    public override void OnSpecial()
    {
        throw new System.NotImplementedException();
    }
}
