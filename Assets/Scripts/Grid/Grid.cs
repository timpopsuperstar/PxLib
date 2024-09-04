using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PxMath;

public class Grid<T>
{    
    Dimensions dimensions;
    Dimensions cellSize;    
    private T[,] gridArray;
    private Vector2 originPosition;
    public Dimensions Dimensions { get { return dimensions; } set { dimensions = value; } }
    public Dimensions CellSize { get { return cellSize; } set { dimensions = value; } }
    public Vector2 OriginPosition { get { return originPosition; } set { originPosition = value; } }

    public Grid(Dimensions dimensions, Dimensions cellSize, Vector2 originPosition)
    {
        this.dimensions = dimensions;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        gridArray = new T[dimensions.width,dimensions.height];
    }

    public T GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < dimensions.width && y < dimensions.height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default;
        }
    }

    public void SetGridObject(int x, int y, T obj)
    {
        if (x >= 0 && y >= 0 && x < dimensions.width && y < dimensions.height)
        {
            gridArray[x, y] = obj;
        }
    }
}
