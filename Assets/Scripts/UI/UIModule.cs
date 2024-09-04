using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public abstract class UIModule : UIFrame
{
    public UIScene ParentScene { get; private set; }

    public virtual void Load(UIScene parentScene)
    {
        ParentScene = parentScene;
        OnLoad();
    }    
    public virtual void Close()
    {
        OnClose();
    }
    protected abstract void OnLoad();
    protected abstract void OnClose();
}
