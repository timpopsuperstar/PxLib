using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public static class Raycaster
{    
    const float skinWidth = -.015f;
    const int maxRayCount = 256;


    public static List<RaycastHit2D> CastRay(Vector2 pos, Vector2 dir = default, float dist = 0f, int layerMask = Physics2D.DefaultRaycastLayers) => Physics2D.RaycastAll(pos, dir, dist, layerMask).ToList();    
    public static List<RaycastHit2D> CastDiagonalRayFromCollider(BoxCollider2D collider, Vector2 dir, float dist, int layerMask = Physics2D.DefaultRaycastLayers)
    {
        RaycastOrigins raycastOrigins = GetRaycastOrigins(collider);
        Direction direction = dir.ToPxDirection();
        Vector2 signedDirection = dir.ToSign();
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        switch (direction)
        {
            case Direction.Southeast:
                var southeastHits = Physics2D.RaycastAll(raycastOrigins.bottomRight, signedDirection, dist, layerMask);
                Debug.DrawRay(raycastOrigins.bottomRight, dir * (dist), Color.magenta);
                if (southeastHits.Length > 0)
                {
                    hits.AddRange(southeastHits);
                }
                break;
            case Direction.Northeast:
                var northeastHits = Physics2D.RaycastAll(raycastOrigins.topRight, signedDirection, dist, layerMask);
                Debug.DrawRay(raycastOrigins.topRight, dir * (dist), Color.magenta);
                if (northeastHits.Length > 0)
                {
                    hits.AddRange(northeastHits);
                }
                break;

            case Direction.Northwest:
                var northwestHits = Physics2D.RaycastAll(raycastOrigins.topLeft, signedDirection, dist, layerMask);
                Debug.DrawRay(raycastOrigins.topLeft, dir * (dist), Color.magenta);
                if (northwestHits.Length > 0)
                {
                    hits.AddRange(northwestHits);
                }
                break;
            case Direction.Southwest:
                var southwestHits = Physics2D.RaycastAll(raycastOrigins.bottomLeft, signedDirection, dist, layerMask);
                Debug.DrawRay(raycastOrigins.bottomLeft, dir * (dist), Color.magenta);
                if (southwestHits.Length > 0)
                {
                    hits.AddRange(southwestHits);
                }
                break;
        }
        return hits;
    }
    public static List<RaycastHit2D> CastRaysFromCollider(BoxCollider2D collider, int rayCount, Vector2 dir, float dist, int layerMask = Physics2D.DefaultRaycastLayers)
    {
        RaycastOrigins raycastOrigins = GetRaycastOrigins(collider);
        var raySpacingVertical = GetRaySpacing(collider, rayCount, Vector2.up);
        var raySpacingHorizontal = GetRaySpacing(collider, rayCount, Vector2.right);
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        for (int i = 0; i < rayCount; i++)
        {
            if (dir.y == 1)
            {
                //ray cast north
                Debug.DrawRay(raycastOrigins.topLeft + Vector2.right * raySpacingHorizontal * i, Vector2.up * dist, Color.green);
                var partialHitsNorth = Physics2D.RaycastAll(raycastOrigins.topLeft + Vector2.right * raySpacingHorizontal * i, Vector2.up, dist, layerMask);
                if (partialHitsNorth.Length > 0)
                {
                    hits.AddRange(partialHitsNorth);
                }
            }
            if (dir.x == 1)
            {
                //ray cast east
                Debug.DrawRay(raycastOrigins.topRight + Vector2.down * raySpacingVertical * i, Vector2.right * dist, Color.green);
                var partialHitsEast = Physics2D.RaycastAll(raycastOrigins.topRight + Vector2.down * raySpacingVertical * i, Vector2.right, dist, layerMask);
                if (partialHitsEast.Length > 0)
                {
                    hits.AddRange(partialHitsEast);
                }
            }
            if (dir.x == -1)
            {
                //ray cast west
                Debug.DrawRay(raycastOrigins.topLeft + Vector2.down * raySpacingVertical * i, Vector2.right * -dist, Color.green);
                var partialHitsWest = Physics2D.RaycastAll(raycastOrigins.topLeft + Vector2.down * raySpacingVertical * i, Vector2.right, -dist, layerMask);
                if (partialHitsWest.Length > 0)
                {
                    hits.AddRange(partialHitsWest);
                }
            }
            if (dir.y == -1)
            {
                //ray cast south
                Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right * raySpacingHorizontal * i, Vector2.up * -dist, Color.green);
                var partialHitsSouth = Physics2D.RaycastAll(raycastOrigins.bottomLeft + Vector2.right * raySpacingHorizontal * i, Vector2.up, -dist, layerMask);
                if (partialHitsSouth.Length > 0)
                {
                    hits.AddRange(partialHitsSouth);
                }
            }
        }
        return hits;
    }
    static RaycastOrigins GetRaycastOrigins(BoxCollider2D collider)
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        RaycastOrigins raycastOrigins;
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);

        return raycastOrigins;
    }
    static float GetRaySpacing(BoxCollider2D collider, int rayCount, Vector2 dir)
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        rayCount = Mathf.Clamp(rayCount, 2, maxRayCount);
        var raySpacing = 0f;
        if (dir.x != 0)
        {
            raySpacing = bounds.size.x / (rayCount - 1);
        }
        if (dir.y != 0)
        {
            raySpacing = bounds.size.y / (rayCount - 1);
        }
        return raySpacing;
    }
    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}
