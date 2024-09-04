using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActions : MonoBehaviour
{ 
    public enum ButtonState { Default, Started, Performed, Canceled }        
    //Public Properties
    public ButtonState LeftMouseButton
    {
        get => _leftMouseButton;
        private set => _leftMouseButton = value;
    }
    public ButtonState RightMouseButton
    {
        get => _rightMouseButton;
        private set => _rightMouseButton = value;
    }
    public float MouseScroll
    {
        get => _mouseScroll;
        private set => _mouseScroll = value;
    }
    public float LastTimeMovementZeroed => _lastTimeMovementZeroed;    

    //Movement Properties
    private float _lastTimeMovementZeroed;
    private struct KeyboardMovementHistory
    {
        public float lastVertDirection;
        public float lastTimeVertZeroed;
        public float lastHorDirection;
        public float lastTimeHorZeroed;
        public bool vertOverrided;
        public bool horOverrided;
    }
    private KeyboardMovementHistory _keyboardMovementHistory = new KeyboardMovementHistory();

    //Mouse Properties
    private ButtonState _leftMouseButton;
    private ButtonState _rightMouseButton;
    private float _mouseScroll;

    //Input Actions
    InputAction moveAction = new InputAction("move");
    InputAction confirmAction = new InputAction("confirm");
    InputAction cancelAction = new InputAction("cancel");
    InputAction optionAction = new InputAction("option");
    InputAction specialAction = new InputAction("special");

    //InputAction mouseLeftClickAction = new InputAction("mouseLeftClick");
    //InputAction mouseRightClickAction = new InputAction("mouseRightClick");
    //InputAction mouseScrollAction = new InputAction("mouseScroll");


    //Events
    public event System.Action<Vector2> OnMove;
    public event System.Action OnMovementZeroed;
    public event System.Action OnConfirm;
    public event System.Action OnConfirmRelease;
    public event System.Action OnCancel;
    public event System.Action OnCancelRelease;
    public event System.Action OnSpecial;
    public event System.Action OnSpecialRelease;
    

    public event System.Action<Vector2> OnMousePosition;
    public event System.Action<Vector2> OnMouseLeftClick;
    public event System.Action<Vector2> OnMouseLeftRelease;
    public event System.Action<Vector2> OnMouseLeftPerform;
    public event System.Action<Vector2> OnMouseRightClick;
    public event System.Action<Vector2> OnMouseRightPerform;
    public event System.Action<Vector2> OnMouseRightRelease;
    public event System.Action<Vector2> OnMouseScroll;

    //Event Tracking Bools
    public bool ConfirmActive { get; private set; }
    public bool CancelActive { get; private set; }
    public bool SpecialActive { get; private set; }

    //MB Methods
    private void Start()
    {
        AddKeyboardBindings();
        AddGamepadBindings();
        EnableInputActions();
    }
    private void Update()
    {
        CaptureControllerInput();
        CaptureMouseInput();
    }

    //Read Controls
    private void ReadMovement()
    {
        Vector2 movement = KeyboardMovementOverrideCheck(moveAction.ReadValue<Vector2>());
        if (movement == Vector2.zero)
        {
            OnMovementZeroed?.Invoke();
            _lastTimeMovementZeroed = Time.time;
        }
        else
        {
            OnMove?.Invoke(movement);
        }
    }
    private void ReadConfirm()
    {
        if (confirmAction.triggered)
        {
            ConfirmActive = true;
            OnConfirm?.Invoke();
        }
        if (cancelAction.ReadValue<float>() == 0 && ConfirmActive)
        {
            ConfirmActive = false;
            OnConfirmRelease?.Invoke();
        }
    }
    private void ReadCancel()
    {
        if (cancelAction.triggered)
        {
            CancelActive = true;
            OnCancel?.Invoke();
        }
        if (cancelAction.ReadValue<float>() == 0 && CancelActive)
        {
            CancelActive = false;
            OnCancelRelease?.Invoke();
        }
    }
    private void ReadSpecial()
    {
        if (specialAction.triggered)
        {
            SpecialActive = true;
            OnSpecial?.Invoke();
        }
        if(specialAction.ReadValue<float>() == 0 && SpecialActive)
        {
            SpecialActive = false;
            OnSpecialRelease?.Invoke();
        }
    }
    private void ReadMousePos()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        OnMousePosition?.Invoke(mousePosition);
    }
    private void ReadLeftMouseButton()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (Mouse.current.leftButton.isPressed)
        {
            if (_leftMouseButton != ButtonState.Started)
            {
                //Debug.Log("started");
                OnMouseLeftClick?.Invoke(mousePosition);
                _leftMouseButton = ButtonState.Started;
            }
        }
        else
        {
            switch (_leftMouseButton)
            {
                case ButtonState.Default:
                    break;
                case ButtonState.Started:
                    //Debug.Log("performed");
                    _leftMouseButton = ButtonState.Performed;
                    OnMouseLeftPerform?.Invoke(mousePosition);
                    break;
                case ButtonState.Performed:
                    //Debug.Log("released");
                    _leftMouseButton = ButtonState.Canceled;
                    OnMouseLeftRelease?.Invoke(mousePosition);
                    break;
                case ButtonState.Canceled:
                    _leftMouseButton = ButtonState.Default;
                    break;
            }
        }
    }
    private void ReadRightMouseButton()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (Mouse.current.rightButton.isPressed)
        {
            if (_rightMouseButton != ButtonState.Started)
            {
                //Debug.Log("started");
                OnMouseRightClick?.Invoke(mousePosition);
                _rightMouseButton = ButtonState.Started;
            }
        }
        else
        {
            switch (_rightMouseButton)
            {
                case ButtonState.Default:
                    break;
                case ButtonState.Started:
                    //Debug.Log("performed");
                    _rightMouseButton = ButtonState.Performed;
                    OnMouseRightPerform?.Invoke(mousePosition);
                    break;
                case ButtonState.Performed:
                    //Debug.Log("released");
                    _rightMouseButton = ButtonState.Canceled;
                    OnMouseRightRelease?.Invoke(mousePosition);
                    break;
                case ButtonState.Canceled:
                    _rightMouseButton = ButtonState.Default;
                    break;
            }
        }
    }
    private void ReadMouseScroll()
    {
        var mouseScroll = Mouse.current.scroll.ReadValue();
        if (mouseScroll == Vector2.zero)
            return;
        OnMouseScroll?.Invoke(mouseScroll);
    }
    private void CaptureMouseInput()
    {
        ReadMousePos();
        ReadLeftMouseButton();
        ReadRightMouseButton();
        ReadMouseScroll();
    }
    private void CaptureControllerInput()
    {
        ReadMovement();
        ReadConfirm();
        ReadCancel();
        ReadSpecial();
    }
    private Vector2 KeyboardMovementOverrideCheck(Vector2 movement)
    {
        if (movement.y == 0 && (moveAction.controls[0].IsPressed() || moveAction.controls[2].IsPressed() || moveAction.controls[4].IsPressed() || moveAction.controls[6].IsPressed()))
        {
            if (!_keyboardMovementHistory.vertOverrided)
            {
                movement.y = _keyboardMovementHistory.lastVertDirection * -1;
            }
            else
            {
                movement.y = _keyboardMovementHistory.lastVertDirection;
            }
            _keyboardMovementHistory.vertOverrided = true;
        }
        if (movement.x == 0 && (moveAction.controls[1].IsPressed() || moveAction.controls[3].IsPressed() || moveAction.controls[5].IsPressed() || moveAction.controls[7].IsPressed()))
        {
            if (!_keyboardMovementHistory.horOverrided)
            {
                movement.x = _keyboardMovementHistory.lastHorDirection * -1;
            }
            else
            {
                movement.x = _keyboardMovementHistory.lastHorDirection;
            }
            _keyboardMovementHistory.horOverrided = true;
        }
        _keyboardMovementHistory.lastHorDirection = movement.x;
        _keyboardMovementHistory.lastVertDirection = movement.y;
        if (movement.x == 0)
            _keyboardMovementHistory.horOverrided = false;
        if (movement.y == 0)
            _keyboardMovementHistory.vertOverrided = false;
        
        return movement;
    }


    //Control Bindings
    private void AddKeyboardBindings()
    {
        moveAction.AddCompositeBinding("Dpad")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Up", "<Keyboard>/w")
            .With("Right", "<Keyboard>/d");
        moveAction.AddCompositeBinding("Dpad")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Up", "<Keyboard>/upArrow")
            .With("Right", "<Keyboard>/rightArrow");

        confirmAction.AddBinding("<Keyboard>/z");
        cancelAction.AddBinding("<Keyboard>/x");
        optionAction.AddBinding("<Keyboard>/e");
        specialAction.AddBinding("<Keyboard>/q");
    }

    private void AddGamepadBindings()
    {
        moveAction.AddBinding("<Gamepad>/leftStick")
            .WithProcessor("StickDeadzone(min=0.2, max=.9)");
        moveAction.AddBinding("<Gamepad>/dpad");

        confirmAction.AddBinding("<Gamepad>/buttonSouth");
        cancelAction.AddBinding("<Gamepad>/buttonEast");
        specialAction.AddBinding("<Gamepad>/buttonWest");
    }
    //Enable/Disable
    private void EnableInputActions()
    {
        moveAction.Enable();
        confirmAction.Enable();
        cancelAction.Enable();
        specialAction.Enable();
    }
    private void DisableInputActions()
    {
        moveAction.Disable();
        confirmAction.Disable();
        cancelAction.Disable();
        specialAction.Disable();
    }
}
