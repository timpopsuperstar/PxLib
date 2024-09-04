using PxMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemGridObject : GridObject
{
    public static Dimensions cellSize = new Dimensions(24, 24);

    [SerializeField] private Item item;
    public Item Item
    {
        get
        {
            return item;
        }
        set
        {
            item = value;
            spriteRenderer.sprite = item.sprite;
        }
    }
    
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ReleaseItem()
    {
        item = null;
        spriteRenderer.sprite = null;
    }
}
