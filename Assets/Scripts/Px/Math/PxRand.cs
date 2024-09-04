using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PxRand 
{
    public static System.Random Random => _random != null ? _random : (_random = new System.Random(System.DateTime.Now.Second));
    private static System.Random _random;
    public static int GetRandom(int min, int max)
    {
        var rand = Random;
        return rand.Next(min, max);
    }
    public static T GetRandomFromArray<T>(this T[] t)
    {
        if (t.Length == 0) return default(T);
        return t[GetRandom(0, t.Length)];
    }
    public static T GetRandomFromList<T>(this List<T> t)
    {
        if (t.Count == 0) return default(T);
        return t[GetRandom(0, t.Count)];
    }
}
