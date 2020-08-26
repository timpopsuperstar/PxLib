using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour, IBounds
{
    //IBounds
    public Transform Transform { get { return transform; } }

    //Events
    public delegate void OnMoveEvent(Vector2 position);
    public event OnMoveEvent OnMove;
    public delegate void OnSizeChangeEvent(Vector2 size);
    public event OnSizeChangeEvent OnResize;

    public Bounds Bounds
    {
        get
        {
            var dimensions = new Vector2(Screen.width / ZoomLevel, Screen.height / ZoomLevel);
            var center = transform.position;
            var bounds = new Bounds(center, dimensions);
            return bounds;
        }
    }

    public Bounds SafeZone
    {
        get
        {
            return new Bounds(Bounds.center, Bounds.size * .9f);
        }
    }
    //Public Properties
    public static int ZoomLevel { get { return 5; } }

    private Vector3 _previousPosition;
    public Vector3 PreviousPosition { get { return _previousPosition; }set { _previousPosition = value; } }

    public Vector3 Position 
    { 
        get 
        { 
            return transform.position; 
        }
        set
        {
            _previousPosition = transform.position;
            transform.position = value;
            OnMove?.Invoke(transform.position);
        }
    }

    private Camera _camera;
    public Camera Camera { get { return _camera; } }
    
    //Singleton
    public static Camera2D instance;

    private void Awake()
    {

        #region singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        #endregion
        _camera = GetComponent<Camera>();

    }

    private void Start()
    {
        SetOrthographicSize();
    }

    private void Update()
    {
        SetOrthographicSize();
    }        

    private void SetOrthographicSize()
    {
        Camera.orthographicSize = (Screen.height / 2) / ZoomLevel;
    }

    public void MoveCamera(Vector2 newPos)
    {
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}


