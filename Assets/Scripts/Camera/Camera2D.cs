using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour, IBounds
{
    Vector3 debugCameraPosition;
    public readonly static int zoomLevel = 5;
    new public Camera camera;

    public static Camera2D instance;

    public delegate void OnCameraMoveEvent(Vector2 cameraPosition);
    public event OnCameraMoveEvent OnCameraMove;

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
        camera = GetComponent<Camera>();

    }

    private void Start()
    {
        SetOrthographicSize();
        debugCameraPosition = transform.position;
    }

    private void Update()
    {
        SetOrthographicSize();

        //Debug Stuff
        if(debugCameraPosition != transform.position)
        {
            MoveCamera(transform.position);
            debugCameraPosition = transform.position;
        }
    }        

    private void SetOrthographicSize()
    {
        camera.orthographicSize = (Screen.height / 2) / zoomLevel;
    }

    public void MoveCamera(Vector2 newPos)
    {
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        OnCameraMove?.Invoke(transform.position);
    }
    public Bounds Bounds
    {
        get
        {
            var zoom = zoomLevel;
            var dimensions = new Vector2(Screen.width / zoom, Screen.height / zoom);
            var center = transform.position;
            var bounds = new Bounds(center, dimensions);
            return bounds;
        }
    }
}


