using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Runtime.Remoting;


[ExecuteInEditMode]
public abstract class UIButton : UIWindow
{    
    public enum State { up,hover, down, disabled};
    
    [BoxGroup("Button")] [SerializeField] private UIButtonStyle _buttonStyle;
    private State _previousState;
    private BoxCollider2D _collision;
    private bool _isActive;
    private State _state;

    //Monobehaviours
    public override void Awake()
    {
        base.Awake();
        _collision = GetComponent<BoxCollider2D>();
        SetState(State.up); 
    }
    //Public Methods
    public void SetState(State state)
    {
        if (state == this._state) return;

        _previousState = this._state;
        this._state = state;
        switch (state)
        {
            case State.up:
                Sprite = _buttonStyle.up;
                break;
            case State.hover:
                Sprite = _buttonStyle.hover;
                break;
            case State.down:
                Sprite = _buttonStyle.down;
                break;
            case State.disabled:
                Sprite = _buttonStyle.disabled;
                break;
        }        
    }
    public void EnableControls(InputActions inputActions)
    {
        inputActions.OnPointerPosition += OnPointerPosition;
        inputActions.OnMouseLeftClick += OnClick;
    }
    public void DisableControls(InputActions inputActions)
    {
        inputActions.OnPointerPosition -= OnPointerPosition;
        inputActions.OnMouseLeftClick -= OnClick;
    }
    public void OnPointerPosition(Vector2 v)
    {
        var xMin = _collision.bounds.center.x - _collision.bounds.extents.x;
        var xMax = _collision.bounds.center.x + _collision.bounds.extents.x;
        var yMin = _collision.bounds.center.x - _collision.bounds.extents.y;
        var yMax = _collision.bounds.center.x + _collision.bounds.extents.y;

        if(v.x > xMin && v.x < xMax)
        {
            if(v.y > yMin && v.y < yMax)
            {
                OnPointerOver();
                return;
            }
        }
        if (!_isActive && _state != State.up && _state != State.disabled)
        {
            OnPointerExit();
        }
    }
    public void OnClick(Vector2 v)
    {
        if (PointerIsOver(v)) StartCoroutine(Activate());        
    }    

    public bool PointerIsOver(Vector2 v)
    {
        var xMin = _collision.bounds.center.x - _collision.bounds.extents.x;
        var xMax = _collision.bounds.center.x + _collision.bounds.extents.x;
        var yMin = _collision.bounds.center.x - _collision.bounds.extents.y;
        var yMax = _collision.bounds.center.x + _collision.bounds.extents.y;

        if (v.x > xMin && v.x < xMax)
        {
            if (v.y > yMin && v.y < yMax)
            {
                return true;
            }
        }
        return false;
    }
    //Private Methods
    private void OnPointerOver()
    {
        if (_state != State.disabled && !_isActive)
        {
            SetState(State.hover);
        }
    }
    private void OnPointerExit()
    {
        SetState(_previousState);
    }
    private void OnPointerRightClick()
    {
        if (!_isActive)
        {
            StartCoroutine(Activate());
        }
    }
    private IEnumerator Activate()
    {
        if (_isActive)
            yield break;
        yield return StartCoroutine(AnimateButton());
        OnActivate();
    }
    public virtual IEnumerator AnimateButton()
    {
        _isActive = true;
        var pos = transform.position;
        pos.y -= 1;
        transform.position = pos;
        SetState(State.down);
        yield return new WaitForSeconds(.10f);
        pos.y += 1;
        transform.position = pos;
        SetState(State.up);
        yield return new WaitForSeconds(.06f);
        _isActive = false;
    }
    protected override void OnSpriteRendererInit()
    {
        Sprite = _buttonStyle.up;
    }
    public abstract void OnActivate();
}
