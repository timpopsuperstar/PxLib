﻿using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Chef/ScriptableObjects/BattleActions/DodgeRightBattleAction")]
public class DodgeRightBattleAction : BattleAction
{
    private const int dodgeSteps = 12;

    protected override IEnumerator IEAct(BattleActor actor)
    {
        actor.Anim = actor.AnimSet.dodgeRight;
        yield return IEMove(actor);
        actor.State = BattleActor.BattleState.Idle;
    }

    private IEnumerator IEMove(BattleActor actor)
    {
        Vector2 p0 = actor.HomePosition;
        Vector2 p1 = new Vector2(p0.x + 40f, p0.y);
        Vector2 p2 = actor.HomePosition;

        for (float t = 0; t <= 1; t += (float)1f / dodgeSteps)
        {
            actor.transform.position = Bezier.GetPoint(p0, p1, p2, t);
            yield return new WaitForSeconds(1 / 60f);
        }
        actor.transform.position = actor.HomePosition;
    }
}
