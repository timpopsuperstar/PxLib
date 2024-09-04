using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtensions 
{
    public static float Interpolate(this float f0, float f1) => f0 / f1;

    public static Vector2 AsVector2y(this float y) => new Vector2(0, y);

    public static Vector2 AsVector2x(this float x) => new Vector2(x, 0);
}
