using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Chef/ScriptableObjects/Ai/States/IdleAiState")]

public class IdleAiState : AiState
{
    public float idleTime;
    protected override IEnumerator IEPerform(BattleActor actor)
    {
        actor.State = BattleActor.BattleState.Idle;
        yield return new WaitForSeconds(idleTime);
    }
}
