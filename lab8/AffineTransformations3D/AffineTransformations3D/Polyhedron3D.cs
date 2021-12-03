using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AffineTransformations3D
{
    class Polyhedron3D
    {
        public List<Polygon3D> polygons { get; set; }

        public Polyhedron3D(List<Polygon3D> p)
        {
            polygons = new List<Polygon3D>();
            polygons.AddRange(p);
        }

        public Polyhedron3D()
        {
            polygons = new List<Polygon3D>();
        }


        public Polyhedron3D Axonometric()
        {
            int angle = 30;
            Polyhedron3D res = new Polyhedron3D();
            double rad = (Math.PI / 180) * angle;

            foreach (Polygon3D p in polygons)
            {
                List<Line3D> lines = new List<Line3D>();
                foreach (Line3D l in p.lines)
                {
                    Point3DWithTexture p1 = l.first;
                    double x = p1.x;
                    double y = p1.y;
                    double z = p1.z;

                    double newX = (x - z) * Math.Cos(rad);
                    double newY = y + (x + z) * Math.Sin(rad);
                    Point3DWithTexture first = new Point3DWithTexture(newX, newY, z, p1.xTex, p1.yTex);

                    Point3DWithTexture p2 = l.second;
                    x = p2.x;
                    y = p2.y;
                    z = p2.z;

                    newX = (x - z) * Math.Cos(rad);
                    newY = y + (x + z) * Math.Sin(rad);
                    Point3DWithTexture second = new Point3DWithTexture(newX, newY, z, p2.xTex, p2.yTex);
                    Line3D line = new Line3D(first, second);
                    lines.Add(line);
                }
                res.polygons.Add(new Polygon3D(lines));
            }

            return res;
        }

        public Polyhedron3D Perspective()
        {
            Polyhedron3D res = new Polyhedron3D();

            foreach (Polygon3D p in polygons)
            {
                List<Line3D> lines = new List<Line3D>();
                foreach (Line3D l in p.lines)
                {
                    Point3DWithTexture p1 = l.first;
                    double x = p1.x;
                    double y = p1.y;
                    double z = p1.z;

                    //double r = -0.1;
                    double k = 1000;

                    double newX = (k * x) / (z + k);
                    double newY = (k * y) / (z + k);
                    Point3DWithTexture first = new Point3DWithTexture(newX, newY, p1.z, p1.xTex, p1.yTex);

                    Point3DWithTexture p2 = l.second;
                    x = p2.x;
                    y = p2.y;
                    z = p2.z;

                    newX = (k * x) / (z + k);
                    newY = (k * y) / (z + k);
                    Point3DWithTexture second = new Point3DWithTexture(newX, newY, p2.z, p2.xTex, p2.yTex);
                    Line3D line = new Line3D(first, second);
                    lines.Add(line);
                }
                res.polygons.Add(new Polygon3D(lines));
            }

            return res;
        }

        public Polyhedron3D Copy()
        {
            Polyhedron3D res = new Polyhedron3D();
            foreach (Polygon3D polygon in polygons)
            {
                List<Line3D> lines = new List<Line3D>();
                foreach (Line3D line in polygon.lines)
                {
                    double x = line.first.x;
                    double y = line.first.y;
                    double z = line.first.z;
                    double xTex = line.first.xTex;
                    double yTex = line.first.yTex;
                    Point3DWithTexture first = new Point3DWithTexture(x, y, z, xTex, yTex);

                    x = line.second.x;
                    y = line.second.y;
                    z = line.second.z;
                    xTex = line.second.xTex;
                    yTex = line.second.yTex;
                    Point3DWithTexture second = new Point3DWithTexture(x, y, z, xTex, yTex);
                    Line3D l = new Line3D(first, second);
                    lines.Add(l);
                }
                Polygon3D p = new Polygon3D(lines);
                res.polygons.Add(p);
            }
            return res;
        }

        public void FixPointsForBitmap()
        {
            foreach (Polygon3D polygon in polygons)
            {
                foreach (Line3D line in polygon.lines)
                {
                    double x = line.first.x;
                    double y = line.first.y;
                    double z = line.first.z;
                    line.first.x = -z;
                    line.first.y = -y;
                    line.first.z = -x;

                    x = line.second.x;
                    y = line.second.y;
                    z = line.second.z;
                    line.second.x = -z;
                    line.second.y = -y;
                    line.second.z = -x;
                }
            }
        }

        public Point3D Center()
        {
            int pointCnt = 0;
            double sumCoordX = 0, sumCoordY = 0, sumCoordZ = 0;
            foreach (var polygon in polygons)
                foreach (var line in polygon.lines)
                {
                    pointCnt += 2;
                    sumCoordX += line.first.x + line.second.x;
                    sumCoordY += line.first.y + line.second.y;
                    sumCoordZ += line.first.z + line.second.z;
                }
            return new Point3D(sumCoordX / pointCnt, sumCoordY / pointCnt, sumCoordZ / pointCnt);

        }
        private void ApplyTransformation(double[,] transformationMatrix)
        {
            for (int i = 0; i < polygons.Count; i++)
                for (int j = 0; j < polygons[i].lines.Count; j++)
                {
                    var firstPointMatrix = GraphMath3D.MatrixMultiplication(transformationMatrix,
                                              GraphMath3D.Point3DToMatix(polygons[i].lines[j].first));
                    var secondPointMatrix = GraphMath3D.MatrixMultiplication(transformationMatrix,
                                              GraphMath3D.Point3DToMatix(polygons[i].lines[j].second));
                    var firstPoint = new Point3DWithTexture(firstPointMatrix[0, 0] / firstPointMatrix[3, 0],
                                                 firstPointMatrix[1, 0] / firstPointMatrix[3, 0],
                                                 firstPointMatrix[2, 0] / firstPointMatrix[3, 0], 
                                                 polygons[i].lines[j].first.xTex, polygons[i].lines[j].first.yTex);
                    var secondPoint = new Point3DWithTexture(secondPointMatrix[0, 0] / secondPointMatrix[3, 0],
                                                 secondPointMatrix[1, 0] / secondPointMatrix[3, 0],
                                                 secondPointMatrix[2, 0] / secondPointMatrix[3, 0], 
                                                 polygons[i].lines[j].second.xTex, polygons[i].lines[j].second.yTex);
                    polygons[i].lines[j].first = firstPoint;
                    polygons[i].lines[j].second = secondPoint;
                }
        }

        public void Scale(double sx, double sy, double sz)
        {
            ApplyTransformation(GraphMath3D.ScaleMatrix(sx, sy, sz));
        }

        public void ScaleCenter(double sx, double sy, double sz)
        {
            var center = Center();
            ApplyTransformation(GraphMath3D.TranslationMatrix(-center.x, -center.y, -center.z));
            Scale(sx, sy, sz);
            ApplyTransformation(GraphMath3D.TranslationMatrix(center.x, center.y, center.z));
        }

        public void Translate(double dx, double dy, double dz)
        {
            ApplyTransformation(GraphMath3D.TranslationMatrix(dx, dy, dz));
        }

        public void Reflect(string axis)
        {
            ApplyTransformation(GraphMath3D.ReflectionMatrix(axis));
        }

        public void RotateCenter(double angleX, double angleY, double angleZ)
        {
            var center = Center();
            ApplyTransformation(GraphMath3D.TranslationMatrix(-center.x, -center.y, -center.z));
            ApplyTransformation(GraphMath3D.RotationMatrix(angleX, angleY, angleZ));
            ApplyTransformation(GraphMath3D.TranslationMatrix(center.x, center.y, center.z));
        }

        public void RotateLine(double angle, Line3D line)
        {
            var centerVector = line.second - line.first;
            var vectorLength = Math.Sqrt(centerVector.x * centerVector.x +
                                         centerVector.y * centerVector.y +
                                         centerVector.z * centerVector.z);
            double l = centerVector.x / vectorLength, m = centerVector.y / vectorLength,
                n = centerVector.z / vectorLength;
            angle *= Math.PI / 180;
            var sin = Math.Sin(angle);
            var cos = Math.Cos(angle);
            double[,] transformationMatrix = {{l*l+cos*(1-l*l),   l*(1-cos)*m-n*sin, l*(1-cos)*n+m*sin, 0},
                                              {l*(1-cos)*m+n*sin, m*m+cos*(1-m*m),   m*(1-cos)*n-l*sin, 0},
                                              {l*(1-cos)*n-m*sin, m*(1-cos)*n+l*sin, n*n+cos*(1-n*n),   0},
                                              {                0,                 0,                 0, 1}};
            ApplyTransformation(transformationMatrix);
        }

        public static Polyhedron3D Graphic(callable func, int countStep, double X0, double X1, double Y0, double Y1)
        {
            double stepX = (X1 - X0) / countStep, stepY = (Y1 - Y0) / countStep,
                   currentX = X0, currentY = Y0;
            List<Point3DWithTexture> points = new List<Point3DWithTexture>();
            for (int i = 0; i < countStep; i++)
            {
                currentX = X0;
                for (int j = 0; j < countStep; j++)
                {
                    points.Add(new Point3DWithTexture(currentX, currentY, func(currentX, currentY)));
                    currentX += stepX;
                }
                currentY += stepY;
            }

            List<Polygon3D> polygons = new List<Polygon3D>();

            for (int i = 0; i < countStep - 1; i++)
                for (int j = 0; j < countStep - 1; j++)
                {
                    Line3D l1 = new Line3D(points[i * (countStep) + j], points[i * (countStep) + j + 1]);
                    Line3D l2 = new Line3D(points[i * (countStep) + j + 1], points[(i + 1) * (countStep) + (j + 1)]);
                    Line3D l3 = new Line3D(points[(i + 1) * (countStep) + (j + 1)], points[(i + 1) * (countStep) + j]);
                    Line3D l4 = new Line3D(points[(i + 1) * (countStep) + j], points[i * (countStep) + j]);
                    polygons.Add(new Polygon3D(new List<Line3D> { l1, l2, l3, l4 }));
                }
            return new Polyhedron3D(polygons);
        }
    }
}
