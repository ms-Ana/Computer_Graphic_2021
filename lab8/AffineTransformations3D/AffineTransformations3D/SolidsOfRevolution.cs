using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AffineTransformations3D
{
    class SolidsOfRevolution
    {
        public static int rotations = 10;
        public static List<Point> points;

        public static Polyhedron3D CreateParallelToXAxis()
        {
            fixPoints();
            Point3DWithTexture pFirst = new Point3DWithTexture(points[1].Y, 0, 0);
            Point3DWithTexture pLast = new Point3DWithTexture(points[0].Y, 0, 0);

            double angle = 360.0 / rotations;
            double rad = (Math.PI / 180) * angle;

            List<double> x = new List<double>();
            List<double> y = new List<double>();
            List<double> z = new List<double>();
            for (int i = 0; i < points.Count - 2; i++)
            {
                x.Add(points[i + 2].Y);
                y.Add(points[i + 2].X);
                z.Add(0);
            }

            List<double> newY = new List<double>();
            List<double> newZ = new List<double>();
            List<Polygon3D> sides = new List<Polygon3D>();

            for (int i = 0; i < rotations - 1; i++)
            {
                for (int j = 0; j < points.Count - 2; j++)
                {
                    newY.Add(y[j] * Math.Cos(rad) - z[j] * Math.Sin(rad));
                    newZ.Add(y[j] * Math.Sin(rad) + z[j] * Math.Cos(rad));
                }

                Point3DWithTexture p1 = new Point3DWithTexture(x[0], newY[0], newZ[0]);
                Point3DWithTexture p2 = new Point3DWithTexture(x[0], y[0], z[0]);
                Line3D l1 = new Line3D(pFirst, p1);
                Line3D l2 = new Line3D(p1, p2);
                Line3D l3 = new Line3D(p2, pFirst);
                Polygon3D side = new Polygon3D(new List<Line3D>() { l1, l2, l3 });
                sides.Add(side);

                for (int j = 0; j < points.Count - 3; j++)
                {
                    Point3DWithTexture point1 = new Point3DWithTexture(x[j], newY[j], newZ[j]);
                    Point3DWithTexture point2 = new Point3DWithTexture(x[j + 1], newY[j + 1], newZ[j + 1]);
                    Point3DWithTexture point3 = new Point3DWithTexture(x[j], y[j], z[j]);
                    Point3DWithTexture point4 = new Point3DWithTexture(x[j + 1], y[j + 1], z[j + 1]);
                    Line3D line1 = new Line3D(point1, point2);
                    Line3D line2 = new Line3D(point2, point4);
                    Line3D line3 = new Line3D(point4, point3);
                    Line3D line4 = new Line3D(point3, point1);
                    Polygon3D side1 = new Polygon3D(new List<Line3D>() { line1, line2, line3, line4 });
                    sides.Add(side1);
                }

                p1 = new Point3DWithTexture(x[x.Count - 1], newY[newY.Count - 1], newZ[newZ.Count - 1]);
                p2 = new Point3DWithTexture(x[x.Count - 1], y[y.Count - 1], z[z.Count - 1]);
                l1 = new Line3D(p1, pLast);
                l2 = new Line3D(pLast, p2);
                l3 = new Line3D(p2, p1);
                side = new Polygon3D(new List<Line3D>() { l1, l2, l3 });
                sides.Add(side);

                for (int j = 0; j < points.Count - 2; j++)
                {
                    y[j] = newY[j];
                    z[j] = newZ[j];
                }
                newY.Clear();
                newZ.Clear();
            }

            Point3DWithTexture lastPoint1 = new Point3DWithTexture(points[2].Y, points[2].X, 0);
            Point3DWithTexture lastPoint2 = new Point3DWithTexture(x[0], y[0], z[0]);
            Line3D lastLine1 = new Line3D(pFirst, lastPoint1);
            Line3D lastLine2 = new Line3D(lastPoint1, lastPoint2);
            Line3D lastLine3 = new Line3D(lastPoint2, pFirst);
            Polygon3D lastSide = new Polygon3D(new List<Line3D>() { lastLine1, lastLine2, lastLine3 });
            sides.Add(lastSide);

            for (int j = 0; j < points.Count - 3; j++)
            {
                Point3DWithTexture point1 = new Point3DWithTexture(points[j + 2].Y, points[j + 2].X, 0);
                Point3DWithTexture point2 = new Point3DWithTexture(points[j + 3].Y, points[j + 3].X, 0);
                Point3DWithTexture point3 = new Point3DWithTexture(x[j], y[j], z[j]);
                Point3DWithTexture point4 = new Point3DWithTexture(x[j + 1], y[j + 1], z[j + 1]);
                Line3D line1 = new Line3D(point1, point2);
                Line3D line2 = new Line3D(point2, point4);
                Line3D line3 = new Line3D(point4, point3);
                Line3D line4 = new Line3D(point3, point1);
                Polygon3D side1 = new Polygon3D(new List<Line3D>() { line1, line2, line3, line4 });
                sides.Add(side1);
            }

            lastPoint1 = new Point3DWithTexture(points[points.Count - 1].Y, points[points.Count - 1].X, 0);
            lastPoint2 = new Point3DWithTexture(x[x.Count - 1], y[y.Count - 1], z[z.Count - 1]);
            lastLine1 = new Line3D(lastPoint1, pLast);
            lastLine2 = new Line3D(pLast, lastPoint2);
            lastLine3 = new Line3D(lastPoint2, lastPoint1);
            lastSide = new Polygon3D(new List<Line3D>() { lastLine1, lastLine2, lastLine3 });
            sides.Add(lastSide);
            return new Polyhedron3D(sides);
        }

        public static Polyhedron3D CreateParallelToYAxis()
        {
            fixPoints();
            Point3DWithTexture pFirst = new Point3DWithTexture(0, points[1].Y, 0);
            Point3DWithTexture pLast = new Point3DWithTexture(0, points[0].Y, 0);

            double angle = 360.0 / rotations;
            double rad = (Math.PI / 180) * angle;

            List<double> x = new List<double>();
            List<double> y = new List<double>();
            List<double> z = new List<double>();
            for (int i = 0; i < points.Count - 2; i++)
            {
                x.Add(points[i + 2].X);
                y.Add(points[i + 2].Y);
                z.Add(0);
            }

            List<double> newX = new List<double>();
            List<double> newZ = new List<double>();
            List<Polygon3D> sides = new List<Polygon3D>();

            for (int i = 0; i < rotations - 1; i++)
            {
                for (int j = 0; j < points.Count - 2; j++)
                {
                    newX.Add(x[j] * Math.Cos(rad) - z[j] * Math.Sin(rad));
                    newZ.Add(x[j] * Math.Sin(rad) + z[j] * Math.Cos(rad));
                }

                Point3DWithTexture p1 = new Point3DWithTexture(newX[0], y[0], newZ[0]);
                Point3DWithTexture p2 = new Point3DWithTexture(x[0], y[0], z[0]);
                Line3D l1 = new Line3D(pFirst, p1);
                Line3D l2 = new Line3D(p1, p2);
                Line3D l3 = new Line3D(p2, pFirst);
                Polygon3D side = new Polygon3D(new List<Line3D>() { l1, l2, l3 });
                sides.Add(side);

                for (int j = 0; j < points.Count - 3; j++)
                {
                    Point3DWithTexture point1 = new Point3DWithTexture(newX[j], y[j], newZ[j]);
                    Point3DWithTexture point2 = new Point3DWithTexture(newX[j + 1], y[j + 1], newZ[j + 1]);
                    Point3DWithTexture point3 = new Point3DWithTexture(x[j], y[j], z[j]);
                    Point3DWithTexture point4 = new Point3DWithTexture(x[j + 1], y[j + 1], z[j + 1]);
                    Line3D line1 = new Line3D(point1, point2);
                    Line3D line2 = new Line3D(point2, point4);
                    Line3D line3 = new Line3D(point4, point3);
                    Line3D line4 = new Line3D(point3, point1);
                    Polygon3D side1 = new Polygon3D(new List<Line3D>() { line1, line2, line3, line4 });
                    sides.Add(side1);
                }

                p1 = new Point3DWithTexture(newX[newX.Count - 1], y[y.Count - 1], newZ[newZ.Count - 1]);
                p2 = new Point3DWithTexture(x[x.Count - 1], y[y.Count - 1], z[z.Count - 1]);
                l1 = new Line3D(p1, pLast);
                l2 = new Line3D(pLast, p2);
                l3 = new Line3D(p2, p1);
                side = new Polygon3D(new List<Line3D>() { l1, l2, l3 });
                sides.Add(side);

                for (int j = 0; j < points.Count - 2; j++)
                {
                    x[j] = newX[j];
                    z[j] = newZ[j];
                }
                newX.Clear();
                newZ.Clear();
            }

            Point3DWithTexture lastPoint1 = new Point3DWithTexture(points[2].X, points[2].Y, 0);
            Point3DWithTexture lastPoint2 = new Point3DWithTexture(x[0], y[0], z[0]);
            Line3D lastLine1 = new Line3D(pFirst, lastPoint1);
            Line3D lastLine2 = new Line3D(lastPoint1, lastPoint2);
            Line3D lastLine3 = new Line3D(lastPoint2, pFirst);
            Polygon3D lastSide = new Polygon3D(new List<Line3D>() { lastLine1, lastLine2, lastLine3 });
            sides.Add(lastSide);

            for (int j = 0; j < points.Count - 3; j++)
            {
                Point3DWithTexture point1 = new Point3DWithTexture(points[j + 2].X, points[j + 2].Y, 0);
                Point3DWithTexture point2 = new Point3DWithTexture(points[j + 3].X, points[j + 3].Y, 0);
                Point3DWithTexture point3 = new Point3DWithTexture(x[j], y[j], z[j]);
                Point3DWithTexture point4 = new Point3DWithTexture(x[j + 1], y[j + 1], z[j + 1]);
                Line3D line1 = new Line3D(point1, point2);
                Line3D line2 = new Line3D(point2, point4);
                Line3D line3 = new Line3D(point4, point3);
                Line3D line4 = new Line3D(point3, point1);
                Polygon3D side1 = new Polygon3D(new List<Line3D>() { line1, line2, line3, line4 });
                sides.Add(side1);
            }

            lastPoint1 = new Point3DWithTexture(points[points.Count - 1].X, points[points.Count - 1].Y, 0);
            lastPoint2 = new Point3DWithTexture(x[x.Count - 1], y[y.Count - 1], z[z.Count - 1]);
            lastLine1 = new Line3D(lastPoint1, pLast);
            lastLine2 = new Line3D(pLast, lastPoint2);
            lastLine3 = new Line3D(lastPoint2, lastPoint1);
            lastSide = new Polygon3D(new List<Line3D>() { lastLine1, lastLine2, lastLine3 });
            sides.Add(lastSide);
            return new Polyhedron3D(sides);
        }

        public static Polyhedron3D CreateParallelToZAxis()
        {
            fixPoints();
            Point3DWithTexture pFirst = new Point3DWithTexture(0, 0, points[1].Y);
            Point3DWithTexture pLast = new Point3DWithTexture(0, 0, points[0].Y);

            double angle = 360.0 / rotations;
            double rad = (Math.PI / 180) * angle;

            List<double> x = new List<double>();
            List<double> y = new List<double>();
            List<double> z = new List<double>();
            for (int i = 0; i < points.Count - 2; i++)
            {
                x.Add(points[i + 2].X);
                y.Add(0);
                z.Add(points[i + 2].Y);
            }

            List<double> newX = new List<double>();
            List<double> newY = new List<double>();
            List<Polygon3D> sides = new List<Polygon3D>();

            for (int i = 0; i < rotations - 1; i++)
            {
                for (int j = 0; j < points.Count - 2; j++)
                {
                    newX.Add(x[j] * Math.Cos(rad) - y[j] * Math.Sin(rad));
                    newY.Add(x[j] * Math.Sin(rad) + y[j] * Math.Cos(rad));
                }

                Point3DWithTexture p1 = new Point3DWithTexture(newX[0], newY[0], z[0]);
                Point3DWithTexture p2 = new Point3DWithTexture(x[0], y[0], z[0]);
                Line3D l1 = new Line3D(pFirst, p1);
                Line3D l2 = new Line3D(p1, p2);
                Line3D l3 = new Line3D(p2, pFirst);
                Polygon3D side = new Polygon3D(new List<Line3D>() { l1, l2, l3 });
                sides.Add(side);

                for (int j = 0; j < points.Count - 3; j++)
                {
                    Point3DWithTexture point1 = new Point3DWithTexture(newX[j], newY[j], z[j]);
                    Point3DWithTexture point2 = new Point3DWithTexture(newX[j + 1], newY[j + 1], z[j + 1]);
                    Point3DWithTexture point3 = new Point3DWithTexture(x[j], y[j], z[j]);
                    Point3DWithTexture point4 = new Point3DWithTexture(x[j + 1], y[j + 1], z[j + 1]);
                    Line3D line1 = new Line3D(point1, point2);
                    Line3D line2 = new Line3D(point2, point4);
                    Line3D line3 = new Line3D(point4, point3);
                    Line3D line4 = new Line3D(point3, point1);
                    Polygon3D side1 = new Polygon3D(new List<Line3D>() { line1, line2, line3, line4 });
                    sides.Add(side1);
                }

                p1 = new Point3DWithTexture(newX[newX.Count - 1], newY[newY.Count - 1], z[z.Count - 1]);
                p2 = new Point3DWithTexture(x[x.Count - 1], y[y.Count - 1], z[z.Count - 1]);
                l1 = new Line3D(p1, pLast);
                l2 = new Line3D(pLast, p2);
                l3 = new Line3D(p2, p1);
                side = new Polygon3D(new List<Line3D>() { l1, l2, l3 });
                sides.Add(side);

                for (int j = 0; j < points.Count - 2; j++)
                {
                    x[j] = newX[j];
                    y[j] = newY[j];
                }
                newX.Clear();
                newY.Clear();
            }

            Point3DWithTexture lastPoint1 = new Point3DWithTexture(points[2].X, 0, points[2].Y);
            Point3DWithTexture lastPoint2 = new Point3DWithTexture(x[0], y[0], z[0]);
            Line3D lastLine1 = new Line3D(pFirst, lastPoint1);
            Line3D lastLine2 = new Line3D(lastPoint1, lastPoint2);
            Line3D lastLine3 = new Line3D(lastPoint2, pFirst);
            Polygon3D lastSide = new Polygon3D(new List<Line3D>() { lastLine1, lastLine2, lastLine3 });
            sides.Add(lastSide);

            for (int j = 0; j < points.Count - 3; j++)
            {
                Point3DWithTexture point1 = new Point3DWithTexture(points[j + 2].X, 0, points[j + 2].Y);
                Point3DWithTexture point2 = new Point3DWithTexture(points[j + 3].X, 0, points[j + 3].Y);
                Point3DWithTexture point3 = new Point3DWithTexture(x[j], y[j], z[j]);
                Point3DWithTexture point4 = new Point3DWithTexture(x[j + 1], y[j + 1], z[j + 1]);
                Line3D line1 = new Line3D(point1, point2);
                Line3D line2 = new Line3D(point2, point4);
                Line3D line3 = new Line3D(point4, point3);
                Line3D line4 = new Line3D(point3, point1);
                Polygon3D side1 = new Polygon3D(new List<Line3D>() { line1, line2, line3, line4 });
                sides.Add(side1);
            }

            lastPoint1 = new Point3DWithTexture(points[points.Count - 1].X, 0, points[points.Count - 1].Y);
            lastPoint2 = new Point3DWithTexture(x[x.Count - 1], y[y.Count - 1], z[z.Count - 1]);
            lastLine1 = new Line3D(lastPoint1, pLast);
            lastLine2 = new Line3D(pLast, lastPoint2);
            lastLine3 = new Line3D(lastPoint2, lastPoint1);
            lastSide = new Polygon3D(new List<Line3D>() { lastLine1, lastLine2, lastLine3 });
            sides.Add(lastSide);
            return new Polyhedron3D(sides);
        }

        private static void fixPoints()
        {
            List<Point> newPoints = new List<Point>();
            newPoints.Add(new Point(0, 0));
            newPoints.Add(new Point(0, points[1].Y - points[0].Y));

            for (int i = 2; i < points.Count; i++)
            {
                newPoints.Add(new Point(points[i].X - points[0].X, points[i].Y - points[0].Y));
            }

            points.Clear();
            points.AddRange(newPoints);
        }
    }
}
