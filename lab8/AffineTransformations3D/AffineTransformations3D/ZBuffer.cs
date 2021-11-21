using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

namespace AffineTransformations3D
{
    class ZBuffer
    {
        public static Bitmap ZBufferAlgorithm(int width, int height, List<Polyhedron3D> polyhedron3Ds)
        {
            Bitmap result = new Bitmap(width, height);
            double[,] buff = new double[height, width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    result.SetPixel(i, j, Color.White);
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    buff[i, j] = double.MinValue;
            List<List<List<Point3D>>> rasterizedPolyhedrons = new List<List<List<Point3D>>>();
            foreach (var polyhedron in polyhedron3Ds)
                rasterizedPolyhedrons.Add(Rasterize(polyhedron, width, height));

            List<List<Color>> colors = new List<List<Color>>();
            foreach (var polyhedron in polyhedron3Ds)
                colors.Add(Colorize(polyhedron));

            Point3D center = new Point3D(width / 2, height / 2, 0);

            for (int i = 0; i < rasterizedPolyhedrons.Count; i++)
            {
                for (int j = 0; j < rasterizedPolyhedrons[i].Count; j++)
                    foreach (var point in rasterizedPolyhedrons[i][j])
                    {
                        int x = (int)(point.x + center.x);
                        int y = (int)(point.y + center.y);
                        if (x < width && x > 0 && y < height && y > 0)
                            if (point.z > buff[x, y])
                            {
                                buff[x, y] = point.z;
                                result.SetPixel(x, y, colors[i][j]);
                            }
                    }

            }

            return result;
        }
        public static List<Color> Colorize(Polyhedron3D polyhedron)
        {
            List<Color> colors = new List<Color>();
            Random randomGen = new Random();
            for (int i = 0; i < polyhedron.polygons.Count(); i++)
                colors.Add(Color.FromArgb(randomGen.Next(255), randomGen.Next(255), randomGen.Next(255)));
            return colors;
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
            var triangle = point3Ds.OrderBy(point => point.y).ToList();

            var x01s = Interpolate(triangle[0].y, triangle[0].x, triangle[1].y, triangle[1].x);
            var x12s = Interpolate(triangle[1].y, triangle[1].x, triangle[2].y, triangle[2].x);
            var x02s = Interpolate(triangle[0].y, triangle[0].x, triangle[2].y, triangle[2].x);

            var z01s = Interpolate(triangle[0].y, triangle[0].z, triangle[1].y, triangle[1].z);
            var z12s = Interpolate(triangle[1].y, triangle[1].z, triangle[2].y, triangle[2].z);
            var z02s = Interpolate(triangle[0].y, triangle[0].z, triangle[2].y, triangle[2].z);

            x01s.RemoveAt(x01s.Count - 1);
            z01s.RemoveAt(z01s.Count - 1);

            var x012s = x01s.Concat(x12s).ToList();
            var z012s = z01s.Concat(z12s).ToList();

            int middle = x012s.Count / 2;
            List<int> lX = x02s[middle] < x012s[middle] ? x02s : x012s,
                      rX = x02s[middle] < x012s[middle] ? x012s : x02s,
                      lZ = x02s[middle] < x012s[middle] ? z02s : z012s,
                      rZ = x02s[middle] < x012s[middle] ? z012s : z02s;

            int y0 = (int)triangle[0].y, y2 = (int)triangle[2].y;
            for (int i = 0; i <= y2 - y0; i++)
            {
                int curxL = lX[i], curxR = rX[i];
                var currZ = Interpolate(curxL, lZ[i], curxR, rZ[i]);
                for (int x = curxL; x < curxR; x++)
                    rasterizedTriangle.Add(new Point3D(x, (y0 + i), currZ[x - curxL]));
            }

            return rasterizedTriangle;
        }

        private static List<int> Interpolate(double cord1Start, double cord2Start, double cord1End, double cord2End)
        {
            int cord1StartI = (int)cord1Start, cord1EndI = (int)cord1End;
            if (cord1Start == cord1End)
                return new List<int> { (int)cord2Start };

            List<int> result = new List<int>();

            var slope = (cord2End - cord2Start) / (cord1End - cord1Start);
            double curx = cord2Start;
            for (int i = cord1StartI; i <= cord1EndI; i++)
            {
                result.Add((int)curx);
                curx += slope;
            }

            return result;
        }

    }
}
