using System;
using System.Collections.Generic;
using System.Text;

namespace AffineTransformations3D
{
    class Point3DWithTexture : Point3D
    {
        public double xTex;
        public double yTex;

        public Point3DWithTexture(double x, double y, double z) : base(x, y, z) { }

        public Point3DWithTexture(double x, double y, double z, double xTex, double yTex) :
            base(x, y, z)
        {
            this.xTex = xTex;
            this.yTex = yTex;
        }
    }
}
