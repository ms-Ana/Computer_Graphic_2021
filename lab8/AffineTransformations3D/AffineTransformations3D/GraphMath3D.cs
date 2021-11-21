using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AffineTransformations3D
{
    class GraphMath3D
    {
        public static double ScalarProduct(Point3D left, Point3D right) =>
         left.x * right.x + left.y * right.y + left.z * right.z;

        public static double VectorLength(Point3D vector) =>
            Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);

        public static double EuclideanDist(Point3D left, Point3D right) =>
        Math.Sqrt(Math.Pow(left.x - right.x, 2) + Math.Pow(left.y - right.y, 2));

        public static Point3D CrossProduct(Point3D vector01, Point3D vector02)
        {
            var x = vector01.y * vector02.z - vector01.z * vector02.y;
            var y = vector01.z * vector02.x - vector01.x * vector02.z;
            var z = vector01.x * vector02.y - vector01.y * vector02.x;
            return new Point3D(x, y, z);
        }


        public static double CosDist(Point3D left, Point3D right)
        {
            var scalarProduct = ScalarProduct(left, right);
            var lengthProduct = VectorLength(left) * VectorLength(right);
            return scalarProduct / lengthProduct;
        }

        public static Point3D CalculateNormal(Polygon3D polygon)
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
            return new Point3D(nx, ny, nz);
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
            return Math.Sin(x) * Math.Cos(y);
        }

        public static double Square(double x, double y)
        {
            return x * x + y * y;
        }

        public static List<List<Point3D>> Triangulate(List<Point3D> polygonPoints)
        {
            if (polygonPoints.Count == 3)
                return new List<List<Point3D>> { polygonPoints };
            List<List<Point3D>> trianglePoints = new List<List<Point3D>>();
            for (int i = 2; i < polygonPoints.Count; i++)
                trianglePoints.Add(new List<Point3D> { polygonPoints[0], polygonPoints[i - 1], polygonPoints[i] });
            return trianglePoints;
        }
        public static List<Point3D> PolygonToPoints(Polygon3D polygon)
        {
            List<Point3D> resultPoints = new List<Point3D>();
            foreach (var line in polygon.lines)
                resultPoints.Add(line.first);
            return resultPoints;
        }
        public static List<List<Point3D>> Rasterize(Polyhedron3D polyhedron, int width, int height)
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
