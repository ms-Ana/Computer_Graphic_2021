using System;
using System.Collections.Generic;
using System.Drawing;
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

        private static Dictionary<Point3D, Point3D> CalculateNormal2Points(Polyhedron3D polyhedron)
        {
            var points2polygons = GraphMath3D.PolyhedronToPoints(polyhedron);
            Dictionary<Point3D, Point3D> pointsNormals = new Dictionary<Point3D, Point3D>();
            List<Point3D> normals = new List<Point3D>();
            for (int i = 0; i < polyhedron.polygons.Count; i++)
                normals.Add(GraphMath3D.CalculateNormal(polyhedron.polygons[i]));
            
            foreach(var item in points2polygons)
            {
                Point3D point3D = new Point3D(0, 0, 0);
                foreach(var i in item.Value)
                {
                    point3D.x += normals[i].x;
                    point3D.y += normals[i].y;
                    point3D.z += normals[i].z;
                }
                point3D.x /= item.Value.Count;
                point3D.y /= item.Value.Count;
                point3D.z /= item.Value.Count;
                pointsNormals[item.Key] = point3D;
            }
            return pointsNormals;
        }

        private static Dictionary<Point3D, double> Shading(Polyhedron3D polyhedron, Point3D pointLight)
        {
            var pointsNormals = CalculateNormal2Points(polyhedron);
            Dictionary<Point3D, double> points2Lights = new Dictionary<Point3D, double>();
            foreach (var item in pointsNormals)
                points2Lights[item.Key] = ModelLambert(item.Key, item.Value, pointLight);
            return points2Lights;
        }

        public static Bitmap Gourand(Polyhedron3D polyhedron, int width, int height, Point3D pointLight, Color color)
        {
            var points2Lights = Shading(polyhedron, pointLight);
            Bitmap bitmap = new Bitmap(width, height);
            double[,] buff = new double[height, width];
            for (int i = 0; i < bitmap.Width; i++)
                for (int j = 0; j < bitmap.Height; j++)
                    bitmap.SetPixel(i, j, Color.White);
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    buff[i, j] = double.MinValue;


            var rasterizedPolyhedron = GraphMath3D.RasterizeWithLight(polyhedron, width, height, points2Lights);
            Point3D center = new Point3D(width / 2, height / 2, 0);
            for (int i = 0; i < rasterizedPolyhedron.Count; i++)
                foreach (var point in rasterizedPolyhedron[i])
                {
                    int x = (int)(point.Item1.x + center.x);
                    int y = (int)(point.Item1.y + center.y);
                    if (x < width && x > 0 && y < height && y > 0)
                        if (point.Item1.z > buff[x, y])
                        {
                            buff[x, y] = point.Item1.z;
                            int red = (int)(color.R * point.Item2) > 255 ? 255 : (int)(color.R * point.Item2);
                            int green = (int)(color.G * point.Item2) > 255 ? 255 : (int)(color.G * point.Item2);
                            int blue = (int)(color.B * point.Item2) > 255 ? 255 : (int)(color.B * point.Item2);
                            bitmap.SetPixel(x, y, Color.FromArgb(red, green, blue));
                        }
                }
            return bitmap;

        }
    }
}
