using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace PxMath
{
    public struct Dimensions
    {
        public readonly int width;
        public readonly int height;
        public Dimensions(int w, int h)
        {
            this.width = w;
            this.height = h;
        }

        public override string ToString()
        {
            return width.ToString() + "," + height.ToString();
        }
    }
}

