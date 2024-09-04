using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Chef/ScriptableObjects/BattleActions/DodgeVerticalBattleAction")]
public class DodgeVerticalBattleAction : BattleAction
{
    private const int dodgeSteps = 12;
    protected override IEnumerator IEAct(BattleActor actor)
    {
        actor.Anim = actor.AnimSet.dodgeVertical;
        yield return IEMove(actor);
        actor.State = BattleActor.BattleState.Idle;
    }
    private IEnumerator IEMove(BattleActor actor)
    {
        Vector2 p0 = actor.HomePosition;
        Vector2 p1 = new Vector2(p0.x, p0.y + -16f);
        Vector2 p2 = actor.HomePosition;

        for (float t = 0; t <= 1; t+= (float)1f / dodgeSteps)
        {
            actor.transform.position = Bezier.GetPoint(p0, p1, p2, t);
            yield return new WaitForSeconds(1/60f);
        }
        actor.transform.position = actor.HomePosition;
    }
}

