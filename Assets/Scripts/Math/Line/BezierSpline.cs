using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public enum BezierControlPointMode
{
    Free,
    Aligned,
    Mirrored
}
public class BezierSpline : MonoBehaviour
{
    [SerializeField][HideInInspector] private Vector3[] _points;
    [SerializeField][HideInInspector] private BezierControlPointMode[] _modes;
    [SerializeField][HideInInspector] private bool _loop;

    public bool Loop
    {
        get => _loop;
        set
        {
            _loop = value;
            if(value == true)
            {
                _modes[_modes.Length - 1] = _modes[0];
                SetControlPoint(0, _points[0]);
            }
        }
    }
    public int ControlPointCount
    {
        get => _points.Length;
    }
    public int CurveCount
    {
        get => (_points.Length - 1) / 3;
    }
    public float Distance
    {
        get
        {
            float distance = 0f;
            Vector3 currentPos = GetPoint(0f);
            Vector3 nextPos; 
            for(float t = 0; t <= 1; t += 1 / 100f)
            {
                nextPos = GetPoint(t);
                distance += Vector3.Distance(currentPos, nextPos);
                currentPos = nextPos;
            }
            return distance;
        }
    }
    public Vector3 GetControlPoint (int index)
    {
        return _points[index];
    }
    public void SetControlPoint (int index, Vector3 point)
    {
        if(index % 3 == 0)
        {
            Vector3 delta = point - _points[index];
            if (_loop)
            {
                if(index == 0)
                {
                    _points[1] += delta;
                    _points[_points.Length - 2] += delta;
                    _points[_points.Length - 1] = point;
                }
                else if(index == _points.Length - 1)
                {
                    _points[0] = point;
                    _points[1] += delta;
                    _points[index - 1] += delta;
                }
                else
                {
                    _points[index - 1] += delta;
                    _points[index + 1] += delta;
                }
            }
            else
            {
                if (index > 0)
                {
                    _points[index - 1] += delta;
                }
                if (index + 1 < _points.Length)
                {
                    _points[index + 1] += delta;
                }
            }
        }
        _points[index] = point;
        EnforceMode(index);
    }
    public Vector3 GetPoint(float t)
    {
        int i;
        if(t >= 1f)
        {
            t = 1f;
            i = _points.Length - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }
        return transform.TransformPoint(Bezier.GetPoint(_points[i], _points[i + 1], _points[i + 2], _points[i + 3], t));
    }
    public BezierControlPointMode GetControlPointMode (int index)
    {
        return _modes[(index + 1) / 3];
    }
    public void SetControlPointMode (int index, BezierControlPointMode mode)
    {
        int modeIndex = (index + 1) / 3;
        _modes[modeIndex] = mode;
        if (_loop)
        {
            if(modeIndex == 0)
            {
                _modes[_modes.Length - 1] = mode;
            }
            else if(modeIndex == _modes.Length - 1)
            {
                _modes[0] = mode;
            }
        }
        EnforceMode(index);
    }
    public Vector3 GetVelocity(float t)
    {
        int i;
        if(t >= 1f)
        {
            t = 1f;
            i = _points.Length - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }
        return transform.TransformPoint(Bezier.GetFirstDerivative(_points[i], _points[i + 1], _points[i + 2], _points[i + 3], t)) - transform.position;
    }
    public Vector3 GetDirection(float t)
    {
        return GetVelocity(t).normalized;
    }
    public float GetNextPositionByDistance(float d, float t, bool loop = false)
    {
        float distanceTraveled = 0f;
        Vector3 currentPos = GetPoint(t);
        Vector3 nextPos;
        while (distanceTraveled < d)
        {
            t += .0001f;
            if(loop && t > 1f)
            {
                t -= 1f;
            }
            nextPos = GetPoint(t);
            distanceTraveled += Vector3.Distance(currentPos, nextPos);
            currentPos = nextPos;
        }
        return t;
    }
    public float GetPreviousPositionByDistance(float d, float t, bool loop = false)
    {
        float distanceTraveled = 0f;
        Vector3 currentPos = GetPoint(t);
        Vector3 nextPos;
        while(distanceTraveled < d)
        {
            t -= .001f;
            if (loop && t < 0f)
            {
                t += 1f;
            }
            nextPos = GetPoint(t);
            distanceTraveled += Vector3.Distance(currentPos, nextPos);
            currentPos = nextPos;
        }
        return t;
    }
    public void AddCurve()
    {
        Vector3 point = _points[_points.Length - 1];
        Array.Resize(ref _points, _points.Length + 3);
        point.x += 24f;
        _points[_points.Length - 3] = point;
        point.x += 24f;
        _points[_points.Length - 2] = point;
        point.x += 24f;
        _points[_points.Length - 1] = point;

        Array.Resize(ref _modes, _modes.Length + 1);
        _modes[_modes.Length - 1] = _modes[_modes.Length - 2];
        EnforceMode(_points.Length - 4);

        if (_loop)
        {
            _points[_points.Length - 1] = _points[0];
            _modes[_modes.Length - 1] = _modes[0];
            EnforceMode(0);
        }
    }    
    private void EnforceMode(int index)
    {
        int modeIndex = (index + 1) / 3;
        BezierControlPointMode mode = _modes[modeIndex];
        if (mode == BezierControlPointMode.Free || !_loop && (modeIndex == 0 || modeIndex == _modes.Length - 1))
        {
            return;
        }

        int middleIndex = modeIndex * 3;
        int fixedIndex;
        int enforcedIndex;
        if (index <= middleIndex)
        {
            fixedIndex = middleIndex - 1;
            if(fixedIndex < 0)
            {
                fixedIndex = _points.Length - 2;
            }
            enforcedIndex = middleIndex + 1;
            if (enforcedIndex >= _points.Length)
            {
                enforcedIndex = 1;
            }
        }
        else
        {
            fixedIndex = middleIndex + 1;
            if(fixedIndex >= _points.Length)
            {
                fixedIndex = 1;
            }
            enforcedIndex = middleIndex - 1;
            if(enforcedIndex < 0)
            {
                enforcedIndex = _points.Length - 2;
            }
        }

        Vector3 middle = _points[middleIndex];
        Vector3 enforcedTangent = middle - _points[fixedIndex];
        if (mode == BezierControlPointMode.Aligned)
        {
            enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, _points[enforcedIndex]);
        }
        _points[enforcedIndex] = middle + enforcedTangent;
    }
    public void Reset()
    {
        _points = new Vector3[]
        {
            new Vector3 (24f, 0f, 0f),
            new Vector3 (48f, 0f, 0f),
            new Vector3 (72f, 0f, 0f),
            new Vector3 (96f, 0f, 0f)
        };
        _modes = new BezierControlPointMode[]
        {
            BezierControlPointMode.Free,
            BezierControlPointMode.Free
        };
    }
}
