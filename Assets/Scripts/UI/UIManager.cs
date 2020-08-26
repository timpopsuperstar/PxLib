using PxMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enums

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
        _canvas = UIPanel.Instantiate(Scratchpad.instance.pinkSquare, _camera, Vector2.zero, "Canvas", Alignment.None, Stretch.FullScreen);
    }

}
