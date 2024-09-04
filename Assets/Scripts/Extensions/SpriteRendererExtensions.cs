using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteRendererExtensions
{
    public static void SetUIWindowProperties(this SpriteRenderer spriteRenderer)
    {
        //spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        spriteRenderer.sortingLayerName = "UI";
        spriteRenderer.sortingOrder = spriteRenderer.transform.hierarchyCount;
    }
}
