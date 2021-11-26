using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;

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

        public static Dictionary<Point3D, List<int>> PolyhedronToPoints(Polyhedron3D polyhedron)
        {
            Dictionary<Point3D, List<int>> points2polygons = new Dictionary<Point3D, List<int>>();
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
                    rasterizedTriangle.Add(new Point3D(x, (y0 + i), currZ[x - curxL]));
            }

            return rasterizedTriangle;
        }

        public static List<List<Tuple<Point3D, double>>> RasterizeWithLight(Polyhedron3D polyhedron, int width, int height, Dictionary<Point3D, double> points2Lights)
        {
            List<List<Tuple<Point3D, double>>> rasterizedPolyhedron = new List<List<Tuple<Point3D, double>>>();
            foreach (var polygon in polyhedron.polygons)
            {
                List<Tuple<Point3D, double>> rasterizedPolygon = new List<Tuple<Point3D, double>>();
                var polygonPoints = PolygonToPoints(polygon);
                var triangles = Triangulate(polygonPoints);
                foreach (var triangle in triangles)
                    rasterizedPolygon.AddRange(RasterizeTriangleWithLight(triangle, width, height, points2Lights));
                rasterizedPolyhedron.Add(rasterizedPolygon);
            }

            return rasterizedPolyhedron;
        }

        private static List<Tuple<Point3D, double>> RasterizeTriangleWithLight(List<Point3D> point3Ds, int width, int height, Dictionary<Point3D, double> points2Lights)
        {
            List<Tuple<Point3D, double>> rasterizedTriangle = new List<Tuple<Point3D, double>>();
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
                    rasterizedTriangle.Add(Tuple.Create(new Point3D(x, y0 + i, currZ[x - curxL]), (double)currH[x -curxL]));
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

        public static List<List<Tuple<Point3D, Tuple<double, double>>>> RasterizeWithTexture(Polyhedron3D polyhedron)
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
                    rasterizedPolygon.AddRange(RasterizeTriangleWithTexture(triangle));
                rasterizedPolyhedron.Add(rasterizedPolygon);
            }

            return rasterizedPolyhedron;
        }

        private static List<Tuple<Point3D, Tuple<double, double>>> RasterizeTriangleWithTexture(List<Point3D> point3Ds)
        {
            List<Tuple<Point3D, Tuple<double, double>>> rasterizedTriangle = 
                new List<Tuple<Point3D, Tuple<double, double>>>();
            var triangle = point3Ds.OrderBy(point => point.y).ToList();

            var x01s = Interpolate(triangle[0].y, triangle[0].x, triangle[1].y, triangle[1].x);
            var x12s = Interpolate(triangle[1].y, triangle[1].x, triangle[2].y, triangle[2].x);
            var x02s = Interpolate(triangle[0].y, triangle[0].x, triangle[2].y, triangle[2].x);

            var z01s = Interpolate(triangle[0].y, triangle[0].z, triangle[1].y, triangle[1].z);
            var z12s = Interpolate(triangle[1].y, triangle[1].z, triangle[2].y, triangle[2].z);
            var z02s = Interpolate(triangle[0].y, triangle[0].z, triangle[2].y, triangle[2].z);

            /*var u01s = Interpolate(triangle[0].x, 0, triangle[1].x, 1);
            var u12s = Interpolate(triangle[1].x, 0, triangle[2].x, 1);
            var u02s = Interpolate(triangle[0].x, 0, triangle[2].x, 1);

            var v01s = Interpolate(triangle[0].y, 0, triangle[1].y, 1);
            var v12s = Interpolate(triangle[1].y, 0, triangle[2].y, 1);
            var v02s = Interpolate(triangle[0].y, 0, triangle[2].y, 1);*/

            x01s.RemoveAt(x01s.Count - 1);
            z01s.RemoveAt(z01s.Count - 1);
            /*if (u01s.Count > 0)
                u01s.RemoveAt(u01s.Count - 1);
            if (v01s.Count > 0)
                v01s.RemoveAt(v01s.Count - 1);*/

            var x012s = x01s.Concat(x12s).ToList();
            var z012s = z01s.Concat(z12s).ToList();
            /*var u012s = u01s.Concat(u12s).ToList();
            var v012s = v01s.Concat(v12s).ToList();*/

            int middle = x012s.Count / 2;
            List<double> lX = x02s[middle] < x012s[middle] ? x02s : x012s,
                      rX = x02s[middle] < x012s[middle] ? x012s : x02s,
                      lZ = x02s[middle] < x012s[middle] ? z02s : z012s,
                      rZ = x02s[middle] < x012s[middle] ? z012s : z02s;
                      /*lU = x02s[middle] < x012s[middle] ? u02s : u012s,
                      rU = x02s[middle] < x012s[middle] ? u012s : u02s,
                      lV = x02s[middle] < x012s[middle] ? v02s : v012s,
                      rV = x02s[middle] < x012s[middle] ? v012s : v02s;*/


            int y0 = (int)triangle[0].y, y2 = (int)triangle[2].y;
            for (int i = 0; i <= y2 - y0; i++)
            {
                int curxL = (int)lX[i], curxR = (int)rX[i];
                var currZ = Interpolate(curxL, lZ[i], curxR, rZ[i]);
                /*var currU = Interpolate(curxL, lU[i], curxR, i >= rU.Count ? rU[0] : rU[i]);
                var currV = Interpolate(curxL, lV[i], curxR, i >= rV.Count ? rV[0] : rV[i]);*/
                for (int x = curxL; x < curxR; x++)
                {
                    Point3D a = triangle[0];
                    Point3D e1 = triangle[1] - triangle[0];
                    Point3D e2 = triangle[2] - triangle[0];
                    Point3D n = CrossProduct(e1, e2);
                    Point3D m = CrossProduct(e2, a);
                    Point3D l = CrossProduct(a, e1);
                    double deltaL = curxL * n.x + y0 * n.y + n.z;
                    double uL = curxL * m.x + y0 * m.y + m.z;
                    double vL = curxL * l.x + y0 * l.y + l.z;
                    uL /= deltaL; vL /= deltaL;
                    double deltaR = curxR * n.x + y2 * n.y + n.z;
                    double uR = curxR * m.x + y2 * m.y + m.z;
                    double vR = curxR * l.x + y2 * l.y + l.z;
                    uR /= deltaR; vR /= deltaR;
                    double curxM = (curxL + curxR) / 2;
                    double yM = (y0 + y2) / 2;
                    double deltaM = curxM * n.x + yM * n.y + n.z;
                    double uM = curxM * m.x + yM * m.y + m.z;
                    double vM = curxM * l.x + yM * l.y + l.z;
                    uM /= deltaM; vM /= deltaM;

                    double k = uL + uR - 2 * uM;
                    double a2 = 2 * k / ((curxR - curxL) * (curxR - curxL));
                    double a1 = (uR - uL) / (curxR - curxL) - ((2 * k) / ((curxR - curxL) * (curxR - curxL))) * (curxR + curxL);
                    double a0 = uL - a1 * curxL - a2 * curxL * curxL;
                    double u = a0 + a1 * x + a2 * x * x;

                    k = vL + vR - 2 * vM;
                    a2 = 2 * k / ((y2 - y0) * (y2 - y0));
                    a1 = (vR - vL) / (y2 - y0) - ((2 * k) / ((y2 - y0) * (y2 - y0))) * (y2 + y0);
                    a0 = vL - a1 * y0 - a2 * y0 * y0;
                    double v = a0 + a1 * (y0 + i) + a2 * (y0 + i) * (y0 + i);

                    rasterizedTriangle.Add(Tuple.Create(new Point3D(x, y0 + i, currZ[x - curxL]), Tuple.Create(u, v)));
                }
            }

            return rasterizedTriangle;
        }
    }
}
