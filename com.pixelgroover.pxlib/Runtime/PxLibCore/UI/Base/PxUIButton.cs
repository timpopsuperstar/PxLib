using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class PxUIButton : PxGraphic
{
    public enum ButtonState { Idle, Hover, Selected, Disabled}
    //Events
    public event System.Action<PxUIButton> OnClickEvent;

    //Properties
    public ButtonState State
    {
        get => _state;
        set 
        {
            switch (value)
            {
                case ButtonState.Idle:
                    OnIdleState();
                    _state = value;
                    break;
                case ButtonState.Hover:
                    OnHoverState();
                    _state = value;
                    break;
                case ButtonState.Selected:
                    OnSelectedState();
                    _state = value;
                    break;
                case ButtonState.Disabled:
                    OnDisabledState();
                    _state = value;
                    break;
            }
        }
    }
    private ButtonState _state = ButtonState.Idle;


    //Mouse Event Methods
    protected virtual void OnClick()
    {
        if (State != ButtonState.Disabled)
        {
            State = ButtonState.Selected;
        }
    }
    protected virtual void OnHover()
    {
        if (State == ButtonState.Idle)
        {
            State = ButtonState.Hover;
        }
    }
    protected virtual void OnExit()
    {
        if (State == ButtonState.Hover)
        {
            State = ButtonState.Idle;
        }
    }

    //State Change Methods
    protected abstract void OnIdleState();
    protected abstract void OnHoverState();
    protected abstract void OnSelectedState();
    protected abstract void OnDisabledState();

    //Mouse Control
    private void OnMouseEnter()
    {
        if(State != ButtonState.Selected && State != ButtonState.Disabled)
        {
            OnHover();
        }
    }
    private void OnMouseExit()
    {
        if(State == ButtonState.Hover)
        {
            OnExit();
        }        
    }
    private void OnMouseDown()
    {
        if(State != ButtonState.Disabled)
        {
            OnClick();
            OnClickEvent?.Invoke(this);
        }        
    }
    private void OnMouseOver() { }
    private void OnMouseUpAsButton() { }
}
