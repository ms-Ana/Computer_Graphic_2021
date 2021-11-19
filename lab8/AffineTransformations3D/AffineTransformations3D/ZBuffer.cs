using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AffineTransformations3D
{
    class ZBuffer
    {
        public static int[,] ZBufferAlgorithm(int width, int height, List<Polyhedron3D> polyhedron3Ds)
        {
            int[,] result = new int[height, width];
            double[,] buff = new double[height, width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    result[i, j] = 255;
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    buff[i, j] = double.MinValue;
            List<List<List<Point3D>>> rasterizedPolyhedrons = new List<List<List<Point3D>>>();
            foreach (var polyhedron in polyhedron3Ds)
                rasterizedPolyhedrons.Add(Rasterize(polyhedron, width, height));

            foreach (var rasterizedPolyhedron in rasterizedPolyhedrons)
            {
                foreach (var rasterizedPolygon in rasterizedPolyhedron)
                    foreach (var point in rasterizedPolygon)
                    {
                        int x = (int)point.x;
                        int y = (int)point.y;
                        if (x < width && x > 0 && y < height && y > 0)
                            if (point.z > buff[x, y])
                                buff[x, y] = point.z;
                    }

            }
            var minRange = GraphMath3D.Min2D(buff);
            var maxRange = GraphMath3D.Max2D(buff);
            var range = maxRange - minRange;
            for (int i = 0; i < buff.GetLength(0); i++)
            {
                for (int j = 0; j < buff.GetLength(1); j++)
                    result[i, j] = (int)((buff[i, j] - minRange) * 255 / range);
            }
            return result;
        }


        private static List<List<Point3D>> Rasterize(Polyhedron3D polyhedron, int width, int height)
        {
            List<List<Point3D>> rasterizedPolyhedron = new List<List<Point3D>>();
            foreach (var polygon in polyhedron.polygons)
            {
                List<Point3D> rasterizedPolygon = new List<Point3D>();
                var polygonPoints = PolygonToPoints(polygon);
                var triangles = Triangulate(polygonPoints);
                foreach (var triangle in triangles)
                    rasterizedPolygon.AddRange(RasterizeTriangle(triangle, width, height));
                rasterizedPolyhedron.Add(rasterizedPolygon);
            }

            return rasterizedPolyhedron;
        }

        private static List<List<Point3D>> Triangulate(List<Point3D> polygonPoints)
        {
            if (polygonPoints.Count == 3)
                return new List<List<Point3D>> { polygonPoints };
            List<List<Point3D>> trianglePoints = new List<List<Point3D>>();
            for (int i = 2; i < polygonPoints.Count; i++)
                trianglePoints.Add(new List<Point3D> { polygonPoints[0], polygonPoints[i - 1], polygonPoints[i] });
            return trianglePoints;
        }
        private static List<Point3D> PolygonToPoints(Polygon3D polygon)
        {
            List<Point3D> resultPoints = new List<Point3D>();
            foreach (var line in polygon.lines)
                resultPoints.Add(line.first);
            return resultPoints;
        }
        private static List<Point3D> RasterizeTriangle(List<Point3D> point3Ds, int width, int height)
        {
            List<Point3D> rasterizedTriangle = new List<Point3D>();
            var triangle = point3Ds.OrderBy(point => -point.y).ToList();

            if (triangle[0].y == triangle[1].y)
                return RasterizeTopTriangle(point3Ds, width, height);
            else if (triangle[1].y == triangle[2].y)
                return RasterizeBottomTriangle(point3Ds, width, height);
            else
            {
                var x = (triangle[0].x + ((triangle[1].y - triangle[0].y) / (triangle[2].y - triangle[0].y))) * (triangle[2].x - triangle[0].x);
                var point = new Point3D(x, triangle[1].y, 0);
                var left = GraphMath3D.EuclideanDist(point, triangle[0]);
                var right = GraphMath3D.EuclideanDist(point, triangle[2]);
                var length = left + right + 1e-9;
                var z = right / length * triangle[0].z + left / length * triangle[2].z;

                var top_triangle = RasterizeTopTriangle(new List<Point3D> { triangle[1], point, triangle[2] }, width, height);
                var bottom_triangle = RasterizeBottomTriangle(new List<Point3D> { triangle[0], triangle[1], point }, width, height);

                return top_triangle.Concat(bottom_triangle).ToList();


            }
        }

        private static List<Point3D> RasterizeTopTriangle(List<Point3D> point3Ds, int width, int height)
        {
            List<Point3D> rasterizedTriangle = new List<Point3D>();
            var triangle = point3Ds.OrderBy(point => -point.y).ToList();
            if (triangle[0].x > triangle[1].x)
            {
                var temp = triangle[0];
                triangle[0] = triangle[1];
                triangle[1] = temp;
            }
            int dy1 = Convert.ToInt32(height - triangle[2].y),
                dy2 = Convert.ToInt32(height - triangle[0].y);
            double slope1 = (triangle[2].x - triangle[0].x) / (triangle[2].y - triangle[0].y),
                   slope2 = (triangle[2].x - triangle[1].x) / (triangle[2].y - triangle[1].y);

            var line1 = GraphMath3D.EuclideanDist(triangle[2], triangle[0]);
            var line2 = GraphMath3D.EuclideanDist(triangle[2], triangle[1]);

            var curx1 = triangle[2].x;
            var curx2 = curx1;

            var curx_border1 = curx1;
            var curx_border2 = curx2;

            var z1 = triangle[2].z;
            var z2 = triangle[2].z;

            dy2 = dy2 > 0 ? dy2 - 1 : dy2;
            for (var y = dy1; y > dy2; --y)
            {
                for (var x = curx_border1; x <= curx_border2; x++)
                {
                    var left = Math.Abs(x - curx_border1);
                    var right = Math.Abs(curx_border2 - x);
                    var length = left + right + 1e+9;
                    rasterizedTriangle.Add(new Point3D(x, y, right / length * z1 + left / length * z2));
                }

                curx1 += slope1;
                curx2 += slope2;

                curx_border1 = Convert.ToInt32(curx1);
                curx_border2 = Convert.ToInt32(curx1);

                z1 = ((line1 - GraphMath3D.EuclideanDist(new Point3D(curx_border1, y, 0), new Point3D(triangle[2].x, height - triangle[2].y, 0))) / line1) * triangle[2].z +
                    ((line1 - GraphMath3D.EuclideanDist(new Point3D(curx_border1, y, 0), new Point3D(triangle[0].x, height - triangle[0].y, 0))) / line1) * triangle[0].z;
                z2 = ((line2 - GraphMath3D.EuclideanDist(new Point3D(curx_border2, y, 0), new Point3D(triangle[2].x, height - triangle[2].y, 0))) / line2) * triangle[2].z
                    + ((line2 - GraphMath3D.EuclideanDist(new Point3D(curx_border2, y, 0), new Point3D(triangle[1].x, height - triangle[1].y, 0))) / line2) * triangle[1].z;
            }
            return rasterizedTriangle;
        }
        private static List<Point3D> RasterizeBottomTriangle(List<Point3D> point3Ds, int width, int height)
        {
            List<Point3D> rasterizedTriangle = new List<Point3D>();
            var triangle = point3Ds.OrderBy(point => -point.y).ToList();
            if (triangle[1].x > triangle[2].x)
            {
                var temp = triangle[1];
                triangle[1] = triangle[2];
                triangle[2] = temp;
            }
            int dy1 = Convert.ToInt32(height - triangle[0].y),
                dy2 = Convert.ToInt32(height - triangle[1].y);

            var slope1 = (triangle[1].x - triangle[0].x) / (triangle[1].y - triangle[0].y);
            var slope2 = (triangle[2].x - triangle[0].x) / (triangle[2].y - triangle[0].y);

            var line1 = GraphMath3D.EuclideanDist(triangle[0], triangle[1]);
            var line2 = GraphMath3D.EuclideanDist(triangle[0], triangle[2]);

            var curx1 = triangle[0].x;
            var curx2 = triangle[0].x;

            var curx_border1 = Convert.ToInt32(curx1);
            var curx_border2 = Convert.ToInt32(curx2);

            var z1 = triangle[0].z;
            var z2 = triangle[0].z;

            dy2 = dy2 < height ? dy2 + 1 : dy2;

            for (int y = dy1; y < dy2; ++y)
            {
                for (int x = curx_border1; x <= curx_border2; x++)
                {
                    var left = Math.Abs(x - curx_border1);
                    var right = Math.Abs(curx_border2 - x);
                    var length = left + right + 1e-9;

                    rasterizedTriangle.Add(new Point3D(x, y, right / length * z1 + left / length * z2));
                }

                curx1 -= slope1;
                curx2 -= slope2;

                curx_border1 = Convert.ToInt32(curx1);
                curx_border2 = Convert.ToInt32(curx2);

                z1 = ((line1 - GraphMath3D.EuclideanDist(new Point3D(curx_border1, y, 0), new Point3D(triangle[0].x, height - triangle[0].y, 0))) / line1) * triangle[0].z +
                   ((line1 - GraphMath3D.EuclideanDist(new Point3D(curx_border1, y, 0), new Point3D(triangle[1].x, height - triangle[1].y, 0))) / line1) * triangle[1].z;
                z2 = ((line2 - GraphMath3D.EuclideanDist(new Point3D(curx_border2, y, 0), new Point3D(triangle[0].x, height - triangle[0].y, 0))) / line2) * triangle[0].z
                    + ((line2 - GraphMath3D.EuclideanDist(new Point3D(curx_border2, y, 0), new Point3D(triangle[2].x, height - triangle[2].y, 0))) / line2) * triangle[2].z;
            }
            return rasterizedTriangle;
        }
    }
}
