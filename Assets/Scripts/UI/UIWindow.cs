using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PxAlignment;
using PxMath;
using NaughtyAttributes;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class UIWindow : UIFrame
{
    public bool Draw
    {
        get => _draw;
        set
        {
            _draw = value;
            _spriteRenderer.enabled = value;
        }
    }
    public Sprite Sprite
    {
        get => _spriteRenderer.sprite;
        set
        {
            _spriteRenderer.sprite = value;
        }
    }

    [BoxGroup("Window")] [SerializeField] [OnValueChanged("OnDrawChangedCallback")] private bool _draw;
    private SpriteRenderer _spriteRenderer;

    //Monobehaviour Methods
    protected virtual void Awake()
    {
        InitSpriteRenderer();
    }

    private void InitSpriteRenderer()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.SetUIWindowProperties();
        _spriteRenderer.enabled = _draw;
        OnSpriteRendererInit();
    }
    //Callbacks
    protected override void OnSizeChanged()
    {
        _spriteRenderer.size = Size;
    }
    protected override void OnRenderBounds()
    {
        _spriteRenderer.size = Size;
    }
    protected virtual void OnSpriteRendererInit() { }

    //Editor Methods
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(Bounds.center, Bounds.size);
    }
    public void OnDrawChangedCallback()
    {
        _spriteRenderer.enabled = _draw;
        if (_draw)
        {
            InitSpriteRenderer();
        }
    }
}
