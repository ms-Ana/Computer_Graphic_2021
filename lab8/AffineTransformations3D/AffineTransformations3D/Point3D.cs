using System;
using System.Collections.Generic;
using System.Text;

namespace AffineTransformations3D
{
    public class Point3D
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public Point3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public static Point3D operator -(Point3D leftPoint, Point3D rightPoint)
        {
            return new Point3D(leftPoint.x - rightPoint.x,
                               leftPoint.y - rightPoint.y,
                               leftPoint.z - rightPoint.z);
        }
    }
}
