using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PxAnimator))]
[RequireComponent(typeof(PxSpriteShader))]
[RequireComponent(typeof(BoxCollider2D))]
//[RequireComponent(typeof(FieldMapDepthSorting))]

public abstract class FieldActor : MonoBehaviour//,IInteractive
{
    //public event System.Action<FieldActor> OnPositionChange;

    //[SerializeField] FieldActorAnimSet _animSet;
    //[SerializeField] FieldActorMovementData _movementData = new FieldActorMovementData();
    //[SerializeField] Interaction _interaction;

    //public FieldActorAnimSet Anims => _animSet;
    //public FieldActorMovementData Movement => _movementData;
    //public Interaction Interaction => _interaction;


    //[SerializeField] private List<FieldActorAbility> _fieldActorAbilities;
    //public List<FieldActorAbility> FieldActorAbilities => _fieldActorAbilities;
    //public SpriteRenderer SpriteRenderer => _spriteRenderer ? _spriteRenderer : (_spriteRenderer = GetComponent<SpriteRenderer>());
    //private SpriteRenderer _spriteRenderer;
    //public PxAnimator Animator => _animator ? _animator : (_animator = GetComponent<PxAnimator>());
    //private PxAnimator _animator;
    //public PxSpriteShader Shader => _shader ? _shader : (_shader = GetComponent<PxSpriteShader>());
    //private PxSpriteShader _shader;
    //public BoxCollider2D BoxCollider => _boxCollider ? _boxCollider : (_boxCollider = GetComponent<BoxCollider2D>());
    //private BoxCollider2D _boxCollider;
    //public Vector3 Position
    //{
    //    get => transform.position;
    //    set
    //    {
    //        value.x = Mathf.RoundToInt(value.x);
    //        value.y = Mathf.RoundToInt(value.y);
    //        value.z = transform.position.z;
    //        transform.position = value;
    //        OnPositionChange?.Invoke(this);
    //    }
    //}
    //private FieldActorState AIState => new MoveCycleFieldActorState(this);
    //public FieldActorPlayerInputController InputController { get; private set; }
    ////State Controller
    //private FieldActorStateController _stateController;

    //public FieldActorState CurrentState
    //{
    //    get => _stateController.CurrentState;
    //    set => _stateController.CurrentState = value;
    //}
    //public FieldActorState PreviousState => _stateController.PreviousState;

    ////MB Methods
    //protected virtual void Awake()
    //{
    //    _stateController = new FieldActorStateController(this);
    //    //_stateController.CurrentState = new IdleFieldActorState(this);
    //    _stateController.CurrentState = AIState;
    //}
    //private void OnDestroy()
    //{
    //    DisableControls();
    //}

    ////Actor Methods
    //public void EnableControls(InputActions inputActions)
    //{
    //    DisableControls();
    //    InputController = new FieldActorPlayerInputController(this);
    //    InputController.EnableControls(inputActions);
    //}
    //public void DisableControls()
    //{
    //    if (InputController != null)
    //    {
    //        InputController.DisableControls();
    //        InputController = null;
    //        Debug.Log("Removed switch to AI state on disable controls");
    //        //_stateController.CurrentState = AIState;
    //    }
    //}
    //public void ForceCollisionCheck()
    //{
    //    Debug.Log("Checking Forced Collisions");
    //    var collisions = BoxCollider.GetCollisions(gameObject.layer.ToLayerMask());
    //    if (collisions.Count > 0)
    //    {            
    //        foreach (ICollisionTrigger collision in collisions)
    //        {
    //            Debug.Log($"Found something {collision}");
    //            collision.OnCollision(this);  
    //        } 
    //    }
    //}
    //public abstract void OnInteract(FieldActor interactor);
    //public struct StateData
    //{
    //    public Vector3Int Position { get; set; }
    //    public PxMap.LayerHeight LayerHeight { get; set; }
    //    public int ObjectLayer { get; set; }
    //    public PxDirection.Direction Facing { get; set; }
    //    public FieldActorState State { get; set; }

    //    public StateData(FieldActor actor)
    //    {
    //        Position = actor.Position.ToVector3Int();
            
    //        //Layer Height
    //        PxMap.LayerHeight layerHeight;
    //        System.Enum.TryParse(actor.SpriteRenderer.sortingLayerName, out layerHeight);
    //        LayerHeight = layerHeight;
    //        ObjectLayer = actor.gameObject.layer;
    //        Facing = actor.Movement.CurrentDirection;
    //        State = actor.CurrentState;
    //    }
    //}
}
