using PxAlignment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainFrame : UIFrame
{
    public static UIMainFrame instance;
    public override Bounds Bounds
    {
        get => _camera.PixelBounds;
    }
    public override Vector2 Size
    {
        get => _camera.PixelBounds.size;
    }
    public override Vector2 Position
    {
        get => Bounds.center;
    }
    private Camera2D _camera;
    private UIManager _uiManager;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static UIMainFrame Instantiate()
    {
        var mainCanvas = new GameObject("UIMainFrame", typeof(UIMainFrame)).GetComponent<UIMainFrame>();
        mainCanvas.InitMainFrame();
        return mainCanvas;
    }

    private void InitMainFrame()
    {
        _camera = Camera2D.instance;
        _uiManager = UIManager.instance;
        _camera.OnCameraMove += OnCameraMove;
    }
    private void OnCameraMove(Vector2 cameraPos)
    {
        Bounds = _camera.PixelBounds;
    }
}
