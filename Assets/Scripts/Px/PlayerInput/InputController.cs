using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController
{
    public InputActions InputActions => _inputActionSource;

    [SerializeField] private InputActions _inputActionSource;

    public InputController() { }
    public void EnableControls(InputActions inputActions)
    {
        _inputActionSource = inputActions;
        _inputActionSource.OnMove += OnMove;
        _inputActionSource.OnConfirm += OnConfirm;
        _inputActionSource.OnCancel += OnCancel;
        _inputActionSource.OnSpecial += OnSpecial;
        _inputActionSource.OnMovementZeroed += OnMovementZeroed;
        _inputActionSource.OnCancelRelease += OnCancelReleased;
        _inputActionSource.OnConfirmRelease += OnConfirmReleased;
        _inputActionSource.OnSpecialRelease += OnSpecialReleased;
    }
    public void DisableControls()
    {
        if(_inputActionSource != null)
        {
            _inputActionSource.OnMove -= OnMove;
            _inputActionSource.OnConfirm -= OnConfirm;
            _inputActionSource.OnCancel -= OnCancel;
            _inputActionSource.OnSpecial -= OnSpecial;
            _inputActionSource.OnMovementZeroed -= OnMovementZeroed;
            _inputActionSource.OnCancelRelease -= OnCancelReleased;
            _inputActionSource.OnConfirmRelease -= OnConfirmReleased;
            _inputActionSource.OnSpecialRelease -= OnSpecialReleased;
        }
        _inputActionSource = null;
    }

    public abstract void OnMove(Vector2 v);
    public abstract void OnConfirm();
    public abstract void OnCancel();
    public abstract void OnSpecial();
    public virtual void OnMovementZeroed() { }
    public virtual void OnConfirmReleased() { }
    public virtual void OnCancelReleased() { }
    public virtual void OnSpecialReleased() { }
}
