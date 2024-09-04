using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PxBounds
{
    public PxBounds(Vector2 position, Vector2 size)
    {
        Size = size;
        Position = position;       
    }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
}
