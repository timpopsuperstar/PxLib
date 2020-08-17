using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    InputActions inputActionSource;
   

    public void EnableControls(InputActions inputActions)
    {
        inputActionSource = inputActions;
        inputActions.OnMove += OnMove;
        inputActions.OnConfirm += OnConfirm;
        inputActions.OnCancel += OnCancel;
        inputActions.OnSpecial += OnSpecial;
    }
    public void DisableControls(InputActions inputActions)
    {
        inputActionSource = null;
        inputActions.OnMove -= OnMove;
        inputActions.OnConfirm -= OnConfirm;
        inputActions.OnCancel -= OnCancel;
        inputActions.OnSpecial -= OnSpecial;
    }

    private void OnMove(Vector2 v)
    {

    }

    private void OnConfirm()
    {
        Debug.Log("Confirm");
    }
    private void OnCancel()
    {

    }

    private void OnSpecial()
    {

    }
}
