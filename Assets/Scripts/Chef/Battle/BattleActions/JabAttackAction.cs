using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Chef/ScriptableObjects/BattleActions/JabAttackAction")]
public class JabAttackAction : AttackAction
{
    public override float Duration => _anticipateDuration + _attackDuration;

    private const float _anticipateDuration = .15f;
    private const float _attackDuration = .2f;
    private const float _anticipateOffset = 6f;
    private const float _attackOffset = 16f;
    
    protected override IEnumerator IEAct(BattleActor actor)
    {
        actor.Animator.LoadAnim(attackAnim);
        actor.Animator.CurrentFrame = 0;
        Vector2 startPos = actor.HomePosition;
        Vector2 midPos = startPos;
        switch (actor.Stats.ActorType)
        {
            case BattleActorStats.Type.Player:
                midPos.y += -_anticipateOffset;
                break;
            case BattleActorStats.Type.Enemy:
                midPos.y += _anticipateOffset;
                break;
        }
        Vector2 endPos = actor.HomePosition;
        yield return IEMove(actor, _anticipateDuration, startPos, midPos, endPos);
        actor.Animator.CurrentFrame = 1;
        yield return IEJab(actor, _attackDuration);
        actor.State = BattleActor.BattleState.Idle;
    }
    private IEnumerator IEMove(BattleActor actor, float duration, Vector2 p0, Vector2 p1, Vector2 p2)
    {
        for (float t = 0; t <= duration; t += Time.deltaTime)
        {            
            actor.transform.position = Bezier.GetPoint(p0, p1, p2, t / duration);
            yield return new WaitForEndOfFrame();
        }
        actor.transform.position = actor.HomePosition;
    }
    private IEnumerator IEJab(BattleActor actor, float duration)
    {
        Vector2 p0 = actor.HomePosition;
        Vector2 p1 = p0;
        switch (actor.Stats.ActorType)
        {
            case BattleActorStats.Type.Player:
                p1.y += _attackOffset;
                break;
            case BattleActorStats.Type.Enemy:
                p1.y += -_attackOffset;
                break;
        }
        Vector2 p2 = actor.HomePosition;
        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            if (!actor.AttackLanded && t >= .1f)
            {
                actor.transform.position = Bezier.GetPoint(p0, p1, p2, t / duration);
                AttackData attackData = new AttackData(BattleActor.AttackType.Jab, actor.Stats.Power);
                actor.InvokeAttack(attackData);
            }
            yield return new WaitForEndOfFrame();
        }
        actor.transform.position = actor.HomePosition;
    }
}
