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
            _sprite = value;
        }
    }
    [BoxGroup("Window")] [SerializeField] [OnValueChanged("OnDrawChangedCallback")] private bool _draw;
    private Sprite _sprite;
    private SpriteRenderer _spriteRenderer;

    //Draw Methods
    private void InitSpriteRenderer()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite = _spriteRenderer.sprite;
        _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        _spriteRenderer.sortingLayerName = "UI";
        _spriteRenderer.sortingOrder = transform.hierarchyCount;
        _spriteRenderer.enabled = _draw;
        OnSpriteRendererInit();
    }

    //Monobehaviour Methods
    public virtual void Awake()
    {
        InitSpriteRenderer();
    }
    public virtual void Update()
    {
    }
    public void OnDestroy()
    {
    }
    //Callbacks
    public override void OnSizeChanged()
    {
        _spriteRenderer.size = Size;
    }
    protected virtual void OnSpriteRendererInit()
    {

    }
    //Editor Methods
    public void OnDrawChangedCallback()
    {
        _spriteRenderer.enabled = _draw;
        if (_draw)
        {
            InitSpriteRenderer();
        }
    }
}
