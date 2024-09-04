using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PxBoxcaster 
{
    public static List<ICollisionTrigger> GetCollisions(this BoxCollider2D collider, LayerMask layerMask)
    {
        var hits = Physics2D.BoxCastAll(collider.bounds.center, collider.bounds.size, 0, Vector2.zero, 0, layerMask);                       
        var iCollisionTriggers = new List<ICollisionTrigger>();
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.GetComponent<ICollisionTrigger>() != null && hits[i].collider != collider)
            {
                if (!iCollisionTriggers.Contains(hits[i].collider.gameObject.GetComponent<ICollisionTrigger>()))
                {
                    iCollisionTriggers.Add(hits[i].collider.gameObject.GetComponent<ICollisionTrigger>());
                }
            }
        }
        return iCollisionTriggers;
    }
}
