using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(SpriteRenderer))]
public class UICanvas : MonoBehaviour
{
    [Header("Graphic Properties")]
    public bool drawBackground;
    [ShowIf("ShowBackgroundProps")] [SerializeField] Sprite canvasSprite;   
    [ShowIf("ShowBackgroundProps")] [SerializeField] FillType fillType;

    public bool drawSafeZone;
    [ShowIf("ShowSafeZoneProps")] [SerializeField] Sprite safeZoneSprite;

    //Private Properties
    SpriteRenderer spriteRenderer;
    Camera2D camera2D;
    SpriteRenderer safeZoneRenderer;


    //Editor Functions
    private bool ShowBackgroundProps { get { return drawBackground; } }
    public bool ShowSafeZoneProps { get { return drawSafeZone; } }


    //MonoBehaviour Methods
    private void Awake()
    {
        camera2D = Camera2D.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        DrawCanvas();
        DrawSafeZone();
    }

    //Private Method
    private void DrawCanvas()
    {
        var zoom = camera2D.zoomLevel;
        spriteRenderer.sprite = canvasSprite;
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        spriteRenderer.size = new Vector2(Screen.width / zoom, Screen.height / zoom);
        transform.position = (Vector2)camera2D.transform.position;
    }

    private void DrawSafeZone()
    {
        if(safeZoneRenderer == null)
        {
            safeZoneRenderer = new GameObject("SafeZone", typeof(SpriteRenderer)).GetComponent<SpriteRenderer>();
            safeZoneRenderer.sprite = safeZoneSprite;
            safeZoneRenderer.drawMode = SpriteDrawMode.Sliced;
            safeZoneRenderer.size = SafeZone.size;
            safeZoneRenderer.sortingLayerName = "UI";            
            safeZoneRenderer.name = "SafeZone";
        }
        safeZoneRenderer.size = SafeZone.size;
        safeZoneRenderer.transform.position = (Vector2)SafeZone.center;
    }


    //Public Properties
    public Bounds SafeZone
    {
        get
        {
            var zoom = camera2D.zoomLevel;
            var dimensions = new Vector2(Screen.width / zoom, Screen.height / zoom);
            var center = camera2D.transform.position;
            dimensions.x = Mathf.RoundToInt(dimensions.x * .9f);
            dimensions.y = Mathf.RoundToInt(dimensions.y *= .9f);              
            var bounds = new Bounds(center, dimensions);
            bounds.center = center;
            return bounds;
        }            
    }
    //Public Methods
    //public Bounds OrthographicBounds(this Camera camera)
    //{
    //    var v1 = camera.ViewportToWorldPoint(Vector3.zero);
    //    var v2 = camera.ViewportToWorldPoint(Vector3.one);
    //    return new Bounds((v1 + v2) / 2f, (v2 - v1));
    //}
}
