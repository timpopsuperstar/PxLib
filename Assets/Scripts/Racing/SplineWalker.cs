using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    public enum SplineWalkerMode
    {
        Once,
        Loop,
        PingPong
    }
    public SplineWalkerMode mode;
    public BezierSpline spline;
    public float duration;
    public bool lookForward;

    private bool _goingForward = true;
    private float _progress;

    private void Update()
    {
        if (_goingForward)
        {
            _progress += Time.deltaTime / duration;
            if (_progress > 1f)
            {
                switch (mode)
                {
                    case SplineWalkerMode.Once:
                        _progress = 1f;
                        break;
                    case SplineWalkerMode.Loop:
                        _progress -= 1f;
                        break;
                    case SplineWalkerMode.PingPong:
                        _progress = 2f - _progress;
                        _goingForward = false;
                        break;
                }
            }
        }
        else
        {
            _progress -= Time.deltaTime / duration;
            if (_progress < 0f)
            {
                _progress = -_progress;
                _goingForward = true;
            }
        }

        Vector3 position = spline.GetPoint(_progress);
        transform.localPosition = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(_progress));
        }
    }
}
