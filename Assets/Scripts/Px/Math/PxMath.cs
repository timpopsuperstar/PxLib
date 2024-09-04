using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PxMath 
{
    public static int ToPositive(this int i) => i < 0 ? i * -1 : i;
    public static float ToPositive(this float f) => f < 0 ? f * -1f : f;

    public static bool Within(this float f, float f2)
    {
        var query = f.ToPositive() <= f2 ? true : false;
        return query;
    }
}
