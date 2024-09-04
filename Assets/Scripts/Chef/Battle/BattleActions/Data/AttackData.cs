using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData
{
    public BattleActor.AttackType Type => _type;
    public int Damage => _damage;

    private BattleActor.AttackType _type;
    private int _damage;

    public AttackData(BattleActor.AttackType type, int damage)
    {
        _type = type;
        _damage = damage;
    }
}
