using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIScene : UIFrame
{
    //public override Bounds Bounds
    //{
    //    get => _camera.PixelBounds;
    //}
    public InputActions InputActions { get; private set; }

    public abstract void OnLoad();
    public abstract void OnClose();

    
    public void Load(InputActions inputActions)
    {
        InputActions = inputActions;
        OnLoad();
    }
    public void Close()
    {
        OnClose();
    }
}
