using PxMath;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Schema;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public enum Alignment { None, Top, TopRight, CenterRight, BottomRight, Bottom, BottomLeft, CenterLeft, TopLeft, Center };
public enum Stretch { None, Vertical, Horizontal, FullScreen }
public enum FillType { Fit, Fill, Stretch, Tile, Center };

[RequireComponent(typeof(SpriteRenderer))]
public class UIPanel : MonoBehaviour, IBounds
{
    //Private Properties
    [SerializeField] private Alignment _alignment;
    [SerializeField] private Stretch _stretch;
    private Vector2 _defaultSize;
    private Vector2 _currentSize;
    private SpriteRenderer _spriteRenderer;
    private bool _alignToSafeZone;
    private IBounds _anchor;
    private Vector3 _previousPosition;

    //IBounds
    public Transform Transform { get { return transform; } }
    public Bounds Bounds { get { return new Bounds(transform.position, _currentSize); }}
    public Bounds SafeZone { get { return new Bounds(Bounds.center, Bounds.size * .9f); } }

    //Events
    public delegate void OnMoveEvent(Vector2 position);
    public event OnMoveEvent OnMove;
    public delegate void OnSizeChangeEvent(Vector2 size);
    public event OnSizeChangeEvent OnSizeChange;

    public Alignment Alignment
    {
        get { return _alignment; }
        set { _alignment = value; }
    }

    public Stretch Stretch
    {
        get { return _stretch; }
        set { _stretch = value;}
    }

    public bool AlignToSafeZone
    {
        get { return _alignToSafeZone; }
        set { _alignToSafeZone = value; }
    }

