using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
    public static Vector2 ToInt(this Vector2 v) => new Vector2((int)v.x, (int)v.y);
    public static Vector2Int ToVector2Int(this Vector2 v) => new Vector2Int((int)v.x, (int)v.y);
    public static Vector3 ToInt(this Vector3 v) => new Vector3((int)v.x, (int)v.y, (int)v.z);
    public static Vector3Int ToVector3Int(this Vector3 v) => new Vector3Int((int)v.x, (int)v.y, (int)v.z);
    public static Vector3Int ToVector3Int(this Vector2 v, int z) => new Vector3Int((int)v.x, (int)v.y, z);
    public static Vector3 ToVector3(this Vector2 v, int z) => new Vector3(v.x, v.y, z);
    public static Vector2 WithoutX(this Vector2 v) => new Vector2(0, v.y);
    public static Vector2 WithoutY(this Vector2 v) => new Vector2(v.x,0);    
    public static Vector3 ReplaceZ(this Vector3 v, float z) => new Vector3 (v.x, v.y, z);
    public static Vector3Int ReplaceZ(this Vector3Int v, int z) => new Vector3Int(v.x, v.y, z);
}
