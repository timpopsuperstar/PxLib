using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackAction : BattleAction
{    
    public abstract float Duration
    { 
        get; 
    }
    public Anim attackAnim;    
}
