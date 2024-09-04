using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MouseInputController 
{
    public InputActions InputActions => _inputActionSource;

    [SerializeField] private InputActions _inputActionSource;
    public void EnableControls(InputActions inputActions)
    {
        _inputActionSource = inputActions;
        _inputActionSource.OnMousePosition += OnMousePosition;
        _inputActionSource.OnMouseScroll += OnMouseScroll;
        _inputActionSource.OnMouseLeftClick += OnMouseLeftClick;
        _inputActionSource.OnMouseLeftPerform += OnMouseLeftPerform;
        _inputActionSource.OnMouseLeftRelease += OnMouseLeftRelease;
        _inputActionSource.OnMouseRightClick += OnMouseRightClick;
        _inputActionSource.OnMouseRightPerform += OnMouseRightPerform;
        _inputActionSource.OnMouseRightRelease += OnMouseRightRelease;
    }
    public void DisableControls()
    {
        if (_inputActionSource != null)
        {
            _inputActionSource.OnMousePosition -= OnMousePosition;
            _inputActionSource.OnMouseScroll -= OnMouseScroll;
            _inputActionSource.OnMouseLeftClick -= OnMouseLeftClick;
            _inputActionSource.OnMouseLeftPerform -= OnMouseLeftPerform;
            _inputActionSource.OnMouseLeftRelease -= OnMouseLeftRelease;
            _inputActionSource.OnMouseRightClick -= OnMouseRightClick;
            _inputActionSource.OnMouseRightPerform -= OnMouseRightPerform;
            _inputActionSource.OnMouseRightRelease -= OnMouseRightRelease;
        }
        _inputActionSource = null;
    }

    public abstract void OnMousePosition(Vector2 v);
    public abstract void OnMouseScroll(Vector2 v);
    public abstract void OnMouseLeftClick(Vector2 v);
    public abstract void OnMouseLeftRelease(Vector2 v);
    public abstract void OnMouseLeftPerform(Vector2 v);
    public abstract void OnMouseRightClick(Vector2 v);
    public abstract void OnMouseRightPerform(Vector2 v);
    public abstract void OnMouseRightRelease(Vector2 v);
}
