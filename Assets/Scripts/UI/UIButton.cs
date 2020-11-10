using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Runtime.Remoting;


[RequireComponent(typeof(BoxCollider2D))]
[ExecuteInEditMode]
public abstract class UIButton : UIWindow
{    
    public enum State { up,hover, down, disabled};

    public delegate UIButton OnClickCallback(UIButton b);
    public event OnClickCallback OnClickEvent;

    public InputActions InputActions { get; private set; }

    [BoxGroup("Button")] [SerializeField] [OnValueChanged("OnButtonStyleChangedCallback")] private UIButtonStyle _buttonStyle;
    private State _previousState;
    private BoxCollider2D _collision;
    private bool _isActive;
    private State _state;
    private InputActions _inputActions;

    //Monobehaviours
    protected override void Awake()
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
        if (InputActions)
        {
            DisableControls();
        }
        InputActions = inputActions;
        InputActions.OnPointerPosition += OnPointerPosition;
        InputActions.OnMouseLeftClick += OnClick;
    }
    public void DisableControls()
    {
        InputActions.OnPointerPosition -= OnPointerPosition;
        InputActions.OnMouseLeftClick -= OnClick;
    }

    //Protected methods
    protected virtual IEnumerator AnimateButton()
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
    protected abstract void OnClick();

    //Editor methods
    protected void OnButtonStyleChangedCallback()
    {
        if (_buttonStyle)
        {
            Sprite = _buttonStyle.up;
        }
        else
        {
            Sprite = null;
        }

    }

    //Private methods
    private void OnPointerPosition(Vector2 v)
    {        
        var xMin = _collision.bounds.center.x - _collision.bounds.extents.x;
        var xMax = _collision.bounds.center.x + _collision.bounds.extents.x;
        var yMin = _collision.bounds.center.y - _collision.bounds.extents.y;
        var yMax = _collision.bounds.center.y + _collision.bounds.extents.y;
        if (v.x > xMin && v.x < xMax)
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
    private void OnClick(Vector2 v)
    {
        if (PointerIsOver(v)) StartCoroutine(Activate());
        OnClickEvent?.Invoke(this);
    }    
    private bool PointerIsOver(Vector2 v)
    {
        var xMin = _collision.bounds.center.x - _collision.bounds.extents.x;
        var xMax = _collision.bounds.center.x + _collision.bounds.extents.x;
        var yMin = _collision.bounds.center.y - _collision.bounds.extents.y;
        var yMax = _collision.bounds.center.y + _collision.bounds.extents.y;

        if (v.x > xMin && v.x < xMax)
        {
            if (v.y > yMin && v.y < yMax)
            {
                return true;
            }
        }
        return false;
    }
    private void OnPointerOver()
    {
        if (_state != State.disabled && !_isActive && _state != State.hover)
        {            
            SetState(State.hover);
        }
    }
    private void OnPointerExit()
    {
        SetState(_previousState);
    }
    private IEnumerator Activate()
    {
        if (_isActive)
            yield break;
        yield return StartCoroutine(AnimateButton());
        OnClick();

    }

    //Editor methods
    protected override void OnSizeChanged()
    {
        base.OnSizeChanged();
        _collision.size = Size;
    }
    protected override void OnRenderBounds()
    {
        base.OnRenderBounds();
        _collision.size = Size;
    }
}
