using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Diagnostics;

namespace AffineTransformations3D
{
    class GraphMath3D
    {
        public static double ScalarProduct(Point3DWithTexture left, Point3DWithTexture right) =>
         left.x * right.x + left.y * right.y + left.z * right.z;

        public static double VectorLength(Point3DWithTexture vector) =>
            Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);

        public static double EuclideanDist(Point3DWithTexture left, Point3DWithTexture right) =>
        Math.Sqrt(Math.Pow(left.x - right.x, 2) + Math.Pow(left.y - right.y, 2));

        public static Point3DWithTexture CrossProduct(Point3DWithTexture vector01, Point3DWithTexture vector02)
        {
            var x = vector01.y * vector02.z - vector01.z * vector02.y;
            var y = vector01.z * vector02.x - vector01.x * vector02.z;
            var z = vector01.x * vector02.y - vector01.y * vector02.x;
            return new Point3DWithTexture(x, y, z);
        }


        public static double CosDist(Point3DWithTexture left, Point3DWithTexture right)
        {
            var scalarProduct = ScalarProduct(left, right);
            var lengthProduct = VectorLength(left) * VectorLength(right);
            return scalarProduct / lengthProduct;
        }

        public static Point3DWithTexture CalculateNormal(Polygon3D polygon)
        {
            double ax = polygon.lines[0].second.x - polygon.lines[0].first.x;
            double ay = polygon.lines[0].second.y - polygon.lines[0].first.y;
            double az = polygon.lines[0].second.z - polygon.lines[0].first.z;

            double bx = polygon.lines[1].second.x - polygon.lines[1].first.x;
            double by = polygon.lines[1].second.y - polygon.lines[1].first.y;
            double bz = polygon.lines[1].second.z - polygon.lines[1].first.z;

            double nx = ay * bz - az * by;
            double ny = az * bx - ax * bz;
            double nz = ax * by - ay * bx;
            return new Point3DWithTexture(nx, ny, nz);
        }

        

