using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
    public int zoomLevel;
    new public Camera camera;

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
        camera = GetComponent<Camera>();
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
        if (zoomLevel <= 0)
        {
            Debug.LogWarning("zoomLevel not set");
            zoomLevel = 1;
        }
        camera.orthographicSize = (Screen.height / 2) / zoomLevel;
    }
}

public static class CameraExtensions
{
    public static Bounds OrthographicBounds(Camera camera)
    {
        var v1 = camera.ViewportToWorldPoint(Vector3.zero);
        var v2 = camera.ViewportToWorldPoint(Vector3.one);
        return new Bounds((v1 + v2) / 2f, (v2 - v1));
    }
}
