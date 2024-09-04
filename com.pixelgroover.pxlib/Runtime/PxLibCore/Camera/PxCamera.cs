using UnityEngine;
using System.Collections;

//Pixel Perfect Camera to be extended
public class PxCamera : MonoBehaviour
{
    public const float SHAKE_FRAMERATE = .03f;
    public static float PixelPerfectOrthographicSize => Screen.height / 2 / ZoomLevel;

    public static int ZoomLevel => 4;
    public event System.Action<Vector2> OnCameraMove;
    public Camera Camera
    {
        get => _camera ? _camera : (_camera = GetComponent<Camera>());
    }
    private Camera _camera;
    public Vector3 Position
    {
        get => transform.position;
        protected set 
        {
            if(value != Position)
            {
                PreviousPosition = Position;
                transform.position = value;
                OnCameraMove?.Invoke(Position);
            }            
        }
    }
    public Vector2 PreviousPosition { get; private set; }
    public Bounds WindowBounds => new Bounds(transform.position, new Vector2((int)Screen.width, (int)Screen.height));
    public Bounds PixelBounds
    {
        get
        {
            float halfHeight = Camera.orthographicSize;
            float halfWidth = Camera.aspect * halfHeight;
            float height = halfHeight * 2;
            float width = halfWidth * 2;
            return new Bounds((Vector2)transform.position, new Vector2(width, height));
        }
    }
    public float OrthographicSize 
    {
        get => Camera.orthographicSize;
        set => Camera.orthographicSize = value;
    }
    public void Awake()
    {
        Init();
    }
    public void Init()
    {
        transform.position = transform.position.ReplaceZ(-10);
    }

    public void Shake(float intensity, float time, Transform t = null)
    {
        StartCoroutine(IEShake(intensity, time, t != null ? t : transform));

    }
    private IEnumerator IEShake(float intensity, float time, Transform t)
    {
        var shakecount = (int)(time / SHAKE_FRAMERATE >= 1 ? time / SHAKE_FRAMERATE : 1);
        //var origPos = t != null ? t.position : transform.position;
        //var origPos = new Vector3(0, 0, t.position.z);
        var origPos = t.position;
        for (int i = 0; i < shakecount; i++)
        {
            var intensityMod = 1f - ((float)i / shakecount);
            var xOffset = intensity * intensityMod * Random.Range(-1f, 1f);
            var yOffset = intensity * intensityMod * Random.Range(-1f, 1f);
            //var xOffset = intensity * intensityMod * Random.Range(-1, 1);
            //var yOffset = intensity * intensityMod * Random.Range(-1, 1);
            var shakePos = new Vector3(origPos.x + xOffset, origPos.y + yOffset, t.position.z);
            t.position = shakePos;
            yield return new WaitForSeconds(SHAKE_FRAMERATE);
        }
        t.position = origPos;
    }
}