        public static double[,] MatrixMultiplication(double[,] leftMatrix, double[,] rightMatrix)
        {
            double[,] resultMatrix = new double[leftMatrix.GetLength(0), rightMatrix.GetLength(1)];
            for (int i = 0; i < resultMatrix.GetLength(0); i++)
                for (int j = 0; j < resultMatrix.GetLength(1); j++)
                    for (int k = 0; k < leftMatrix.GetLength(0); k++)
                        resultMatrix[i, j] += leftMatrix[i, k] * rightMatrix[k, j];
            return resultMatrix;
        }
        public static double[,] TranslationMatrix(double dx, double dy, double dz)
        {
            return new double[,] {{1,0,0,dx},
                                  {0,1,0,dy},
                                  {0,0,1,dz},
                                  {0,0,0,1}};
        }
        public static double[,] ScaleMatrix(double sx, double sy, double sz)
        {
            return new double[,]  {{sx,0,0,0},
                                   {0,sy,0,0},
                                   {0,0,sz,0},
                                   {0,0,0,1}};
        }
        public static double[,] ReflectionMatrix(string axis)
        {
            switch (axis)
            {
                case "xy":
                    return new double[,]{{1,0,0,0},
                                         {0,1,0,0},
                                         {0,0,-1,0},
                                         {0,0,0,1} };
                case "xz":
                    return new double[,] {{1,0,0,0},
                                          {0,-1,0,0},
                                          {0,0,1,0},
                                          {0,0,0,1}};
                case "yz":
                    return new double[,] {{-1,0,0,0},
                                          {0,1,0,0},
                                          {0,0,1,0},
                                          {0,0,0,1}};
                default:
                    return null;
            }
        }
        public static double[,] RotationMatrix(double angleX, double angleY, double angleZ)
        {
            angleX *= Math.PI / 180;
            angleY *= Math.PI / 180;
            angleZ *= Math.PI / 180;
            var sin = Math.Sin(angleX);
            var cos = Math.Cos(angleX);
            double[,] rotationX = {{1,  0,   0, 0},
                                   {0,cos,-sin, 0},
                                   {0,sin, cos, 0},
                                   {0,  0,   0, 1}};
            sin = Math.Sin(angleY);
            cos = Math.Cos(angleY);
            double[,] rotationY = {{cos, 0, sin, 0},
                                   {  0, 1,   0, 0},
                                   {-sin,0, cos, 0},
                                   {  0, 0,   0, 1}};
            sin = Math.Sin(angleZ);
            cos = Math.Cos(angleZ);
            double[,] rotationZ = {{cos,-sin,0,0},
                                   {sin, cos,0,0},
                                   {  0,   0,1,0},
                                   {  0,   0,0,1}};
            return MatrixMultiplication(MatrixMultiplication(rotationX, rotationY), rotationZ);

        }
        public static double[,] Point3DToMatix(Point3D point)
        {
            return new double[,] {{ point.x },
                                  { point.y },
                                  { point.z },
                                  { 1 }};
        }
        public static double Max2D(double[,] matrix)
        {
            double max = double.MinValue;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] > max)
                        max = matrix[i, j];
            return max;
        }
        public static double Min2D(double[,] matrix)
        {
            double min = double.MaxValue;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] < min)
                        min = matrix[i, j];
            return min;
        }

        public static double SinCos(double x, double y)
        {
            return Math.Sin(x) * Math.Cos(y) * 10;
        }

        public static double Square(double x, double y)
        {
            return (x * x + y * y) / 100;
        }

        public static double FuncView(double x, double y)
        {
            return Math.Sin(x * x + y * y);
        }

        public static List<List<Point3DWithTexture>> Triangulate(List<Point3DWithTexture> polygonPoints)
        {
            if (polygonPoints.Count == 3)
                return new List<List<Point3DWithTexture>> { polygonPoints };
            List<List<Point3DWithTexture>> trianglePoints = new List<List<Point3DWithTexture>>();
            for (int i = 2; i < polygonPoints.Count; i++)
                trianglePoints.Add(new List<Point3DWithTexture> { polygonPoints[0], polygonPoints[i - 1], polygonPoints[i] });
            return trianglePoints;
        }
        public static List<Point3DWithTexture> PolygonToPoints(Polygon3D polygon)
        {
            List<Point3DWithTexture> resultPoints = new List<Point3DWithTexture>();
            foreach (var line in polygon.lines)
                resultPoints.Add(line.first);
            return resultPoints;
        }

        public static Dictionary<Point3DWithTexture, List<int>> PolyhedronToPoints(Polyhedron3D polyhedron)
        {
            Dictionary<Point3DWithTexture, List<int>> points2polygons = new Dictionary<Point3DWithTexture, List<int>>();
            for (int i = 0; i < polyhedron.polygons.Count; i++)
            {
                foreach (var line in polyhedron.polygons[i].lines)
                {
                    if (!points2polygons.ContainsKey(line.first))
                        points2polygons[line.first] = new List<int>();
                    points2polygons[line.first].Add(i);  
                }
            }
            return points2polygons;

        }
        public static List<List<Point3DWithTexture>> Rasterize(Polyhedron3D polyhedron, int width, int height)
        {
            List<List<Point3DWithTexture>> rasterizedPolyhedron = new List<List<Point3DWithTexture>>();
            foreach (var polygon in polyhedron.polygons)
            {
                List<Point3DWithTexture> rasterizedPolygon = new List<Point3DWithTexture>();
                var polygonPoints = PolygonToPoints(polygon);
                var triangles = Triangulate(polygonPoints);
                foreach (var triangle in triangles)
                    rasterizedPolygon.AddRange(RasterizeTriangle(triangle, width, height));
                rasterizedPolyhedron.Add(rasterizedPolygon);
            }

            return rasterizedPolyhedron;
        }

        private static List<Point3DWithTexture> RasterizeTriangle(List<Point3DWithTexture> point3Ds, int width, int height)
        {
            List<Point3DWithTexture> rasterizedTriangle = new List<Point3DWithTexture>();
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
            List<double> lX = x02s[middle] < x012s[middle] ? x02s : x012s,
                      rX = x02s[middle] < x012s[middle] ? x012s : x02s,
                      lZ = x02s[middle] < x012s[middle] ? z02s : z012s,
                      rZ = x02s[middle] < x012s[middle] ? z012s : z02s;

            int y0 = (int)triangle[0].y, y2 = (int)triangle[2].y;
            for (int i = 0; i <= y2 - y0; i++)
            {
                int curxL = (int)lX[i], curxR = (int)rX[i];
                var currZ = Interpolate(curxL, lZ[i], curxR, rZ[i]);
                for (int x = curxL; x < curxR; x++)
                    rasterizedTriangle.Add(new Point3DWithTexture(x, (y0 + i), currZ[x - curxL]));
            }

            return rasterizedTriangle;
        }

        public static List<List<Tuple<Point3DWithTexture, double>>> RasterizeWithLight(Polyhedron3D polyhedron, int width, int height, 
            Dictionary<Point3DWithTexture, double> points2Lights)
        {
            List<List<Tuple<Point3DWithTexture, double>>> rasterizedPolyhedron = new List<List<Tuple<Point3DWithTexture, double>>>();
            foreach (var polygon in polyhedron.polygons)
            {
                List<Tuple<Point3DWithTexture, double>> rasterizedPolygon = new List<Tuple<Point3DWithTexture, double>>();
                var polygonPoints = PolygonToPoints(polygon);
                var triangles = Triangulate(polygonPoints);
                foreach (var triangle in triangles)
                    rasterizedPolygon.AddRange(RasterizeTriangleWithLight(triangle, width, height, points2Lights));
                rasterizedPolyhedron.Add(rasterizedPolygon);
            }

            return rasterizedPolyhedron;
        }

        private static List<Tuple<Point3DWithTexture, double>> RasterizeTriangleWithLight(List<Point3DWithTexture> point3Ds, int width, int height,
            Dictionary<Point3DWithTexture, double> points2Lights)
        {
            List<Tuple<Point3DWithTexture, double>> rasterizedTriangle = new List<Tuple<Point3DWithTexture, double>>();
            var triangle = point3Ds.OrderBy(point => point.y).ToList();

            var x01s = Interpolate(triangle[0].y, triangle[0].x, triangle[1].y, triangle[1].x);
            var x12s = Interpolate(triangle[1].y, triangle[1].x, triangle[2].y, triangle[2].x);
            var x02s = Interpolate(triangle[0].y, triangle[0].x, triangle[2].y, triangle[2].x);

            var z01s = Interpolate(triangle[0].y, triangle[0].z, triangle[1].y, triangle[1].z);
            var z12s = Interpolate(triangle[1].y, triangle[1].z, triangle[2].y, triangle[2].z);
            var z02s = Interpolate(triangle[0].y, triangle[0].z, triangle[2].y, triangle[2].z);

            var h01s = Interpolate(triangle[0].y, points2Lights[triangle[0]], triangle[1].y, points2Lights[triangle[1]]);
            var h12s = Interpolate(triangle[1].y, points2Lights[triangle[1]], triangle[2].y, points2Lights[triangle[2]]);
            var h02s = Interpolate(triangle[0].y, points2Lights[triangle[0]], triangle[2].y, points2Lights[triangle[2]]);

            x01s.RemoveAt(x01s.Count - 1);
            z01s.RemoveAt(z01s.Count - 1);
            h01s.RemoveAt(h01s.Count - 1);

            var x012s = x01s.Concat(x12s).ToList();
            var z012s = z01s.Concat(z12s).ToList();
            var h012s = h01s.Concat(h12s).ToList();

            int middle = x012s.Count / 2;
            List<double> lX = x02s[middle] < x012s[middle] ? x02s : x012s,
                      rX = x02s[middle] < x012s[middle] ? x012s : x02s,
                      lZ = x02s[middle] < x012s[middle] ? z02s : z012s,
                      rZ = x02s[middle] < x012s[middle] ? z012s : z02s,
                      lH = x02s[middle] < x012s[middle] ? h02s : h012s,
                      rH = x02s[middle] < x012s[middle] ? h012s : h02s;


            int y0 = (int)triangle[0].y, y2 = (int)triangle[2].y;
            for (int i = 0; i <= y2 - y0; i++)
            {
                int curxL = (int)lX[i], curxR = (int)rX[i];
                var currZ = Interpolate(curxL, lZ[i], curxR, rZ[i]);
                var currH = Interpolate(curxL, lH[i], curxR, rH[i]);
                for (int x = curxL; x < curxR; x++)
                    rasterizedTriangle.Add(Tuple.Create(new Point3DWithTexture(x, y0 + i, currZ[x - curxL]), (double)currH[x -curxL]));
            }

            return rasterizedTriangle;
        }

        private static List<double> Interpolate(double cord1Start, double cord2Start, double cord1End, double cord2End)
        {
            int cord1StartI = (int)cord1Start, cord1EndI = (int)cord1End;
            if (cord1Start == cord1End)
                return new List<double> { (double)cord2Start };

            List<double> result = new List<double>();

            var slope = (cord2End - cord2Start) / (cord1End - cord1Start);
            double curx = cord2Start;
            for (int i = cord1StartI; i <= cord1EndI; i++)
            {
                result.Add(curx);
                curx += slope;
            }

            return result;
        }

        public static List<List<Tuple<Point3D, Tuple<double, double>>>> RasterizeWithTexture(Polyhedron3D polyhedron, Bitmap texture)
        {
            List<List<Tuple<Point3D, Tuple<double, double>>>> rasterizedPolyhedron = 
                new List<List<Tuple<Point3D, Tuple<double, double>>>>();
            foreach (var polygon in polyhedron.polygons)
            {
                List<Tuple<Point3D, Tuple<double, double>>> rasterizedPolygon = 
                    new List<Tuple<Point3D, Tuple<double, double>>>();
                var polygonPoints = PolygonToPoints(polygon);
                var triangles = Triangulate(polygonPoints);
                foreach (var triangle in triangles)
                    rasterizedPolygon.AddRange(RasterizeTriangleWithTexture(triangle, texture));
                rasterizedPolyhedron.Add(rasterizedPolygon);
            }

            return rasterizedPolyhedron;
        }

        private static List<Tuple<Point3D, Tuple<double, double>>> RasterizeTriangleWithTexture(List<Point3DWithTexture> point3Ds, Bitmap texture)
        {
            List<Tuple<Point3D, Tuple<double, double>>> rasterizedTriangle = 
                new List<Tuple<Point3D, Tuple<double, double>>>();
            var triangle = point3Ds.OrderBy(point => point.y).ToList();

            double x0 = triangle[0].xTex * (texture.Width - 1); double y0 = triangle[0].yTex * (texture.Height - 1);
            double x1 = triangle[1].xTex * (texture.Width - 1); double y1 = triangle[1].yTex * (texture.Height - 1);
            double x2 = triangle[2].xTex * (texture.Width - 1); double y2 = triangle[2].yTex * (texture.Height - 1);

            var x01s = Interpolate(triangle[0].y, triangle[0].x, triangle[1].y, triangle[1].x);
            var x12s = Interpolate(triangle[1].y, triangle[1].x, triangle[2].y, triangle[2].x);
            var x02s = Interpolate(triangle[0].y, triangle[0].x, triangle[2].y, triangle[2].x);

            var z01s = Interpolate(triangle[0].y, triangle[0].z, triangle[1].y, triangle[1].z);
            var z12s = Interpolate(triangle[1].y, triangle[1].z, triangle[2].y, triangle[2].z);
            var z02s = Interpolate(triangle[0].y, triangle[0].z, triangle[2].y, triangle[2].z);

            var xTex01s = Interpolate(triangle[0].y, x0, triangle[1].y, x1);
            var xTex12s = Interpolate(triangle[1].y, x1, triangle[2].y, x2);
            var xTex02s = Interpolate(triangle[0].y, x0, triangle[2].y, x2);

            var yTex01s = Interpolate(triangle[0].y, y0, triangle[1].y, y1);
            var yTex12s = Interpolate(triangle[1].y, y1, triangle[2].y, y2);
            var yTex02s = Interpolate(triangle[0].y, y0, triangle[2].y, y2);

            x01s.RemoveAt(x01s.Count - 1);
            z01s.RemoveAt(z01s.Count - 1);
            xTex01s.RemoveAt(xTex01s.Count - 1);
            yTex01s.RemoveAt(yTex01s.Count - 1);

            var x012s = x01s.Concat(x12s).ToList();
            var z012s = z01s.Concat(z12s).ToList();
            var xTex012s = xTex01s.Concat(xTex12s).ToList();
            var yTex012s = yTex01s.Concat(yTex12s).ToList();

            int middle = x012s.Count / 2;
            List<double> lX = x02s[middle] < x012s[middle] ? x02s : x012s,
                      rX = x02s[middle] < x012s[middle] ? x012s : x02s,
                      lZ = x02s[middle] < x012s[middle] ? z02s : z012s,
                      rZ = x02s[middle] < x012s[middle] ? z012s : z02s,
                      lXTex = x02s[middle] < x012s[middle] ? xTex02s : xTex012s,
                      rXTex = x02s[middle] < x012s[middle] ? xTex012s : xTex02s,
                      lYTex = x02s[middle] < x012s[middle] ? yTex02s : yTex012s,
                      rYTex = x02s[middle] < x012s[middle] ? yTex012s : yTex02s;


            int yFirst = (int)triangle[0].y, yLast = (int)triangle[2].y;
            for (int i = 0; i <= yLast - yFirst; i++)
            {
                int curxL = (int)lX[i], curxR = (int)rX[i];
                var currZ = Interpolate(curxL, lZ[i], curxR, rZ[i]);
                var currTX = Interpolate(curxL, lXTex[i], curxR, rXTex[i]);
                var currTY = Interpolate(curxL, lYTex[i], curxR, rYTex[i]);
                for (int x = curxL; x < curxR; x++)
                {
                    rasterizedTriangle.Add(Tuple.Create(new Point3D(x, yFirst + i, currZ[x - curxL]), 
                        Tuple.Create(currTX[x - curxL], currTY[x - curxL])));
                }
            }

            return rasterizedTriangle;
        }


    }
}
