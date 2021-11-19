using System;
using System.Collections.Generic;
using System.Text;

namespace AffineTransformations3D
{
    class Figures
    {
        public static Polyhedron3D Hexahedron(int scale)
        {
            Point3D p1 = new Point3D(0, 0, 0);
            Point3D p2 = new Point3D(scale, 0, 0);
            Point3D p3 = new Point3D(0, scale, 0);
            Point3D p4 = new Point3D(0, 0, scale);
            Point3D p5 = new Point3D(scale, scale, 0);
            Point3D p6 = new Point3D(scale, 0, scale);
            Point3D p7 = new Point3D(0, scale, scale);
            Point3D p8 = new Point3D(scale, scale, scale);

            Line3D l1 = new Line3D(p6, p8);
            Line3D l2 = new Line3D(p8, p7);
            Line3D l3 = new Line3D(p7, p4);
            Line3D l4 = new Line3D(p4, p6);
            Polygon3D side1 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4 });

            l1 = new Line3D(p2, p6);
            l2 = new Line3D(p6, p4);
            l3 = new Line3D(p4, p1);
            l4 = new Line3D(p1, p2);
            Polygon3D side2 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4 });

            l1 = new Line3D(p8, p6);
            l2 = new Line3D(p6, p2);
            l3 = new Line3D(p2, p5);
            l4 = new Line3D(p5, p8);
            Polygon3D side3 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4 });

            l1 = new Line3D(p5, p2);
            l2 = new Line3D(p2, p1);
            l3 = new Line3D(p1, p3);
            l4 = new Line3D(p3, p5);
            Polygon3D side4 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4 });

            l1 = new Line3D(p7, p8);
            l2 = new Line3D(p8, p5);
            l3 = new Line3D(p5, p3);
            l4 = new Line3D(p3, p7);
            Polygon3D side5 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4 });

            l1 = new Line3D(p4, p7);
            l2 = new Line3D(p7, p3);
            l3 = new Line3D(p3, p1);
            l4 = new Line3D(p1, p4);
            Polygon3D side6 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4 });

            Polyhedron3D p = new Polyhedron3D(new List<Polygon3D> { side1, side2, side3, side4, side5, side6 });
            return p;
        }

        public static Polyhedron3D Tetrahedron(int scale)
        {
            Point3D p1 = new Point3D(0, 0, 0);
            Point3D p2 = new Point3D(scale, 0, 0);
            Point3D p3 = new Point3D(0, scale, 0);
            Point3D p4 = new Point3D(0, 0, scale);
            Point3D p8 = new Point3D(scale, scale, scale);

            Line3D l1 = new Line3D(p2, p8);
            Line3D l2 = new Line3D(p8, p4);
            Line3D l3 = new Line3D(p4, p2);
            Polygon3D side1 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p8, p2);
            l2 = new Line3D(p2, p3);
            l3 = new Line3D(p3, p8);
            Polygon3D side2 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p3, p4);
            l2 = new Line3D(p4, p8);
            l3 = new Line3D(p8, p3);
            Polygon3D side3 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p4, p3);
            l2 = new Line3D(p3, p2);
            l3 = new Line3D(p2, p4);
            Polygon3D side4 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            Polyhedron3D p = new Polyhedron3D(new List<Polygon3D> { side1, side2, side3, side4 });
            return p;
        }

        public static Polyhedron3D Octahedron(int scale)
        {
            Point3D p1 = new Point3D(scale, scale, scale * 2);
            Point3D p2 = new Point3D(scale, 0, scale);
            Point3D p3 = new Point3D(scale * 2, scale, scale);
            Point3D p4 = new Point3D(0, scale, scale);
            Point3D p5 = new Point3D(scale, scale, 0);
            Point3D p6 = new Point3D(scale, scale * 2, scale);

            Line3D l1 = new Line3D(p1, p2);
            Line3D l2 = new Line3D(p2, p3);
            Line3D l3 = new Line3D(p3, p1);
            Polygon3D side1 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p1, p4);
            l2 = new Line3D(p4, p2);
            l3 = new Line3D(p2, p1);
            Polygon3D side2 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p1, p6);
            l2 = new Line3D(p6, p4);
            l3 = new Line3D(p4, p1);
            Polygon3D side3 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p1, p3);
            l2 = new Line3D(p3, p6);
            l3 = new Line3D(p6, p1);
            Polygon3D side4 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p3, p2);
            l2 = new Line3D(p2, p5);
            l3 = new Line3D(p5, p3);
            Polygon3D side5 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p2, p4);
            l2 = new Line3D(p4, p5);
            l3 = new Line3D(p5, p2);
            Polygon3D side6 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p4, p6);
            l2 = new Line3D(p6, p5);
            l3 = new Line3D(p5, p4);
            Polygon3D side7 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p6, p3);
            l2 = new Line3D(p3, p5);
            l3 = new Line3D(p5, p6);
            Polygon3D side8 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            Polyhedron3D p = new Polyhedron3D(new List<Polygon3D> { side1, side2, side3, side4, side5, side6, side7, side8 });
            return p;
        }

        public static Polyhedron3D Icosahedron(int scale)
        {
            double t = scale * Math.Sqrt((5 - Math.Sqrt(5)) / 2);
            double h = Math.Sqrt(t * t - t * t / 4);
            double h2 = Math.Sqrt(scale * scale - t * t / 4);
            double val = Math.Sqrt(h * h - h2 * h2);
            Point3D p1 = new Point3D(0, 0, 0);
            double z = val;

            Point3D p2 = new Point3D(-scale, 0, z);
            double rad = (Math.PI / 180) * 72;
            double x = -scale;
            double y = 0.0;
            double newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            double newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p3 = new Point3D(newX, newY, z);
            x = newX; y = newY;

            newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p4 = new Point3D(newX, newY, z);
            x = newX; y = newY;

            newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p5 = new Point3D(newX, newY, z);
            x = newX; y = newY;

            newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p6 = new Point3D(newX, newY, z);

            z += h;
            Point3D p7 = new Point3D(scale, 0, z);
            x = scale;
            y = 0.0;
            newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p8 = new Point3D(newX, newY, z);
            x = newX; y = newY;

            newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p9 = new Point3D(newX, newY, z);
            x = newX; y = newY;

            newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p10 = new Point3D(newX, newY, z);
            x = newX; y = newY;

            newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p11 = new Point3D(newX, newY, z);

            z += val;
            Point3D p12 = new Point3D(0, 0, z);

            Line3D l1 = new Line3D(p1, p4);
            Line3D l2 = new Line3D(p4, p5);
            Line3D l3 = new Line3D(p5, p1);
            Polygon3D side1 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p1, p5);
            l2 = new Line3D(p5, p6);
            l3 = new Line3D(p6, p1);
            Polygon3D side2 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p1, p6);
            l2 = new Line3D(p6, p2);
            l3 = new Line3D(p2, p1);
            Polygon3D side3 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p1, p2);
            l2 = new Line3D(p2, p3);
            l3 = new Line3D(p3, p1);
            Polygon3D side4 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p1, p3);
            l2 = new Line3D(p3, p4);
            l3 = new Line3D(p4, p1);
            Polygon3D side5 = new Polygon3D(new List<Line3D> { l1, l2, l3 });


            l1 = new Line3D(p7, p5);
            l2 = new Line3D(p5, p4);
            l3 = new Line3D(p4, p7);
            Polygon3D side6 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p7, p8);
            l2 = new Line3D(p8, p5);
            l3 = new Line3D(p5, p7);
            Polygon3D side7 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p8, p6);
            l2 = new Line3D(p6, p5);
            l3 = new Line3D(p5, p8);
            Polygon3D side8 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p8, p9);
            l2 = new Line3D(p9, p6);
            l3 = new Line3D(p6, p8);
            Polygon3D side9 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p9, p2);
            l2 = new Line3D(p2, p6);
            l3 = new Line3D(p6, p9);
            Polygon3D side10 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p9, p10);
            l2 = new Line3D(p10, p2);
            l3 = new Line3D(p2, p9);
            Polygon3D side11 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p10, p3);
            l2 = new Line3D(p3, p2);
            l3 = new Line3D(p2, p10);
            Polygon3D side12 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p10, p11);
            l2 = new Line3D(p11, p3);
            l3 = new Line3D(p3, p10);
            Polygon3D side13 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p11, p4);
            l2 = new Line3D(p4, p3);
            l3 = new Line3D(p3, p11);
            Polygon3D side14 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p11, p7);
            l2 = new Line3D(p7, p4);
            l3 = new Line3D(p4, p11);
            Polygon3D side15 = new Polygon3D(new List<Line3D> { l1, l2, l3 });


            l1 = new Line3D(p12, p8);
            l2 = new Line3D(p8, p7);
            l3 = new Line3D(p7, p12);
            Polygon3D side16 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p12, p9);
            l2 = new Line3D(p9, p8);
            l3 = new Line3D(p8, p12);
            Polygon3D side17 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p12, p10);
            l2 = new Line3D(p10, p9);
            l3 = new Line3D(p9, p12);
            Polygon3D side18 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p12, p11);
            l2 = new Line3D(p11, p10);
            l3 = new Line3D(p10, p12);
            Polygon3D side19 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p12, p7);
            l2 = new Line3D(p7, p11);
            l3 = new Line3D(p11, p12);
            Polygon3D side20 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            Polyhedron3D p = new Polyhedron3D(new List<Polygon3D> { side1, side2, side3, side4, side5, side6, side7, side8, side9, side10,
                                                                    side11, side12, side13, side14, side15, side16, side17, side18, side19, side20 });
            return p;
        }

        public static Polyhedron3D Dodecahedron(int scale)
        {
            double t = scale * Math.Sqrt((5 - Math.Sqrt(5)) / 2);
            double h = Math.Sqrt(t * t - t * t / 4);
            double h2 = Math.Sqrt(scale * scale - t * t / 4);
            double val = Math.Sqrt(h * h - h2 * h2);
            Point3D p1 = new Point3D(0, 0, 0);
            double z = val;

            Point3D p2 = new Point3D(-scale, 0, z);
            double rad = (Math.PI / 180) * 72;
            double x = -scale;
            double y = 0.0;
            double newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            double newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p3 = new Point3D(newX, newY, z);
            x = newX; y = newY;

            newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p4 = new Point3D(newX, newY, z);
            x = newX; y = newY;

            newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p5 = new Point3D(newX, newY, z);
            x = newX; y = newY;

            newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p6 = new Point3D(newX, newY, z);

            z += h;
            Point3D p7 = new Point3D(scale, 0, z);
            x = scale;
            y = 0.0;
            newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p8 = new Point3D(newX, newY, z);
            x = newX; y = newY;

            newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p9 = new Point3D(newX, newY, z);
            x = newX; y = newY;

            newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p10 = new Point3D(newX, newY, z);
            x = newX; y = newY;

            newX = x * Math.Cos(rad) - y * Math.Sin(rad);
            newY = x * Math.Sin(rad) + y * Math.Cos(rad);
            Point3D p11 = new Point3D(newX, newY, z);

            z += val;
            Point3D p12 = new Point3D(0, 0, z);

            Line3D l1 = new Line3D(p1, p5);
            Line3D l2 = new Line3D(p5, p4);
            Line3D l3 = new Line3D(p4, p1);
            Polygon3D side1 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d1 = TriangleCenter(side1);

            l1 = new Line3D(p1, p5);
            l2 = new Line3D(p5, p6);
            l3 = new Line3D(p6, p1);
            Polygon3D side2 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d2 = TriangleCenter(side2);

            l1 = new Line3D(p1, p6);
            l2 = new Line3D(p6, p2);
            l3 = new Line3D(p2, p1);
            Polygon3D side3 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d3 = TriangleCenter(side3);

            l1 = new Line3D(p1, p2);
            l2 = new Line3D(p2, p3);
            l3 = new Line3D(p3, p1);
            Polygon3D side4 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d4 = TriangleCenter(side4);

            l1 = new Line3D(p1, p3);
            l2 = new Line3D(p3, p4);
            l3 = new Line3D(p4, p1);
            Polygon3D side5 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d5 = TriangleCenter(side5);


            l1 = new Line3D(p7, p4);
            l2 = new Line3D(p4, p5);
            l3 = new Line3D(p5, p7);
            Polygon3D side6 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d6 = TriangleCenter(side6);

            l1 = new Line3D(p7, p8);
            l2 = new Line3D(p8, p5);
            l3 = new Line3D(p5, p7);
            Polygon3D side7 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d7 = TriangleCenter(side7);

            l1 = new Line3D(p8, p5);
            l2 = new Line3D(p5, p6);
            l3 = new Line3D(p6, p8);
            Polygon3D side8 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d8 = TriangleCenter(side8);

            l1 = new Line3D(p8, p9);
            l2 = new Line3D(p9, p6);
            l3 = new Line3D(p6, p8);
            Polygon3D side9 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d9 = TriangleCenter(side9);

            l1 = new Line3D(p9, p2);
            l2 = new Line3D(p2, p6);
            l3 = new Line3D(p6, p9);
            Polygon3D side10 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d10 = TriangleCenter(side10);

            l1 = new Line3D(p9, p10);
            l2 = new Line3D(p10, p2);
            l3 = new Line3D(p2, p9);
            Polygon3D side11 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d11 = TriangleCenter(side11);

            l1 = new Line3D(p10, p2);
            l2 = new Line3D(p2, p3);
            l3 = new Line3D(p3, p10);
            Polygon3D side12 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d12 = TriangleCenter(side12);

            l1 = new Line3D(p10, p11);
            l2 = new Line3D(p11, p3);
            l3 = new Line3D(p3, p10);
            Polygon3D side13 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d13 = TriangleCenter(side13);

            l1 = new Line3D(p11, p3);
            l2 = new Line3D(p3, p4);
            l3 = new Line3D(p4, p11);
            Polygon3D side14 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d14 = TriangleCenter(side14);

            l1 = new Line3D(p11, p4);
            l2 = new Line3D(p4, p7);
            l3 = new Line3D(p7, p11);
            Polygon3D side15 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d15 = TriangleCenter(side15);


            l1 = new Line3D(p12, p7);
            l2 = new Line3D(p7, p8);
            l3 = new Line3D(p8, p12);
            Polygon3D side16 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d16 = TriangleCenter(side16);

            l1 = new Line3D(p12, p8);
            l2 = new Line3D(p8, p9);
            l3 = new Line3D(p9, p12);
            Polygon3D side17 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d17 = TriangleCenter(side17);

            l1 = new Line3D(p12, p9);
            l2 = new Line3D(p9, p10);
            l3 = new Line3D(p10, p12);
            Polygon3D side18 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d18 = TriangleCenter(side18);

            l1 = new Line3D(p12, p10);
            l2 = new Line3D(p10, p11);
            l3 = new Line3D(p11, p12);
            Polygon3D side19 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d19 = TriangleCenter(side19);

            l1 = new Line3D(p12, p11);
            l2 = new Line3D(p11, p7);
            l3 = new Line3D(p7, p12);
            Polygon3D side20 = new Polygon3D(new List<Line3D> { l1, l2, l3 });
            Point3D d20 = TriangleCenter(side20);


            l1 = new Line3D(d1, d2);
            l2 = new Line3D(d2, d3);
            l3 = new Line3D(d3, d4);
            Line3D l4 = new Line3D(d4, d5);
            Line3D l5 = new Line3D(d5, d1);
            side1 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            l1 = new Line3D(d1, d6);
            l2 = new Line3D(d6, d7);
            l3 = new Line3D(d7, d8);
            l4 = new Line3D(d8, d2);
            l5 = new Line3D(d2, d1);
            side2 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            l1 = new Line3D(d2, d8);
            l2 = new Line3D(d8, d9);
            l3 = new Line3D(d9, d10);
            l4 = new Line3D(d10, d3);
            l5 = new Line3D(d3, d2);
            side3 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            l1 = new Line3D(d3, d10);
            l2 = new Line3D(d10, d11);
            l3 = new Line3D(d11, d12);
            l4 = new Line3D(d12, d4);
            l5 = new Line3D(d4, d3);
            side4 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            l1 = new Line3D(d4, d12);
            l2 = new Line3D(d12, d13);
            l3 = new Line3D(d13, d14);
            l4 = new Line3D(d14, d5);
            l5 = new Line3D(d5, d4);
            side5 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            l1 = new Line3D(d1, d5);
            l2 = new Line3D(d5, d14);
            l3 = new Line3D(d14, d15);
            l4 = new Line3D(d15, d6);
            l5 = new Line3D(d6, d1);
            side6 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            l1 = new Line3D(d6, d15);
            l2 = new Line3D(d15, d20);
            l3 = new Line3D(d20, d16);
            l4 = new Line3D(d16, d7);
            l5 = new Line3D(d7, d6);
            side7 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            l1 = new Line3D(d8, d7);
            l2 = new Line3D(d7, d16);
            l3 = new Line3D(d16, d17);
            l4 = new Line3D(d17, d9);
            l5 = new Line3D(d9, d8);
            side8 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            l1 = new Line3D(d10, d9);
            l2 = new Line3D(d9, d17);
            l3 = new Line3D(d17, d18);
            l4 = new Line3D(d18, d11);
            l5 = new Line3D(d11, d10);
            side9 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            l1 = new Line3D(d11, d18);
            l2 = new Line3D(d18, d19);
            l3 = new Line3D(d19, d13);
            l4 = new Line3D(d13, d12);
            l5 = new Line3D(d12, d11);
            side10 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            l1 = new Line3D(d14, d13);
            l2 = new Line3D(d13, d19);
            l3 = new Line3D(d19, d20);
            l4 = new Line3D(d20, d15);
            l5 = new Line3D(d15, d14);
            side11 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            l1 = new Line3D(d16, d20);
            l2 = new Line3D(d20, d19);
            l3 = new Line3D(d19, d18);
            l4 = new Line3D(d18, d17);
            l5 = new Line3D(d17, d16);
            side12 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            Polyhedron3D p = new Polyhedron3D(new List<Polygon3D> { side1, side2, side3, side4, side5, side6, side7,
                                                                    side8, side9, side10, side11, side12 });
            return p;
        }

        private static Point3D TriangleCenter(Polygon3D triangle)
        {
            Line3D a = triangle.lines[0];
            Line3D b = triangle.lines[1];

            double x1 = a.first.x;
            double y1 = a.first.y;
            double z1 = a.first.z;

            double x2 = a.second.x;
            double y2 = a.second.y;
            double z2 = a.second.z;

            double x3, y3, z3;
            if ((x1 == b.first.x && y1 == b.first.y && z1 == b.first.z) ||
                (x2 == b.first.x && y2 == b.first.y && z2 == b.first.z))
            {
                x3 = b.second.x;
                y3 = b.second.y;
                z3 = b.second.z;
            }
            else
            {
                x3 = b.first.x;
                y3 = b.first.y;
                z3 = b.first.z;
            }

            Point3D center = new Point3D((x1 + x2 + x3) / 3, (y1 + y2 + y3) / 3, (z1 + z2 + z3) / 3);
            return center;
        }
    }
}
