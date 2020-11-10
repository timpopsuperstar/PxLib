﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Vector3[] points;

    public Vector3 GetPoint(float t)
    {
        return transform.TransformPoint(Bezier.GetPoint(points[0], points[1], points[2], points[3], t));
    }

    public Vector3 GetVelocity(float t)
    {
        return transform.TransformPoint(Bezier.GetFirstDerivative(points[0], points[1], points[2], points[3], t)) - transform.position;
    }

    public Vector3 GetDirection(float t)
    {
        return GetVelocity(t).normalized;
    }
    public void Reset()
    {
        points = new Vector3[]
        {
            new Vector3 (24f, 0f, 0f),
            new Vector3 (48f, 0f, 0f),
            new Vector3 (72f, 0f, 0f),
            new Vector3 (96f, 0f, 0f)
        };
    }
}
