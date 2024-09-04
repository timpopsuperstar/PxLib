using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PxGridExtensions
{
    public static Vector2Int GetPosCenter(Vector2Int size)
    {
        return new Vector2Int((int)size.x / 2, (int)size.y / 2);
    }
    public static Vector2Int GetPosAbove(this Vector2Int pos, Vector2Int size, bool clamp = true)
    {
        pos.y = clamp ? Mathf.Clamp(pos.y - 1, 0, size.y - 1) : ((pos.y - 1) + size.y) % size.y;
        return pos;
    }
    public static Vector2Int GetPosBelow(this Vector2Int pos, Vector2Int size, bool clamp = true)
    {
        pos.y = clamp ? Mathf.Clamp(pos.y + 1, 0, size.y - 1) : ((pos.y + 1) + size.y) % size.y;
        return pos;
    }
    public static Vector2Int GetPosRight(this Vector2Int pos, Vector2Int size, bool clamp = true)
    {
        pos.x = clamp ? Mathf.Clamp(pos.x + 1, 0, size.x - 1) : ((pos.x + 1) + size.x) % size.x;
        return pos;
    }
    public static Vector2Int GetPosLeft(this Vector2Int pos, Vector2Int size, bool clamp = true)
    {
        pos.x = clamp ? Mathf.Clamp(pos.x - 1, 0, size.x - 1) : ((pos.x - 1) + size.x) % size.x;
        return pos;
    }
    public static Vector2Int GetPosTopLeft(this Vector2Int pos, Vector2Int size, bool clamp = true)
    {
        pos.y = clamp ? Mathf.Clamp(pos.y - 1, 0, size.y - 1) : ((pos.y - 1) + size.y) % size.y;
        pos.x = clamp ? Mathf.Clamp(pos.x - 1, 0, size.x - 1) : ((pos.x - 1) + size.x) % size.x;
        return pos;
    }
    public static Vector2Int GetPosTopRight(this Vector2Int pos, Vector2Int size, bool clamp = true)
    {
        pos.y = clamp ? Mathf.Clamp(pos.y - 1, 0, size.y - 1) : ((pos.y - 1) + size.y) % size.y;
        pos.x = clamp ? Mathf.Clamp(pos.x + 1, 0, size.x - 1) : ((pos.x + 1) + size.x) % size.x;
        return pos;
    }
    public static Vector2Int GetPosBotLeft(this Vector2Int pos, Vector2Int size, bool clamp = true)
    {
        pos.y = clamp ? Mathf.Clamp(pos.y + 1, 0, size.y - 1) : ((pos.y + 1) + size.y) % size.y;
        pos.x = clamp ? Mathf.Clamp(pos.x - 1, 0, size.x - 1) : ((pos.x - 1) + size.x) % size.x;
        return pos;
    }
    public static Vector2Int GetPosBotRight(this Vector2Int pos, Vector2Int size, bool clamp = true)
    {
        pos.y = clamp ? Mathf.Clamp(pos.y + 1, 0, size.y - 1) : ((pos.y + 1) + size.y) % size.y;
        pos.x = clamp ? Mathf.Clamp(pos.x + 1, 0, size.x - 1) : ((pos.x + 1) + size.x) % size.x;
        return pos;
    }
    public static List<Vector2Int> GetAdjacent(this Vector2Int pos, Vector2Int size, bool clamp = true)
    {
        var adjacentPositions = new List<Vector2Int>();
        adjacentPositions.Add(pos.GetPosAbove(size, clamp));
        adjacentPositions.Add(pos.GetPosBelow(size, clamp));
        adjacentPositions.Add(pos.GetPosRight(size, clamp));
        adjacentPositions.Add(pos.GetPosLeft(size, clamp));
        return adjacentPositions;
    }
    public static List<Vector2Int> GetDiagonal(this Vector2Int pos, Vector2Int size, bool clamp = true)
    {
        var diagonalPositions = new List<Vector2Int>();
        diagonalPositions.Add(pos.GetPosBotRight(size, clamp));
        diagonalPositions.Add(pos.GetPosBotLeft(size, clamp));
        diagonalPositions.Add(pos.GetPosTopRight(size, clamp));
        diagonalPositions.Add(pos.GetPosTopLeft(size, clamp));
        return diagonalPositions;
    }
    public static List<Vector2Int> GetNeighbors(this Vector2Int pos, Vector2Int size, bool clamp = true)
    {
        var neighbors = new List<Vector2Int>();
        var adjacent = pos.GetAdjacent(size, clamp);
        var diagonal = pos.GetDiagonal(size, clamp);
        foreach(Vector2Int adjacentPos in adjacent)
        {
            neighbors.Add(adjacentPos);
        }
        foreach (Vector2Int diagPos in diagonal)
        {
            neighbors.Add(diagPos);
        }
        return neighbors;
    }
}
