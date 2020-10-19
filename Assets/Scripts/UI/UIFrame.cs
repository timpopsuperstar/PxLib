using PxAlignment;
using PxMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Runtime.CompilerServices;
using TMPro;


public class UIFrame : MonoBehaviour
{
    //Public Properties
    public virtual Bounds Bounds
    {
        get => _bounds;
        set
        {
            _bounds.center = value.center.ToInt();
            _bounds.size = value.size.ToInt();         
            transform.position = _bounds.center;
            SetFrameAlignment();
            OnSizeChanged();
            OnPositionChanged();
        }
    }
    public Alignment FrameAlignment
    {
        get => _frameAlignment;
        set
        {
            if (_anchor)
            {                
                _frameAlignment = value;
                SetFrameAlignment();
            }
            else
            {
                _frameAlignment = Alignment.None;
            }
        }
    }
    public UIFrame Anchor
    {
        get => _anchor;
        set
        {
            AnchorToFrame(value, FrameAlignment);
        }
    }
    public virtual Vector2 Position
    {
        get => transform.position;
        set
        {
            Bounds = new Bounds(value, Bounds.size);
        }
    }
    public virtual Vector2 Size
    {
        get => Bounds.size;
        set
        {
            Bounds = new Bounds(Bounds.center, value);
        }
    }

    //Private Fields
    [BoxGroup("Frame")] [SerializeField] [OnValueChanged("OnParentChangedEditorCallback")] private UIFrame _anchor;
    [BoxGroup("Frame")] [SerializeField] [OnValueChanged("OnSizeToAnchorChangedEditorCallback")] [ShowIf("HasParent")] private bool _sizeToAnchor;
    [BoxGroup("Frame")] [SerializeField] [OnValueChanged("OnSafeZoneChangedEditorCallback")] [ShowIf("HasParent")] private bool _useSafeZone;
    [BoxGroup("Frame")] [SerializeField] [OnValueChanged("OnAlignmentChangedEditorCallback")] [ShowIf("HasParent")] private Alignment _frameAlignment;
    [BoxGroup("Frame")] [SerializeField] [OnValueChanged("OnBoundsChangedEditorCallback")] private Bounds _bounds;


    //Anchor Methods
    public void AnchorToFrame(UIFrame parent, Alignment alignment = Alignment.None)
    {
        transform.parent = parent.transform;
        _frameAlignment = alignment;
        _anchor = parent;
        SetFrameAlignment();
    }
    protected void SetFrameAlignment()
    {
        if (Anchor && FrameAlignment != Alignment.None)
        {
            transform.localPosition = Align.ToBounds(Bounds, _useSafeZone? Anchor.Bounds.ToSafeZone() : Anchor.Bounds, FrameAlignment).ToInt();
        }
    }
    public void SizeToAnchor()
    {
        if (Anchor)
        {
            Bounds = new Bounds(Bounds.center, _useSafeZone? Anchor.Bounds.ToSafeZone().size : Anchor.Bounds.size);
            Debug.Log(Bounds);
            Debug.Log(Anchor.Bounds.ToSafeZone().size);
            Debug.Log(Anchor.Bounds.size);
        }
    }
    //Callbacks
    public virtual void OnSizeChanged()
    {

    }
    public virtual void OnPositionChanged()
    {

    }

    //Editor Methods
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, _bounds.size);
    }
    public void OnParentChangedEditorCallback()
    {
        if (Anchor)
        {
            AnchorToFrame(Anchor, FrameAlignment);
        }
        else
        {
            transform.parent = null;
        }           
    }
    public bool HasParent => Anchor;
    public void OnAlignmentChangedEditorCallback()
    {
        SetFrameAlignment();
    }
    public void OnSizeToAnchorChangedEditorCallback() => SizeToAnchor();

    public void OnSafeZoneChangedEditorCallback()
    {
        SetFrameAlignment();
        if (_sizeToAnchor)
        {
            SizeToAnchor();
            
        }
    }
    public void OnBoundsChangedEditorCallback()
    {
        Bounds = new Bounds(Bounds.center.ToInt(), Bounds.size.ToInt());
        SetFrameAlignment();
        if (_sizeToAnchor)
        {
            SizeToAnchor();
        }
        OnSizeChanged();
    }
}
