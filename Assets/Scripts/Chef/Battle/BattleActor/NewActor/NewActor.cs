using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActorStateController))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(SimpleAnimator))]
public class NewActor : MonoBehaviour
{
    //Enums
    public enum Type
    {
        Player,
        Enemy
    }
    public enum ControllerType
    {
        Player, 
        Cpu
    }

    //Events
    public event System.Action<AttackAction> OnAttack;

    //Public Properties
    public Vector2 HomePosition
    {
        get
        {
            switch (_type)
            {
                case Type.Player:
                    return CombatData.PlayerHomePosition;
                case Type.Enemy:
                    return CombatData.EnemyHomePosition;
            }
            return Vector2.zero;
        }
    }
    public ActorState CurrentState 
    { 
        get => _stateController.State;
        set => _stateController.State = value;
    } 

    //Actor Data
    public new BattleActorStats Stats { get; set; }    
    [SerializeField] private BattleActorDefaultAnimSet _defaultAnimSet;

    //New Combat State Props
    private BattleAction _currentAction;
    private List<BattleAction> _previousActions = new List<BattleAction>();

    //Serialized Private Properties
    [SerializeField] private Type _type;
    [SerializeField] private ControllerType _controllerType;
    [SerializeField] private AttackAction[] _attackActions;
    //Private Properties
    private BattleActorStats _stats;
    private BattleActor _opponent;
    //Component References
    private SimpleAnimator _animator;
    private BattleActorController _controller;
    private ActorStateController _stateController;

    //Public Methods
    public void SetState()
    {

    }
    public void Act(BattleAction action)
    {
        if(_currentAction != null)
        {
            _previousActions.Add(_currentAction);
        }        
        _currentAction = action;
        action.OnActionPerformed += OnActionPerformed;
        //action.Act(this as BattleActor);
    }
    public void Attack(AttackAction attack)
    {
        OnAttack?.Invoke(attack);
    }
    public void OnAttacked(AttackAction attack)
    {
        switch (_type)
        {
            case Type.Player:
                break;
            case Type.Enemy:
                break;
        }
    }
    //Private Methods
    private void OnActionPerformed(BattleAction action)
    {
        action.OnActionPerformed -= OnActionPerformed;
        _previousActions.Add(_currentAction);
        _currentAction = null;
    }
    //MonoBehaviour Methods
    private void Awake()
    {
        _animator = GetComponent<SimpleAnimator>();
        _controller = GetComponent<BattleActorController>();
        _stateController = GetComponent<ActorStateController>();
    }
}
