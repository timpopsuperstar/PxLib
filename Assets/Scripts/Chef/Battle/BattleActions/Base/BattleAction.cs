using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleAction : ScriptableObject
{
    public event System.Action<BattleAction> OnActionPerformed;
    //public abstract void Act(BattleActor actor);
    public void Act(BattleActor actor)
    {
        MonoBehaviour mb = actor.GetComponent<MonoBehaviour>();
        mb.StartCoroutine(IEBattleActionSequence(actor, mb));
    }
    private IEnumerator IEBattleActionSequence(BattleActor actor, MonoBehaviour mb)
    {
        yield return mb.StartCoroutine(IEAct(actor));
        OnActionPerformed?.Invoke(this);
    }
    protected abstract IEnumerator IEAct(BattleActor actor);
}
