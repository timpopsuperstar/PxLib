using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBounds
{
   Bounds Bounds { get;}
   Bounds SafeZone { get; }
   Transform Transform { get; }
}
