using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditorInternal;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]

public class UIContainer : MonoBehaviour
{
    //[SerializeField] Vector2 size;
    [SerializeField] Sprite sprite;
    [SerializeField] Alignment alignment;
    [SerializeField] Stretch stretch;

    SpriteRenderer spriteRenderer;

    //MonoBehaviour Methods
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();        
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        //Find way to do callback on change
        ScaleOnAxis();
        SetAlignment();
    }

    //Class Methods
    private void Init()
    {
        spriteRenderer.sprite = sprite;
        ScaleOnAxis();
        SetAlignment();
    }

    private void ScaleOnAxis()
    {
        var zoom = Camera2D.instance.zoomLevel;
        switch (stretch)
        {
            case Stretch.None:
                break;
            case Stretch.Horizontal:
                spriteRenderer.size = new Vector2(Screen.width / zoom, spriteRenderer.size.y);
                transform.position = new Vector2(0, transform.position.y);
                break;
            case Stretch.Vertical:
                spriteRenderer.size = new Vector2(spriteRenderer.size.x, Screen.height / zoom);
                transform.position = new Vector2(transform.position.x, 0);
                break;
            case Stretch.FullScreen:
                spriteRenderer.size = new Vector2(Screen.width / zoom, Screen.height / zoom);
                transform.position = Vector2.zero;
                break;
        }
    }

    private void SetAlignment()
    {
        if (alignment == Alignment.None) return;

        var zoom = Camera2D.instance.zoomLevel;
        Vector2 newPos = Vector2.zero;
        switch (alignment)
        {
            case Alignment.Bottom:
                newPos.x = 0;
                newPos.y = (-Screen.height / zoom) / 2 + (spriteRenderer.size.y / 2);
                break;
            case Alignment.BottomLeft:
                newPos.x = (-Screen.width / zoom) / 2 + (spriteRenderer.size.x / 2);
                newPos.y = (-Screen.height / zoom) / 2 + (spriteRenderer.size.y / 2);
                break;
            case Alignment.BottomRight:
                newPos.x = (Screen.width / zoom) / 2 - (spriteRenderer.size.x / 2);
                newPos.y = (-Screen.height / zoom) / 2 + (spriteRenderer.size.y / 2);
                break;
            case Alignment.Top:
                newPos.x = 0;
                newPos.y = (Screen.height / zoom) / 2 - (spriteRenderer.size.y / 2);
                break;
            case Alignment.TopLeft:
                newPos.x = (-Screen.width / zoom) / 2 + (spriteRenderer.size.x / 2);
                newPos.y = (Screen.height / zoom) / 2 - (spriteRenderer.size.y / 2);
                break;
            case Alignment.TopRight:
                newPos.x = (Screen.width / zoom) / 2 - (spriteRenderer.size.x / 2);
                newPos.y = (Screen.height / zoom) / 2 - (spriteRenderer.size.y / 2);
                break;
            case Alignment.Center:
                newPos = Vector2.zero;
                break;
            case Alignment.CenterLeft:
                newPos.x = (-Screen.width / zoom) / 2 + (spriteRenderer.size.x / 2);
                newPos.y = 0;
                break;
            case Alignment.CenterRight:
                newPos.x = (Screen.width / zoom) / 2 - (spriteRenderer.size.x / 2);
                newPos.y = 0;
                break;
        }        
        this.transform.position = newPos;
    }
}
