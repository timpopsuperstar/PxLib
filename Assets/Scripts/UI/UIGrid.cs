using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class UIGrid : MonoBehaviour
{

    [ReorderableList] public List<GameObject> gridObjects;
    public Vector2 Dimensions
    {
        get;
        private set;
    }
    public int Width { get { return (int)Dimensions.x; } }
    public int Height { get { return (int)Dimensions.y; } }

    private void Awake()
    {
        
    }

    public void InitializeGrid(Vector2 dimensions, List<GameObject> objs, Vector2 position)
    {
        Dimensions = dimensions;
    }
}
