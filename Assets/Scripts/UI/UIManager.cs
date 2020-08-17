using PxMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Alignment { None, Top, TopRight, CenterRight, BottomRight, Bottom, BottomLeft, CenterLeft, TopLeft, Center};
public enum Stretch { None, Vertical, Horizontal, FullScreen}
//Enums
public enum FillType { Fit, Fill, Stretch, Tile, Center };
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public UIPanel Canvas
    {
        get { return _canvas; }
    }

    UIPanel _canvas;
    Camera2D _camera;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _camera = Camera2D.instance;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("More than one instance of UIManager");
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {      
        _canvas = UIPanel.Create(Scratchpad.instance.pinkSquare, _camera.Bounds, "Canvas", _camera.transform, Alignment.Center,
            Stretch.FullScreen);
    }


}
