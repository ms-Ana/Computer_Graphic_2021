using System;
using System.Collections.Generic;
using System.Text;

namespace AffineTransformations3D
{
    class Line3D
    {
        public Point3DWithTexture first { get; set; }
        public Point3DWithTexture second { get; set; }

        public Line3D(Point3DWithTexture p1, Point3DWithTexture p2)
        {
            first = new Point3DWithTexture(p1.x, p1.y, p1.z, p1.xTex, p1.yTex);
            second = new Point3DWithTexture(p2.x, p2.y, p2.z, p1.xTex, p1.yTex);
        }
    }
}
