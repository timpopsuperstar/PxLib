using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SimpleAnimator))]
[RequireComponent(typeof(BattleActorController))]
[RequireComponent(typeof(AiController))]
public class BattleActor : MonoBehaviour
{    
    public enum BattleState
    {
        Idle,
        Damaged,
        Dead,
        DodgeVertical,
        DodgeLeft,
        DodgeRight,
        JabLeft,
        JabRight,
        Special,
        Dazed
    };    
    public enum AttackType
    {
        Jab,
        LeftHook,
        RightHook
    }

    public event System.Action<AttackData> OnAttack;

    public BattleState State
    {
        get => _state;
        set
        {
            SetBattleState(value);
        }
    }
    public Anim Anim
    {
        get => Animator.CurrentAnim;
        set => Animator.PlayAnim(value);
    }
    public BattleActorDefaultAnimSet AnimSet
    {
        get => _defaultAnimSet;
        set => _defaultAnimSet = value;
    }
    public bool Busy 
    {
        get => _busy;
        set => _busy = value;
    }
    public bool AttackLanded
    {
        get => _attackHit;
        set => _attackHit = value;
    }
    public AiController Ai => GetComponent<AiController>();
    public Vector2 HomePosition { get; private set; }
    public BattleState PreviousState => _previousState;
    public SimpleAnimator Animator => _animator;
    public AttackAction[] AttackActions => _attackActions;
    public BattleActorController Controller => _controller;
    public BattleActor Opponent
    {
        get => _opponent;
        set
        {
            _opponent = value;
            _opponent.OnAttack += OnOpponentAttack;
        }
    }
    public BattleActorStats Stats
    {
        get => _stats;
        set 
        {
            _stats = value;
            _stats.OnDead += OnDead;
        } 
    }

    [SerializeField] private BattleActorDefaultAnimSet _defaultAnimSet;
    [SerializeField] private BattleState _state;
    [SerializeField] private AttackAction[] _attackActions;

    private BattleState _previousState = BattleState.Idle;
    private SimpleAnimator _animator;
    private BattleActorController _controller;
    private bool _busy = false;
    private bool _attackHit;
    private BattleActor _opponent;
    private BattleActorStats _stats;

    public void InvokeAttack(AttackData attackData)
    {
        if(Opponent.State != BattleState.Damaged && !AttackLanded)
        {
            OnAttack?.Invoke(attackData);
        }        
    }

    private void Awake()
    {
        _animator = GetComponent<SimpleAnimator>();
        _controller = GetComponent<BattleActorController>();
        HomePosition = transform.position;
        State = BattleState.Idle;
    }
    private void OnDisable()
    {
        if(_stats != null)
        {
            _stats.OnDead -= OnDead;
        }        
    }
    private void SetBattleState(BattleState state)
    {
        transform.position = HomePosition;
        _busy = true;
        if (_previousState != state)
        {
            _previousState = _state;
        }
        _state = state;
        switch (_state)
        {
            case BattleState.Idle:
                ResetCombatData();
                CombatResources.instance.idleBattleAction.Act(this);
                break;
            case BattleState.Damaged:
                CombatResources.instance.damagedBattleAction.Act(this);
                break;
            case BattleState.Dead:
                CombatResources.instance.deadBattleAction.Act(this);
                break;
            case BattleState.DodgeVertical:
                CombatResources.instance.dodgeVerticalBattleAction.Act(this);
                break;
            case BattleState.DodgeLeft:
                CombatResources.instance.dodgeLeftBattleAction.Act(this);
                break;                        
            case BattleState.DodgeRight:
                CombatResources.instance.dodgeRightBattleAction.Act(this);
                break;
            case BattleState.JabLeft:
                AttackActions[0].Act(this);
                break;                
            case BattleState.JabRight:
                AttackActions[1].Act(this);
                break;
            case BattleState.Special:
                AttackActions[2].Act(this);
                break;
            case BattleState.Dazed:
                Debug.Log("Implement Dazed");
                break;
        }
    }
    private void OnOpponentAttack(AttackData attackData)
    {
        switch (attackData.Type)
        {
            case AttackType.Jab:
                State = BattleState.Damaged;
                Stats.AddHp(-attackData.Damage);
                Opponent.AttackLanded = true;
                break;
        }
    }
    private void ResetCombatData()
    {
        _attackHit = false;
        _busy = false;
    }

    private void OnDead()
    {
        Debug.Log("I'm Dead");
        State = BattleState.Dead;
    }
}