    public Sprite Sprite
    {
        get { return _spriteRenderer.sprite; }
        set { _spriteRenderer.sprite = value; }
    }
    public IBounds Anchor
    {
        get { return _anchor; }
        set { _anchor = value; }
    }
    public Vector2 Size
    {
        get { return _currentSize; }
        set 
        { 
            _currentSize = value; 
            _spriteRenderer.size = value;
            OnSizeChange?.Invoke(_currentSize);
        }
    }
    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
        set
        {
            _previousPosition = transform.position;
            transform.position = value;
            OnMove?.Invoke(transform.position);
        }
    }

    //constructor
    public static UIPanel Instantiate(Sprite sprite, IBounds anchor, Vector2 defaultSize, string name = "UIPanel", Alignment alignment = Alignment.None, Stretch stretch = Stretch.None, bool alignToSafeZone = false)
    {
        var panel = new GameObject("UIPanel", typeof(UIPanel)).GetComponent<UIPanel>();
        panel.Sprite = sprite;
        panel.name = name;

        panel.transform.position = anchor.Bounds.center;
        panel._alignment = alignment;
        panel._stretch = stretch;
        panel._alignToSafeZone = alignToSafeZone;
        panel._anchor = anchor;        
        panel.transform.SetParent(anchor.Transform);
        panel._spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        panel._spriteRenderer.size = defaultSize;
        panel._defaultSize = defaultSize;
        panel._spriteRenderer.sortingOrder = panel.transform.hierarchyCount;
        panel._spriteRenderer.sortingLayerName = "UI";


        panel.Draw();
        return panel;
    }

    //MonoBehaviour Methods
    private void Awake()
    {        
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void OnEnable()
    {
        
        OnMove += OnAnchorMove;
    }

    void OnDisable()
    {
        OnMove -= OnAnchorMove;
    }
    //Private Methods
    private void Draw()
    {
        ScaleToAnchor();
        AlignToAnchor();
    }

    private void ScaleToAnchor()
    {
        var anchorBounds = Anchor.Bounds;

        if (AlignToSafeZone)
        {
            anchorBounds = Anchor.SafeZone;
        }
        
        switch (_stretch)
        {
            case Stretch.Horizontal:
                _spriteRenderer.size = new Vector2(anchorBounds.size.x, Bounds.size.y);
                transform.position = new Vector2(anchorBounds.center.x, transform.position.y);
                break;
            case Stretch.Vertical:
                _spriteRenderer.size = new Vector2(Bounds.size.x, anchorBounds.size.y);
                transform.position = new Vector2(transform.position.x, anchorBounds.center.y);
                break;
            case Stretch.FullScreen:
                _spriteRenderer.size = anchorBounds.size;
                transform.position = anchorBounds.center;
                break;
            case Stretch.None:
                _spriteRenderer.size = _defaultSize;
                transform.position = transform.position;
                break;

        }

    }

    private void AlignToAnchor()
    {

    }

    private void OnAnchorMove(Vector2 anchorPosition)
    {

    }

    ////Public Methods
    //public static UIPanel InstantiatePanel(Bounds defaultSize)
    //{
    //    var panel = new GameObject("UIPanel", typeof(UIPanel)).GetComponent<UIPanel>();

    //    panel.name = name;

    //    panel.transform.position = bounds.center;
    //    panel._alignment = alignment;
    //    panel.Stretch = stretch;
    //    panel.Sprite = sprite;
    //    panel._spriteRenderer.drawMode = SpriteDrawMode.Sliced;
    //    panel._spriteRenderer.size = bounds.size;
    //    panel.transform.SetParent(parent);
    //    if (parent == null)
    //    {
    //        panel.transform.SetParent(UIManager.instance.Canvas.transform);
    //    }
    //    panel._spriteRenderer.sortingOrder = panel.transform.hierarchyCount;
    //    panel._spriteRenderer.sortingLayerName = "UI";
    //    panel._alignToSafeZone = sizeToSafeZone;

    //    return panel;
    //}

    //private void ScaleToParent()
    //{
    //    //Update method to refer to parent's size, require parent to have a size property.

    //    var zoom = Camera2D.zoomLevel;

    //    Vector2 panelSize;
    //    if (_cropToSafeZone)
    //    {
    //        panelSize = UICanvas.instance.SafeZoneBounds.size;
    //    }
    //    else
    //    {
    //        panelSize = UICanvas.instance.CanvasBounds.size;
    //    }

    //    switch (_stretch)
    //    {
    //        case global::Stretch.Horizontal:
    //            _spriteRenderer.size = new Vector2(panelSize.x, Bounds.size.y);
    //            transform.position = new Vector2(0, transform.position.y);
    //            break;
    //        case global::Stretch.Vertical:
    //            _spriteRenderer.size = new Vector2(Bounds.size.x, panelSize.y);
    //            transform.position = new Vector2(transform.position.x, 0);
    //            break;
    //        case global::Stretch.FullScreen:
    //            _spriteRenderer.size = panelSize;
    //            transform.position = Vector2.zero;
    //            break;
    //    }
    //}

    //private void AlignToParent()
    //{
    //    if (_alignment == Alignment.None) return;

    //    //FIX THIS to work with safe zone
    //    Vector2 panelExtents;

    //    if (_cropToSafeZone)
    //    {
    //        panelExtents = UICanvas.instance.SafeZoneBounds.extents;
    //    }
    //    else
    //    {
    //        panelExtents = UICanvas.instance.CanvasBounds.extents;      
    //    }    

    //    var zoom = Camera2D.zoomLevel;
    //    Vector2 newPos = Vector2.zero;
    //    switch (_alignment)
    //    {
    //        case Alignment.Bottom:
    //            newPos.x = 0;
    //            newPos.y = (-Screen.height / zoom) / 2 + (_spriteRenderer.size.y / 2);
    //            break;
    //        case Alignment.BottomLeft:
    //            newPos.x = (-Screen.width / zoom) / 2 + (_spriteRenderer.size.x / 2);
    //            newPos.y = (-Screen.height / zoom) / 2 + (_spriteRenderer.size.y / 2);
    //            break;
    //        case Alignment.BottomRight:
    //            newPos.x = (Screen.width / zoom) / 2 - (_spriteRenderer.size.x / 2);
    //            newPos.y = (-Screen.height / zoom) / 2 + (_spriteRenderer.size.y / 2);
    //            break;
    //        case Alignment.Top:
    //            newPos.x = 0;
    //            newPos.y = (Screen.height / zoom) / 2 - (_spriteRenderer.size.y / 2);
    //            break;
    //        case Alignment.TopLeft:
    //            newPos.x = (-Screen.width / zoom) / 2 + (_spriteRenderer.size.x / 2);
    //            newPos.y = (Screen.height / zoom) / 2 - (_spriteRenderer.size.y / 2);
    //            break;
    //        case Alignment.TopRight:
    //            newPos.x = (Screen.width / zoom) / 2 - (_spriteRenderer.size.x / 2);
    //            newPos.y = (Screen.height / zoom) / 2 - (_spriteRenderer.size.y / 2);
    //            break;
    //        case Alignment.Center:
    //            newPos = Vector2.zero;
    //            break;
    //        case Alignment.CenterLeft:
    //            newPos.x = (-Screen.width / zoom) / 2 + (_spriteRenderer.size.x / 2);
    //            newPos.y = 0;
    //            break;
    //        case Alignment.CenterRight:
    //            newPos.x = (Screen.width / zoom) / 2 - (_spriteRenderer.size.x / 2);
    //            newPos.y = 0;
    //            break;
    //    }        
    //    this.transform.position = newPos;
    //}

    //public void AlignToCanvas()
    //{

    //    if (_alignment == Alignment.None) return;

    //    Vector2 panelExtents;

    //    if (_cropToSafeZone)
    //    {
    //        panelExtents = UICanvas.instance.SafeZoneBounds.extents;
    //    }
    //    else
    //    {
    //        panelExtents = UICanvas.instance.CanvasBounds.extents;
    //    }

    //    var zoom = Camera2D.zoomLevel;
    //    Vector2 newPos = Vector2.zero;

    //    switch (_alignment)
    //    {
    //        case Alignment.Bottom:
    //            newPos.x = 0;
    //            newPos.y = (-Screen.height / zoom) / 2 + (_spriteRenderer.size.y / 2);
    //            break;
    //        case Alignment.BottomLeft:
    //            newPos.x = (-Screen.width / zoom) / 2 + (_spriteRenderer.size.x / 2);
    //            newPos.y = (-Screen.height / zoom) / 2 + (_spriteRenderer.size.y / 2);
    //            break;
    //        case Alignment.BottomRight:
    //            newPos.x = (Screen.width / zoom) / 2 - (_spriteRenderer.size.x / 2);
    //            newPos.y = (-Screen.height / zoom) / 2 + (_spriteRenderer.size.y / 2);
    //            break;
    //        case Alignment.Top:
    //            newPos.x = 0;
    //            newPos.y = (Screen.height / zoom) / 2 - (_spriteRenderer.size.y / 2);
    //            break;
    //        case Alignment.TopLeft:
    //            newPos.x = (-Screen.width / zoom) / 2 + (_spriteRenderer.size.x / 2);
    //            newPos.y = (Screen.height / zoom) / 2 - (_spriteRenderer.size.y / 2);
    //            break;
    //        case Alignment.TopRight:
    //            newPos.x = (Screen.width / zoom) / 2 - (_spriteRenderer.size.x / 2);
    //            newPos.y = (Screen.height / zoom) / 2 - (_spriteRenderer.size.y / 2);
    //            break;
    //        case Alignment.Center:
    //            newPos = Vector2.zero;
    //            break;
    //        case Alignment.CenterLeft:
    //            newPos.x = (-Screen.width / zoom) / 2 + (_spriteRenderer.size.x / 2);
    //            newPos.y = 0;
    //            break;
    //        case Alignment.CenterRight:
    //            newPos.x = (Screen.width / zoom) / 2 - (_spriteRenderer.size.x / 2);
    //            newPos.y = 0;
    //            break;
    //    }
    //    this.transform.position = newPos;
    //}
}
