using System;
using System.Collections.Generic;
using System.Text;

namespace AffineTransformations3D
{
    class GouraudShading
    {
        private static double ConvertCosToBrightness(double cos)
        {
            return (cos + 1) / 2;
        }
        private static double ModelLambert(Point3D point, Point3D normal, Point3D light)
        {
            Point3D rayLight = new Point3D(point.x - light.x, point.y - light.y, point.z - light.z);
            double cos = GraphMath3D.CosDist(rayLight, normal);
            return ConvertCosToBrightness(cos);
        }

        private static void CalculateShading(Polyhedron3D polyhedron, Point3D point)
        {
            Dictionary<int, Point3D> pointsnormal;


        }
    }
}
