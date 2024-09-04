using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastHit2DExtensions 
{
    public static T GetType<T>(this RaycastHit2D hit)
    {
        var hitObj = hit.transform.GetComponent<T>();
        if (hitObj != null)
            return hitObj;
        return default;
    }
    public static T TryGet<T>(this RaycastHit2D hit)
    {
        if (hit.transform.gameObject.TryGetComponent<T>(out T obj))
        {
            return obj;
        }
        return default;
    }
}
