using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Chef/ScriptableObjects/BattleActions/IdleBattleAction")]
public class IdleBattleAction : BattleAction
{
    [SerializeField] private float _waitTime = 0f;
    protected override IEnumerator IEAct(BattleActor actor)
    {
        actor.Anim = actor.AnimSet.idle;
        yield return new WaitForSeconds(_waitTime);
    }
}
