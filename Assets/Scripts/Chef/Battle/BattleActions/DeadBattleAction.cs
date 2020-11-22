using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Chef/ScriptableObjects/BattleActions/DeadBattleAction")]
public class DeadBattleAction : BattleAction
{   
    protected override IEnumerator IEAct(BattleActor actor)
    {
        Debug.Log("Dead");
        actor.Anim = actor.AnimSet.dead;
        yield return new WaitForSeconds(.1f);
    }
}

