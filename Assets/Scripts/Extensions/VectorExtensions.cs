using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions 
{
    public static Vector2 ToInt(this Vector2 v)
    {
        return new Vector2((int)v.x, (int)v.y);
    }
    public static Vector3 ToInt(this Vector3 v)
    {
        return new Vector3((int)v.x, (int)v.y, (int)v.z);
    }

    public static Vector2 WithoutY(this Vector2 v, Vector2 v2)
    {
        return new Vector2(v2.x, v.y);
    }
    public static Vector2 WithoutX(this Vector2 v, Vector2 v2)
    {
        return new Vector2(v.x, v2.y);
    }
}
