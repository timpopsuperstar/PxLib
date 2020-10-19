using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBounds
{
   Bounds Bounds { get;}
   Bounds SafeZone { get; }
   Transform Transform { get; }

    //public event EventHandler PositionChanged;
    event EventHandler SizeChanged;
}

public class MyEventArgs : EventArgs
{

}
