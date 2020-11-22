using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleActorStats
{
    public enum Type
    {
        Player,
        Enemy
    };

    public event System.Action OnDead;

    public int CurrentHp => _hp;
    public int MaxHp => _maxHp;
    public int CurrentBp => _bp;
    public int MaxBp => _maxBp;
    public int Speed => _speed;
    public int Power => _power;
    public Type ActorType => _type;

    private int _hp;
    private int _maxHp;
    private int _bp;
    private int _maxBp;
    private int _speed;
    private int _power;
    private Type _type;

    public BattleActorStats(Type type, int maxHp, int maxBp, int speed, int attack )
    {
        _type = type;
        _maxHp = maxHp;
        _hp = maxHp;
        _maxBp = maxBp;
        _bp = maxBp;
        _speed = speed;
        _power = attack;
    }

    public void AddHp(int hpMod)
    {
        int adjustedHp = _hp + hpMod;
        _hp = Mathf.Clamp(adjustedHp, 0, _maxHp);
        if (_hp == 0)
        {
            OnDead?.Invoke();
        }            
    }
}
