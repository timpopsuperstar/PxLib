using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Chef/ScriptableObjects/Ai/States/JabAiState")]

public class JabAiState : AiState
{
    public AttackAction attackAction;

    protected override IEnumerator IEPerform(BattleActor actor)
    {
        actor.State = BattleActor.BattleState.JabLeft;        
        yield return new WaitForSeconds(attackAction.Duration);        
    }
}
