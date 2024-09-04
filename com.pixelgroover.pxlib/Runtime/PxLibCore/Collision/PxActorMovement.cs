using System.Collections.Generic;
using UnityEngine;

public static class PxActorMovement 
{
    //public static Vector2 GetLegalDirections(Vector2 dir, float dist, float distBuffer, LayerMask layerMask, FieldActor actor)
    //{
    //    var xSigned = Mathf.Sign(dir.x);
    //    var ySigned = Mathf.Sign(dir.y);
    //    if (dir.IsDiagonal())
    //    {
    //        if (CanMoveDiagonally(dir, dist + distBuffer, layerMask, actor))
    //        {
    //            if (!CanMove(ySigned.AsVector2y(), dist + distBuffer, layerMask, actor))
    //            {
    //                dir.y = 0;
    //            }
    //            if (!CanMove(xSigned.AsVector2x(), dist + distBuffer, layerMask, actor))
    //            {
    //                dir.x = 0;
    //            }
    //        }
    //        else
    //        {
    //            dir.y = Mathf.Sign(dir.y);
    //            dir.x = Mathf.Sign(dir.x);
    //            if (!CanMove(dir.y.AsVector2y(), dist + distBuffer, layerMask, actor))
    //            {
    //                dir.y = 0;
    //            }
    //            if (!CanMove(dir.x.AsVector2x(), dist + distBuffer, layerMask, actor))
    //            {
    //                dir.x = 0;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (dir.y != 0 && !CanMove(ySigned.AsVector2y(), dist + distBuffer, layerMask, actor))
    //        {
    //            dir.y = 0;
    //        }
    //        if (dir.x != 0 && !CanMove(xSigned.AsVector2x(), dist + distBuffer, layerMask, actor))
    //        {
    //            dir.x = 0;
    //        }
    //    }
    //    return dir;
    //}
    //public static bool CanMove(Vector2 dir, float distance, LayerMask layerMask, FieldActor actor)
    //{
    //    var hits = Raycaster.CastRaysFromCollider(actor.BoxCollider, 8, dir, distance, layerMask);
    //    var nonCollisionTriggerHits = new List<RaycastHit2D>();
    //    foreach (RaycastHit2D hit in hits)
    //    {
    //        if (hit.collider.gameObject.GetComponent<ICollisionTrigger>() == null)
    //        {
    //            nonCollisionTriggerHits.Add(hit);
    //        }
    //        else
    //        {
    //            var collisionTrigger = hit.collider.gameObject.GetComponent<ICollisionTrigger>();
    //            if(collisionTrigger.isNpcCollision() && actor is NpcFieldActor)
    //            {
    //                nonCollisionTriggerHits.Add(hit);
    //            }
    //        }
    //    }
    //    return nonCollisionTriggerHits.Count == 0;
    //}
    //public static bool CanMoveDiagonally(Vector2 dir, float distance, LayerMask layerMask, FieldActor actor)
    //{
    //    var hits = Raycaster.CastDiagonalRayFromCollider(actor.BoxCollider, dir, distance, layerMask);
    //    var nonCollisionTriggerHits = new List<RaycastHit2D>();
    //    foreach (RaycastHit2D hit in hits)
    //    {
    //        if (hit.collider.gameObject.GetComponent<ICollisionTrigger>() == null)
    //        {
    //            nonCollisionTriggerHits.Add(hit);
    //        }
    //    }
    //    return nonCollisionTriggerHits.Count == 0;
    //}

    //public static void Move(this FieldActor actor, Vector2 dir,  float dist, bool toInt = false) => SetPosition( actor, (Vector2)actor.Position + dir.normalized * dist, toInt);
    //private static void SetPosition(this FieldActor actor, Vector2 p, bool toInt)
    //{
    //    actor.Position = toInt ? new Vector3(Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y), actor.Position.z) : new Vector3(p.x, p.y, actor.Position.z);
    //}
}
