using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiState : ScriptableObject
{
    public event System.Action<AiState> OnStatePerformed;

    public void Perform(BattleActor actor)
    {
        MonoBehaviour mb = actor.GetComponent<MonoBehaviour>();
        mb.StartCoroutine(IERunAIStateLogic(actor, mb));
    }
    private IEnumerator IERunAIStateLogic(BattleActor actor, MonoBehaviour mb)
    {
        yield return mb.StartCoroutine(IEPerform(actor));
        OnStatePerformed?.Invoke(this);
    }
    protected abstract IEnumerator IEPerform(BattleActor actor);
}
