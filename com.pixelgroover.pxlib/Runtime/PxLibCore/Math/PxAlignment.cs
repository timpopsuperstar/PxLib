using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PxAlignment
{
    public enum Alignment
    {
        None,
        TopCenter,
        TopRight,
        CenterRight,
        BottomRight,
        BottomCenter,
        BottomLeft,
        CenterLeft,
        TopLeft,
        Center,
        Horizontal,
        Vertical,
        Bottom,
        Top
    }
    public static class Align
    {
        public static Vector2 ToBounds(Bounds childBounds, Bounds parentBounds, Alignment alignment)
        {
            var newPos = new Vector2();

            switch (alignment)
            {
                case Alignment.Center:
                {
                    newPos.x = 0;
                    newPos.y = 0;
                    break;
                }
                case Alignment.TopCenter:
                {
                    newPos.x = 0;
                    newPos.y = parentBounds.size.y / 2 - childBounds.size.y / 2;
                    break;
                }
                case Alignment.TopRight:
                {
                    newPos.x = parentBounds.size.x / 2 - childBounds.size.x / 2;
                    newPos.y = parentBounds.size.y / 2 - childBounds.size.y / 2;
                    break;
                }
                case Alignment.CenterRight:
                {
                    newPos.y = 0;
                    newPos.x = parentBounds.size.x / 2 - childBounds.size.x / 2;
                    break;
                }
                case Alignment.BottomRight:
                {
                    newPos.x = parentBounds.size.x / 2 - childBounds.size.x / 2;
                    newPos.y = -parentBounds.size.y / 2 + childBounds.size.y / 2;
                    break;
                }
                case Alignment.BottomCenter:
                {
                    newPos.x = 0;
                    newPos.y = -parentBounds.size.y / 2 + childBounds.size.y / 2;
                    break;
                }
                case Alignment.BottomLeft:
                {
                    newPos.x = -parentBounds.size.x / 2 + childBounds.size.x / 2;
                    newPos.y = -parentBounds.size.y / 2 + childBounds.size.y / 2;
                    break;
                }
                case Alignment.CenterLeft:
                {
                    newPos.y = 0;
                    newPos.x = -parentBounds.size.x / 2 + childBounds.size.x / 2;
                    break;
                }
                case Alignment.TopLeft:
                {
                    newPos.x = -parentBounds.size.x / 2 + childBounds.size.x / 2;
                    newPos.y = parentBounds.size.y / 2 - childBounds.size.y / 2;
                    break;
                }
            }
            return newPos;
        }
        public static Vector2 AlignTo(this Bounds b, Bounds parent, Alignment alignment) => ToBounds(b, parent, alignment);

        public static Bounds ToSafeZone(this Bounds b) => new Bounds(b.center, new Vector2((int)(b.size.x * .9f), (int)(b.size.y * .9f)));     

        public static Bounds SetAdjacent(this Bounds childBounds, Bounds parentBounds, Alignment alignment)
        {
            var newPos = new Vector2();

            switch (alignment)
            {
                case Alignment.Center:
                {
                    newPos.x = 0;
                    newPos.y = 0;
                    break;
                }
                case Alignment.TopCenter:
                {
                    newPos.x = 0;
                    newPos.y = parentBounds.size.y / 2 - childBounds.size.y / 2;
                    break;
                }
                case Alignment.TopRight:
                {
                    newPos.x = parentBounds.size.x / 2 - childBounds.size.x / 2;
                    newPos.y = parentBounds.size.y / 2 - childBounds.size.y / 2;
                    break;
                }
                case Alignment.CenterRight:
                {
                    newPos.y = 0;
                    newPos.x = parentBounds.size.x / 2 - childBounds.size.x / 2;
                    break;
                }
                case Alignment.BottomRight:
                {
                    newPos.x = parentBounds.size.x / 2 - childBounds.size.x / 2;
                    newPos.y = -parentBounds.size.y / 2 + childBounds.size.y / 2;
                    break;
                }
                case Alignment.BottomCenter:
                {
                    newPos.x = 0;
                    newPos.y = -parentBounds.size.y / 2 + childBounds.size.y / 2;
                    break;
                }
                case Alignment.BottomLeft:
                {
                    newPos.x = -parentBounds.size.x / 2 + childBounds.size.x / 2;
                    newPos.y = -parentBounds.size.y / 2 + childBounds.size.y / 2;
                    break;
                }
                case Alignment.CenterLeft:
                {
                    newPos.y = 0;
                    newPos.x = -parentBounds.size.x / 2 + childBounds.size.x / 2;
                    break;
                }
                case Alignment.TopLeft:
                {
                    newPos.x = -parentBounds.size.x / 2 + childBounds.size.x / 2;
                    newPos.y = parentBounds.size.y / 2 - childBounds.size.y / 2;
                    break;
                }
            }
            return new Bounds(newPos, childBounds.size);
        }
    }
}

