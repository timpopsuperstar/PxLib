using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : MonoBehaviour
{
    public InputActions InputActions => _inputActionSource;

    [SerializeField] private InputActions _inputActionSource;
   

    public void EnableControls(InputActions inputActions)
    {
        _inputActionSource = inputActions;
        inputActions.OnMove += OnMove;
        inputActions.OnConfirm += OnConfirm;
        inputActions.OnCancel += OnCancel;
        inputActions.OnSpecial += OnSpecial;
    }
    public void DisableControls(InputActions inputActions)
    {
        _inputActionSource = null;
        inputActions.OnMove -= OnMove;
        inputActions.OnConfirm -= OnConfirm;
        inputActions.OnCancel -= OnCancel;
        inputActions.OnSpecial -= OnSpecial;
    }

    public abstract void OnMove(Vector2 v);
    public abstract void OnConfirm();
    public abstract void OnCancel();
    public abstract void OnSpecial();
}
