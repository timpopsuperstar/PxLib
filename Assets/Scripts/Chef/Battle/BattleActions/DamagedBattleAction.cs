using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Chef/ScriptableObjects/BattleActions/DamagedBattleAction")]
public class DamagedBattleAction : BattleAction
{
    protected override IEnumerator IEAct(BattleActor actor)
    {
        actor.Anim = actor.AnimSet.damaged;
        yield return new WaitForSeconds(actor.AnimSet.damaged.Duration);        
        if(actor.State == BattleActor.BattleState.Dead)
        {
            Debug.Log("def dead");
        }
        else
        {
            actor.State = BattleActor.BattleState.Idle;
        }
    }
}

