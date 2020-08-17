using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using PxMath;

[RequireComponent(typeof(SpriteRenderer))]
public class UICanvas : MonoBehaviour
{
    public Bounds CanvasBounds
    {
        get
        {
            var zoom = Camera2D.zoomLevel;
            var dimensions = new Vector2(Screen.width / zoom, Screen.height / zoom);
            var center = camera2D.transform.position;
            var bounds = new Bounds(center, dimensions);
            return bounds;
        }
    }

    public Bounds SafeZoneBounds
    {
        get
        {
            var zoom = Camera2D.zoomLevel;
            var dimensions = new Vector2(Screen.width / zoom, Screen.height / zoom);
            var center = camera2D.transform.position;
            dimensions.x = dimensions.x * .9f;
            dimensions.y = dimensions.y *= .9f;
            var bounds = new Bounds(center, dimensions);
            return bounds;
        }
    }

    public bool drawPanels;

    UIPanel canvasPanel;
    UIPanel safeZonePanel;

    Camera2D camera2D;
    public static UICanvas instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            camera2D = Camera2D.instance;            
        }
        else
        {
            Debug.Log("More than one UICanvas");
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        transform.SetParent(camera2D.transform);
        //transform.position = camera2D.transform.position;
        transform.position = Vector2.zero;

        canvasPanel = UIPanel.Create
        (
            Scratchpad.instance.panelSprite,
            CanvasBounds,
            "CanvasPanel",
            transform,
            Alignment.Center,
            Stretch.FullScreen
        );        
        //canvasPanel._spriteRenderer.sortingOrder = canvasPanel.transform.parent.hierarchyCount;
        safeZonePanel = UIPanel.Create
        (
            Scratchpad.instance.pinkSquare,
            CanvasBounds,
            "SafeZonePanel",
            canvasPanel.transform,
            Alignment.Center,
            Stretch.FullScreen,
            true
        );
        safeZonePanel.GetComponent<SpriteRenderer>().sortingOrder = safeZonePanel.transform.parent.hierarchyCount;

        //canvasPanel._spriteRenderer.enabled = drawPanels;
        //safeZonePanel._spriteRenderer.enabled = drawPanels;
    }
}
