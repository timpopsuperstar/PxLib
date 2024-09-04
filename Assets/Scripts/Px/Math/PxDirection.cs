using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Direction { South, Southwest, West, Northwest, North, Northeast, East, Southeast, Neutral };
public static class PxDirection
{
    public static Dictionary<Vector2, Direction> Directions = new Dictionary<Vector2, Direction>()
    {
        {new Vector2(0,0), Direction.Neutral},
        {new Vector2(0,-1), Direction.South},
        {new Vector2(-1,-1), Direction.Southwest},
        {new Vector2(-1,0), Direction.West},
        {new Vector2(-1, 1), Direction.Northwest},
        {new Vector2(0,1), Direction.North},
        {new Vector2(1,1), Direction.Northeast},
        {new Vector2(1,0), Direction.East},
        {new Vector2(1,-1), Direction.Southeast},
    };
    public static Dictionary<Direction, Vector2> PxDirections = new Dictionary<Direction, Vector2>()
    {
        {Direction.Neutral, new Vector2(0,0)},
        {Direction.South, new Vector2(0,-1)},
        {Direction.Southwest, new Vector2(-1,-1)},
        {Direction.West, new Vector2(-1,0)},
        {Direction.Northwest, new Vector2(-1, 1)},
        {Direction.North, new Vector2(0,1) },
        {Direction.Northeast, new Vector2(1,1) },                     
        {Direction.East, new Vector2(1,0)},   
        {Direction.Southeast, new Vector2(1, -1)} 
    };

    //Random Directions
    public static Direction GetRandom() => (Direction)PxRand.Random.Next(0, 8);
    public static Direction GetRandomHorizontal()
    {
        var dir = PxRand.Random.Next(0, 2) == 1 ? Direction.East : Direction.West;
        return dir;
    }
    public static Direction GetRandomVertical()
    {
        var dir = PxRand.Random.Next(0, 2) == 1 ? Direction.South : Direction.North;
        return dir;
    }
    public static Direction GetRandomDiagonal()
    {
        var dirX = PxRand.Random.Next(0, 2) == 1 ? 1 : -1;
        var dirY = PxRand.Random.Next(0, 2) == 1 ? 1 : -1;
        return new Vector2(dirX, dirY).ToPxDirection();
    }
    public static Direction GetNextDiagonal(Direction dir)
    {
        switch (dir)
        {
            case Direction.Southwest:
                return Direction.Northwest;
            case Direction.Northwest:
                return Direction.Northeast;
            case Direction.Northeast:
                return Direction.Southeast;
            case Direction.Southeast:
                return Direction.Southwest;
        }
        return GetRandomDiagonal();
    }
    //PxDirection Extensions
    public static Direction ToPxDirection(this Vector2 v) => Directions[v.ToSign()];
    public static Direction ToPxCardinalDirection(this Vector2 v) => Directions[v.ToCardinal()];
    public static Vector2 ToVector2(this Direction d)=> Directions.FirstOrDefault(x => x.Value == d).Key;

    //Vector2 Extensions
    public static Vector2 ToSign(this Vector2 dir)
    {
        if (dir.x != 0)
        {
            if (dir.x > 0)
            {
                dir.x = 1;
            }
            else if (dir.x < 0)
            {
                dir.x = -1;
            }
        }
        if (dir.y != 0)
        {
            if (dir.y > 0)
            {
                dir.y = 1;
            }
            else if (dir.y < 0)
            {
                dir.y = -1;
            }
        }
        return dir;
    }
    public static Vector2 ToCardinal(this Vector2 dir)
    {
        dir = dir.ToSign();
        if (dir.y != 0)
        {
            dir.x = 0;
            return dir;
        }
        if (dir.x != 0)
        {
            dir.y = 0;
            return dir;
        }
        return dir;
    }
    public static bool IsDiagonal(this Vector2 v)
    {
        if (v.x != 0 && v.y != 0)
            return true;
        else
            return false;
    }
    public static int ToInt(this Direction d) => (int)d;
}
