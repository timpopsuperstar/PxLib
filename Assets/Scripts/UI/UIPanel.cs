using PxMath;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Schema;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]

public class UIPanel : MonoBehaviour, IBounds
{
    SpriteRenderer _spriteRenderer;

    public Bounds Bounds
    {
        get
        {
            return new Bounds(transform.position, _spriteRenderer.size);
        }
    }

    public Bounds SafeZone
    {
        get
        {
            return new Bounds(transform.position, _spriteRenderer.size * .9f);
        }
    }

    [SerializeField] Alignment _alignment;
    public Alignment Alignment
    {
        get { return _alignment; }
        set 
        { 
            _alignment = value;
            Draw();
        }
    }

    [SerializeField] Stretch _stretch;
    public Stretch Stretch
    {
        get { return _stretch; }
        set 
        { 
            _stretch = value;
            Draw();
        }
    }

    private bool _sizeToSafeZone;
    public bool SizeToSafeZone
    {
        get { return _sizeToSafeZone; }
        set
        {
            _sizeToSafeZone = value;
            Draw();
        }
    }

    public Sprite Sprite
    {
        get { return _spriteRenderer.sprite; }
        set 
        { 
            _spriteRenderer.sprite = value;
            Draw();
        }
    }

    public static UIPanel Create(Sprite sprite, Bounds bounds, string name = "UIPanel", Transform parent = null, Alignment alignment = Alignment.None, Stretch stretch = Stretch.None, bool sizeToSafeZone = false)
    {
        var panel = new GameObject("UIPanel", typeof(UIPanel)).GetComponent<UIPanel>();

        panel.name = name;
        panel.transform.position = bounds.center;
        panel._alignment = alignment;
        panel.Stretch = stretch;
        panel.Sprite = sprite;
        panel._spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        panel._spriteRenderer.size = bounds.size;
        panel.transform.SetParent(parent);
        if (parent == null)
        {
            panel.transform.SetParent(UIManager.instance.Canvas.transform);
        }
        panel._spriteRenderer.sortingOrder = panel.transform.hierarchyCount;
        panel._spriteRenderer.sortingLayerName = "UI";
        panel._sizeToSafeZone = sizeToSafeZone;
        return panel;
    }

    //MonoBehaviour Methods
    private void Awake()
    {        
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Draw()
    {
        Scale();
        Align();
    }

    private void Scale()
    {
        
    }

    private void Align()
    {

    }

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
    //        case Stretch.Horizontal:
    //            _spriteRenderer.size = new Vector2(panelSize.x, Bounds.size.y);
    //            transform.position = new Vector2(0, transform.position.y);
    //            break;
    //        case Stretch.Vertical:
    //            _spriteRenderer.size = new Vector2(Bounds.size.x, panelSize.y);
    //            transform.position = new Vector2(transform.position.x, 0);
    //            break;
    //        case Stretch.FullScreen:
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
