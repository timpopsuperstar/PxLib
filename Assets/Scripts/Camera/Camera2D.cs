using PxMath;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Schema;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

//Pixel Perfect Camera to be extended
public class Camera2D : MonoBehaviour
{
    public delegate void OnCameraMoveAction(Vector2 v);
    public event OnCameraMoveAction OnCameraMove;

    public Camera Camera { get; private set; }
    public Bounds WindowBounds
    {
        get => new Bounds(transform.position, new Vector2((int)Screen.width, (int)Screen.height));        
    }
    public Bounds PixelBounds
    {
        get
        {
            float halfHeight = Camera.orthographicSize;
            float halfWidth = Camera.aspect * halfHeight;
            var height = halfHeight * 2;
            var width = halfWidth * 2;
            return new Bounds((Vector2)transform.position, new Vector2(width, height));
        }
    }

    public static Camera2D instance;

    private Vector2 _previousPosition;
    private Vector2 _currentPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        InitCamera();
    }
    private void Update()
    {
        _currentPosition = transform.position;
        if(_currentPosition != _previousPosition)
        {
            OnCameraMove?.Invoke(_currentPosition);
        }
        _previousPosition = _currentPosition;
    }    
    private void InitCamera()
    {
        Camera = GetComponent<Camera>();
        SetOrthographicSize();
    }
    private void SetOrthographicSize()
    {
        Camera.orthographicSize = Screen.height / 2 / GameSettings.zoomLevel;
    }

}


