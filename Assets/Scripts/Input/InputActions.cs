using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActions : MonoBehaviour
{
    //Properties
    public enum ButtonState { Default, Started, Performed, Canceled}
    ButtonState leftMouseButton;
    ButtonState rightMouseButton;
    float mouseScroll;

    public ButtonState LeftMouseButton
    {
        get
        {
            return leftMouseButton;
        }
        private set
        {
            leftMouseButton = value;
        }
    }
    public ButtonState RightMouseButton
    {
        get
        {
            return rightMouseButton;
        }
        private set
        {
            rightMouseButton = value;
        }
    }
    public float MouseScroll
    {
        get
        {
            return mouseScroll;
        }
        private set
        {
            mouseScroll = value;
        }
    }

    //Input Actions
    InputAction moveAction = new InputAction("move");
    InputAction confirmAction = new InputAction("confirm");
    InputAction cancelAction = new InputAction("cancel");
    InputAction optionAction = new InputAction("option");
    InputAction specialAction = new InputAction("special");

    InputAction mouseLeftClickAction = new InputAction("mouseLeftClick");
    InputAction mouseRightClickAction = new InputAction("mouseRightClick");
    InputAction mouseScrollAction = new InputAction("mouseScroll");

    //Delegates
    public delegate void OnMoveAction(Vector2 v);
    public event OnMoveAction OnMove;
    public delegate void OnConfirmAction();
    public event OnConfirmAction OnConfirm;
    public delegate void OnCancelAction();
    public event OnCancelAction OnCancel;
    public delegate void OnSpecialAction();
    public event OnSpecialAction OnSpecial;

    public delegate void OnPointerPositionAction(Vector2 v);
    public event OnPointerPositionAction OnPointerPosition;
    public delegate void OnMouseLeftClickAction(Vector2 v);
    public event OnMouseLeftClickAction OnMouseLeftClick;
    public delegate void OnMouseRightClickAction();
    public event OnMouseRightClickAction OnMouseRightClick;
    public delegate void OnMouseScrollAction();
    public event OnMouseScrollAction OnMouseScroll;

    void Start()
    {     
        AddKeyboardBindings();
        AddGamepadBindings();

        EnableInputActions();
    }
    void Update()
    {
        var movement = moveAction.ReadValue<Vector2>();
        OnMove?.Invoke(movement);

        if (confirmAction.triggered)
        {
            OnConfirm?.Invoke();
        }
        if (cancelAction.triggered)
        {
            OnCancel?.Invoke();
        }
        if (specialAction.triggered)
        {
            OnSpecial?.Invoke();
        }

        //MouseStuff
        var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        OnPointerPosition?.Invoke(mousePosition);

        if (Mouse.current.leftButton.isPressed)
        {
            if(leftMouseButton != ButtonState.Started)
            {
                Debug.Log("starting");
                leftMouseButton = ButtonState.Started;
            }
        }
        else
        {
            switch (leftMouseButton)
            {
                case ButtonState.Default:
                    break;
                case ButtonState.Started:
                    leftMouseButton = ButtonState.Performed;
                    break;
                case ButtonState.Performed:
                    leftMouseButton = ButtonState.Canceled;
                    break;
                case ButtonState.Canceled:
                    leftMouseButton = ButtonState.Default;
                    break;
            }
        }

        if (leftMouseButton == ButtonState.Canceled)
        {
            OnMouseLeftClick?.Invoke(mousePosition);
            Debug.Log("executed mouse click");
        }
    }



    private void AddKeyboardBindings()
    {
        moveAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        moveAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/leftArrow")
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
