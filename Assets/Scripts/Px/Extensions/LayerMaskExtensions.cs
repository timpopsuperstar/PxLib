using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskExtensions 
{
    public static int ToLayerMask(this int layerIndex) => 1 << layerIndex;
}
