using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIComposition : MonoBehaviour
{
    public InputActions InputActions => _inputActions;

    private InputActions _inputActions;
 
    public abstract void OnLoad();
    public abstract void OnClose();
    private void LoadComposition(InputActions inputActions)
    {
        _inputActions = inputActions;
        OnLoad();
    }
    private void CloseComposition()
    {
        OnClose();
    }
}
