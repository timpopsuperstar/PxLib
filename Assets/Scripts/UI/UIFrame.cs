using PxAlignment;
using PxMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine.UI;

public class UIFrame : MonoBehaviour
{
    //Public Properties
    public virtual Bounds Bounds
    {
        get => new Bounds(transform.localPosition, _size);
        set
        {
            _previousPosition = Bounds.center;
            _previousSize = Bounds.size;
            transform.localPosition = value.center.ToInt();
            
            _size = value.size.ToInt();
            if(_previousPosition != (Vector2)Bounds.center)
            {
                OnPositionChanged();
            }
            if(_previousSize != (Vector2)Bounds.size)
            {
                OnSizeChanged();
            } 
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
                UpdateAlignment();
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
            SetAnchor(value, FrameAlignment);
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

    //Editor Fields
    [BoxGroup("Frame")] [SerializeField] [OnValueChanged("OnBoundsPropertyChangedCallback")] protected Vector2 _size;
    [BoxGroup("Frame")] [SerializeField] [OnValueChanged("OnParentChangedCallback")] protected UIFrame _anchor;
    [BoxGroup("Frame")] [SerializeField] [OnValueChanged("OnBoundsPropertyChangedCallback")] [ShowIf("HasParent")] private Alignment _frameAlignment;
    [BoxGroup("Frame")] [SerializeField] [OnValueChanged("OnBoundsPropertyChangedCallback")] [ShowIf("HasParent")] private bool _useSafeZone;
    [BoxGroup("Frame")] [SerializeField] [OnValueChanged("OnBoundsPropertyChangedCallback")] [ShowIf("HasParent")] protected bool _sizeToAnchor;

    //Private Fields
    private List<UIFrame> _childrenFrames;
    private Vector2 _previousPosition;
    private Vector2 _previousSize;

    //Public Methods
    public void SetAnchor(UIFrame parent, Alignment alignment = Alignment.None)
    {
        if (_anchor)
        {
            _anchor.RemoveChild(this);
        }
        transform.SetParent(parent.transform);
        //transform.parent = parent.transform;
        _frameAlignment = alignment;
        _anchor = parent;        
        parent.AddChild(this);
        RenderBounds();
    }
    public void ReleaseAnchor()
    {
        if (_anchor)
        {
            _anchor.RemoveChild(this);
            transform.SetParent(null);
            _anchor = null;
            RenderBounds();
        }
    }   
    //Callbacks
    protected virtual void OnSizeChanged() { }
    protected virtual void OnPositionChanged() { }
    protected virtual void OnRenderBounds() { }

    //Private Methods
    private void RenderBounds()
    {
        UpdateAlignment();
        UpdateSize();
        OnRenderBounds();
    }
    private void UpdateAlignment()
    {
        if (Anchor && FrameAlignment != Alignment.None)
        {
            var newPos = Align.ToBounds(Bounds, _useSafeZone ? Anchor.Bounds.ToSafeZone() : Anchor.Bounds, FrameAlignment).ToInt();
            Bounds = new Bounds(newPos, Bounds.size);
        }
    }
    private void UpdateSize()
    {
        if (Anchor && _sizeToAnchor)
        {
            Bounds = new Bounds(Bounds.center, _useSafeZone ? Anchor.Bounds.ToSafeZone().size : Anchor.Bounds.size);
        }
    }
    private void AddChild(UIFrame child)
    {
        _childrenFrames.Add(child);
    }
    private void RemoveChild(UIFrame child)
    {
        _childrenFrames.Remove(child);
        child.RenderBounds();
    }
    private void RemoveChildren()
    {
        var childrenToRemove = _childrenFrames;
        for(int i = 0; i < childrenToRemove.Count; i++)
        {
            RemoveChild(childrenToRemove[i]);
        }
    }

    //Editor Methods
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, Bounds.size);
    }
    protected void OnParentChangedCallback()
    {
        if (Anchor)
        {
            SetAnchor(Anchor, FrameAlignment);
        }
        else
        {
            transform.parent = null;
        }           
    }
    private bool HasParent => Anchor;
    protected void OnBoundsPropertyChangedCallback()
    {
        _size = _size.ToInt();
        RenderBounds();
    }
}
