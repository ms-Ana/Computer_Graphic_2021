using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace AffineTransformations3D
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private int W, H;
        private int scale = 100;

        private int currentPolyhedron;
        private bool currentAxonometric;
        private Polyhedron3D current;

        private double rotationAngle;
        private int rotation;
        bool rotating;

        private int rotations = 10;
        private List<Point> points;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            g = this.CreateGraphics();
            W = Width / 2; H = Height / 2;

            buttonAxonometric.Visible = false;
            buttonPerspective.Visible = false;
            currentAxonometric = true;
            rotationAngle = 0;
            rotation = -1;
            rotating = false;
            HideButtons(false);
            points = new List<Point>();
        }

        private Polyhedron3D Hexahedron(int scale)
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
            Line3D l2 = new Line3D(p4, p6);
            Line3D l3 = new Line3D(p4, p7);
            Line3D l4 = new Line3D(p7, p8);
            Polygon3D side1 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4 });

            l1 = new Line3D(p6, p2);
            l2 = new Line3D(p4, p6);
            l3 = new Line3D(p2, p1);
            l4 = new Line3D(p1, p4);
            Polygon3D side2 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4 });

            l1 = new Line3D(p6, p8);
            l2 = new Line3D(p2, p6);
            l3 = new Line3D(p2, p5);
            l4 = new Line3D(p5, p8);
            Polygon3D side3 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4 });

            l1 = new Line3D(p1, p3);
            l2 = new Line3D(p1, p2);
            l3 = new Line3D(p2, p5);
            l4 = new Line3D(p5, p3);
            Polygon3D side4 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4 });

            l1 = new Line3D(p7, p8);
            l2 = new Line3D(p8, p5);
            l3 = new Line3D(p5, p3);
            l4 = new Line3D(p3, p7);
            Polygon3D side5 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4 });

            l1 = new Line3D(p4, p7);
            l2 = new Line3D(p4, p1);
            l3 = new Line3D(p1, p3);
            l4 = new Line3D(p3, p7);
            Polygon3D side6 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4 });

            Polyhedron3D p = new Polyhedron3D(new List<Polygon3D> { side1, side2, side3, side4, side5, side6 });
            return p;
        }

        private Polyhedron3D Tetrahedron(int scale)
        {
            Point3D p1 = new Point3D(0, 0, 0);
            Point3D p2 = new Point3D(scale, 0, 0);
            Point3D p3 = new Point3D(0, scale, 0);
            Point3D p4 = new Point3D(0, 0, scale);
            Point3D p8 = new Point3D(scale, scale, scale);

            Line3D l1 = new Line3D(p4, p2);
            Line3D l2 = new Line3D(p2, p8);
            Line3D l3 = new Line3D(p8, p4);
            Polygon3D side1 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p8, p2);
            l2 = new Line3D(p2, p3);
            l3 = new Line3D(p3, p8);
            Polygon3D side2 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p4, p8);
            l2 = new Line3D(p8, p3);
            l3 = new Line3D(p3, p4);
            Polygon3D side3 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p4, p2);
            l2 = new Line3D(p2, p3);
            l3 = new Line3D(p3, p4);
            Polygon3D side4 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            Polyhedron3D p = new Polyhedron3D(new List<Polygon3D> { side1, side2, side3, side4 });
            return p;
        }

        private Polyhedron3D Octahedron(int scale)
        {
            Point3D p1 = new Point3D(scale, scale, scale * 2);
            Point3D p2 = new Point3D(scale, 0, scale);
            Point3D p3 = new Point3D(scale * 2, scale, scale);
            Point3D p4 = new Point3D(0, scale, scale);
            Point3D p5 = new Point3D(scale, scale, 0);
            Point3D p6 = new Point3D(scale, scale * 2, scale);

            Line3D l1 = new Line3D(p1, p2);
            Line3D l2 = new Line3D(p2, p3);
            Line3D l3 = new Line3D(p3, p2);
            Polygon3D side1 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p1, p2);
            l2 = new Line3D(p2, p4);
            l3 = new Line3D(p4, p1);
            Polygon3D side2 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p1, p4);
            l2 = new Line3D(p4, p6);
            l3 = new Line3D(p6, p1);
            Polygon3D side3 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p1, p3);
            l2 = new Line3D(p3, p6);
            l3 = new Line3D(p6, p1);
            Polygon3D side4 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p2, p3);
            l2 = new Line3D(p3, p5);
            l3 = new Line3D(p5, p2);
            Polygon3D side5 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p2, p4);
            l2 = new Line3D(p4, p5);
            l3 = new Line3D(p5, p2);
            Polygon3D side6 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p4, p6);
            l2 = new Line3D(p6, p5);
            l3 = new Line3D(p5, p4);
            Polygon3D side7 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p3, p6);
            l2 = new Line3D(p6, p5);
            l3 = new Line3D(p5, p3);
            Polygon3D side8 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            Polyhedron3D p = new Polyhedron3D(new List<Polygon3D> { side1, side2, side3, side4, side5, side6, side7, side8 });
            return p;
        }

        private Polyhedron3D Icosahedron(int scale)
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


            l1 = new Line3D(p7, p4);
            l2 = new Line3D(p4, p5);
            l3 = new Line3D(p5, p7);
            Polygon3D side6 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p7, p8);
            l2 = new Line3D(p8, p5);
            l3 = new Line3D(p5, p7);
            Polygon3D side7 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p8, p5);
            l2 = new Line3D(p5, p6);
            l3 = new Line3D(p6, p8);
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

            l1 = new Line3D(p10, p2);
            l2 = new Line3D(p2, p3);
            l3 = new Line3D(p3, p10);
            Polygon3D side12 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p10, p11);
            l2 = new Line3D(p11, p3);
            l3 = new Line3D(p3, p10);
            Polygon3D side13 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p11, p3);
            l2 = new Line3D(p3, p4);
            l3 = new Line3D(p4, p11);
            Polygon3D side14 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p11, p4);
            l2 = new Line3D(p4, p7);
            l3 = new Line3D(p7, p11);
            Polygon3D side15 = new Polygon3D(new List<Line3D> { l1, l2, l3 });


            l1 = new Line3D(p12, p7);
            l2 = new Line3D(p7, p8);
            l3 = new Line3D(p8, p12);
            Polygon3D side16 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p12, p8);
            l2 = new Line3D(p8, p9);
            l3 = new Line3D(p9, p12);
            Polygon3D side17 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p12, p9);
            l2 = new Line3D(p9, p10);
            l3 = new Line3D(p10, p12);
            Polygon3D side18 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p12, p10);
            l2 = new Line3D(p10, p11);
            l3 = new Line3D(p11, p12);
            Polygon3D side19 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            l1 = new Line3D(p12, p11);
            l2 = new Line3D(p11, p7);
            l3 = new Line3D(p7, p12);
            Polygon3D side20 = new Polygon3D(new List<Line3D> { l1, l2, l3 });

            Polyhedron3D p = new Polyhedron3D(new List<Polygon3D> { side1, side2, side3, side4, side5, side6, side7, side8, side9, side10,
                                                                    side11, side12, side13, side14, side15, side16, side17, side18, side19, side20 });
            return p;
        }

        private Polyhedron3D Dodecahedron(int scale)
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

            l1 = new Line3D(d1, d2);
            l2 = new Line3D(d2, d8);
            l3 = new Line3D(d8, d7);
            l4 = new Line3D(d7, d6);
            l5 = new Line3D(d6, d1);
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

            l1 = new Line3D(d1, d6);
            l2 = new Line3D(d6, d15);
            l3 = new Line3D(d15, d14);
            l4 = new Line3D(d14, d5);
            l5 = new Line3D(d5, d1);
            side6 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            l1 = new Line3D(d6, d7);
            l2 = new Line3D(d7, d16);
            l3 = new Line3D(d16, d20);
            l4 = new Line3D(d20, d15);
            l5 = new Line3D(d15, d6);
            side7 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            l1 = new Line3D(d8, d9);
            l2 = new Line3D(d9, d17);
            l3 = new Line3D(d17, d16);
            l4 = new Line3D(d16, d7);
            l5 = new Line3D(d7, d8);
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

            l1 = new Line3D(d14, d15);
            l2 = new Line3D(d15, d20);
            l3 = new Line3D(d20, d19);
            l4 = new Line3D(d19, d13);
            l5 = new Line3D(d13, d14);
            side11 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            l1 = new Line3D(d16, d17);
            l2 = new Line3D(d17, d18);
            l3 = new Line3D(d18, d19);
            l4 = new Line3D(d19, d20);
            l5 = new Line3D(d20, d16);
            side12 = new Polygon3D(new List<Line3D> { l1, l2, l3, l4, l5 });

            Polyhedron3D p = new Polyhedron3D(new List<Polygon3D> { side1, side2, side3, side4, side5, side6, side7, 
                                                                    side8, side9, side10, side11, side12 });
            return p;
        }

        private Point3D TriangleCenter(Polygon3D triangle)
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

        private void buttonHexahedron_Click(object sender, EventArgs e)
        {
            currentAxonometric = true;
            g.Clear(SystemColors.Control);
            DrawAxisAxonometric();
            ShowFigure(current.Axonometric(), Color.Black);
        }

        private void buttonPerspective_Click(object sender, EventArgs e)
        {
            currentAxonometric = false;
            g.Clear(SystemColors.Control);
            DrawAxisPerspective();
            ShowFigure(current.Perspective(), Color.Black);
        }

        private void TranslateFigure()
        {
            if (!String.IsNullOrEmpty(textBoxDx.Text) && 
                !String.IsNullOrEmpty(textBoxDy.Text) && 
                !String.IsNullOrEmpty(textBoxDz.Text))
            {
                int dx, dy, dz;
                bool dxSuccess = Int32.TryParse(textBoxDx.Text, out dx);
                bool dySuccess = Int32.TryParse(textBoxDy.Text, out dy);
                bool dzSuccess = Int32.TryParse(textBoxDz.Text, out dz);
                if (dxSuccess && dySuccess && dzSuccess)
                {
                    current.Translate(dx, dy, dz);
                    g.Clear(SystemColors.Control);
                    if (currentAxonometric)
                    {
                        DrawAxisAxonometric();
                        ShowFigure(current.Axonometric(), Color.Black);
                    }
                    else
                    {
                        DrawAxisPerspective();
                        ShowFigure(current.Perspective(), Color.Black);
                    }
                }
            }
        }

        private void ScaleFigure()
        {
            if (!String.IsNullOrEmpty(textBoxMx.Text) &&
                !String.IsNullOrEmpty(textBoxMy.Text) &&
                !String.IsNullOrEmpty(textBoxMz.Text))
            {
                int mx, my, mz;
                bool mxSuccess = Int32.TryParse(textBoxMx.Text, out mx);
                bool mySuccess = Int32.TryParse(textBoxMy.Text, out my);
                bool mzSuccess = Int32.TryParse(textBoxMz.Text, out mz);
                if (mxSuccess && mySuccess && mzSuccess)
                {
                    current.ScaleCenter(mx, my, mz);
                    g.Clear(SystemColors.Control);
                    if (currentAxonometric)
                    {
                        DrawAxisAxonometric();
                        ShowFigure(current.Axonometric(), Color.Black);
                    }
                    else
                    {
                        DrawAxisPerspective();
                        ShowFigure(current.Perspective(), Color.Black);
                    }
                }
            }
        }

        private void ReflectFigure(String axis)
        {
            current.Reflect(axis);
            g.Clear(SystemColors.Control);
            if (currentAxonometric)
            {
                DrawAxisAxonometric();
                ShowFigure(current.Axonometric(), Color.Black);
            }
            else
            {
                DrawAxisPerspective();
                ShowFigure(current.Perspective(), Color.Black);
            }
        }

        private void RotateFigure()
        {
            if (rotation < 0)
            {
                return;
            }
            switch (rotation)
            {
                case 0:
                    current.RotateCenter(rotationAngle, 0, 0);
                    break;
                case 1:
                    current.RotateCenter(0, rotationAngle, 0);
                    break;
                case 2:
                    current.RotateCenter(0, 0, rotationAngle);
                    break;
                case 3:
                    if (!String.IsNullOrEmpty(textBoxLine.Text))
                    {
                        String[] points = textBoxLine.Text.Split(';');
                        if (points.Count() < 6)
                        {
                            return;
                        }
                        int x1, y1, x2, y2, z1, z2;
                        bool x1Success = Int32.TryParse(points[0], out x1);
                        bool y1Success = Int32.TryParse(points[1], out y1);
                        bool z1Success = Int32.TryParse(points[2], out z1);
                        bool x2Success = Int32.TryParse(points[3], out x2);
                        bool y2Success = Int32.TryParse(points[4], out y2);
                        bool z2Success = Int32.TryParse(points[5], out z2);
                        if (x1Success && y1Success && z1Success && x2Success && y2Success && z2Success)
                        {
                            Point3D first = new Point3D(x1, y1, z1);
                            Point3D second = new Point3D(x2, y2, z2);
                            Line3D line = new Line3D(first, second);
                            current.RotateLine(rotationAngle, line);
                        }
                    }
                    break;
                default:
                    break;
            }

            g.Clear(SystemColors.Control);
            if (currentAxonometric)
            {
                DrawAxisAxonometric();
                ShowFigure(current.Axonometric(), Color.Black);
            }
            else
            {
                DrawAxisPerspective();
                ShowFigure(current.Perspective(), Color.Black);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            currentPolyhedron = cmb.SelectedIndex;
            Polyhedron3D p = Hexahedron(scale);
            HideButtons(false);
            switch (currentPolyhedron)
            {
                case 1:
                    p = Tetrahedron(scale);
                    break;
                case 2:
                    p = Octahedron(scale);
                    break;
                case 3:
                    p = Icosahedron(scale);
                    break;
                case 4:
                    p = Dodecahedron(scale);
                    break;
                case 5:
                    p = Polyhedron3D.Graphic(SinCos, 30, -100, 100, -100, 100);
                    break;
                case 6:
                    p = Polyhedron3D.Graphic(Square, 30, -10, 10, -10, 10);
                    break;
                case 7:
                    HideButtons(true);
                    break;
                default:
                    break;
            }
            current = p;

            buttonAxonometric.Visible = true;
            buttonPerspective.Visible = true;
        }

        private void HideButtons(bool hide)
        {
            button1.Visible = hide;
            buttonX.Visible = hide;
            buttonY.Visible = hide;
            buttonZ.Visible = hide;
            textBoxRotations.Visible = hide;
            label13.Visible = hide;
        }

        private void DrawAxisAxonometric()
        {
            Point3D startX = new Point3D(-2000, 0, 0);
            Point3D startY = new Point3D(0, -2000, 0);
            Point3D startZ = new Point3D(0, 0, -2000);
            Point3D x = new Point3D(2000, 0, 0);
            Point3D y = new Point3D(0, 2000, 0);
            Point3D z = new Point3D(0, 0, 2000);

            Line3D xAxis = new Line3D(startX, x);
            Polygon3D s1 = new Polygon3D(new List<Line3D> { xAxis });
            Polyhedron3D p1 = new Polyhedron3D(new List<Polygon3D> { s1 });
            ShowFigure(p1.Axonometric(), Color.Red);

            Line3D yAxis = new Line3D(startY, y);
            Polygon3D s2 = new Polygon3D(new List<Line3D> { yAxis });
            Polyhedron3D p2 = new Polyhedron3D(new List<Polygon3D> { s2 });
            ShowFigure(p2.Axonometric(), Color.Blue);

            Line3D zAxis = new Line3D(startZ, z);
            Polygon3D s3 = new Polygon3D(new List<Line3D> { zAxis });
            Polyhedron3D p3 = new Polyhedron3D(new List<Polygon3D> { s3 });
            ShowFigure(p3.Axonometric(), Color.Green);
        }

        private void DrawAxisPerspective()
        {
            Point3D startX = new Point3D(-2000, 0, 0);
            Point3D startY = new Point3D(0, -2000, 0);
            Point3D startZ = new Point3D(0, 0, -2000);
            Point3D x = new Point3D(2000, 0, 0);
            Point3D y = new Point3D(0, 2000, 0);
            Point3D z = new Point3D(0, 0, 2000);

            Line3D xAxis = new Line3D(startX, x);
            Polygon3D s1 = new Polygon3D(new List<Line3D> { xAxis });
            Polyhedron3D p1 = new Polyhedron3D(new List<Polygon3D> { s1 });
            ShowFigure(p1.Perspective(), Color.Red);

            Line3D yAxis = new Line3D(startY, y);
            Polygon3D s2 = new Polygon3D(new List<Line3D> { yAxis });
            Polyhedron3D p2 = new Polyhedron3D(new List<Polygon3D> { s2 });
            ShowFigure(p2.Perspective(), Color.Blue);

            Line3D zAxis = new Line3D(startZ, z);
            Polygon3D s3 = new Polygon3D(new List<Line3D> { zAxis });
            Polyhedron3D p3 = new Polyhedron3D(new List<Polygon3D> { s3 });
            ShowFigure(p3.Perspective(), Color.Green);
        }

        private void buttonTranslation_Click(object sender, EventArgs e)
        {
            TranslateFigure();
        }

        private void buttonScale_Click(object sender, EventArgs e)
        {
            ScaleFigure();
        }

        private void buttonReflectXy_Click(object sender, EventArgs e)
        {
            ReflectFigure("xy");
        }

        private void buttonReflectXz_Click(object sender, EventArgs e)
        {
            ReflectFigure("xz");
        }

        private void buttonReflectYz_Click(object sender, EventArgs e)
        {
            ReflectFigure("yz");
        }

        private void Form1_MouseWheelRotate(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                rotationAngle = 10;
            }
            else
            {
                rotationAngle = -10;
            }
            RotateFigure();
        }

        private void buttonRotateX_Click(object sender, EventArgs e)
        {
            if (rotating)
            {
                if (buttonRotateX.BackColor == Color.LightSkyBlue)
                {
                    rotating = false;
                    rotationAngle = 0;
                    rotation = -1;
                    buttonRotateX.BackColor = SystemColors.ControlLight;
                    this.MouseWheel -= Form1_MouseWheelRotate;
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                rotating = true;
                rotationAngle = 0;
                rotation = 0;
                buttonRotateX.BackColor = Color.LightSkyBlue;
                this.MouseWheel += Form1_MouseWheelRotate;
            }
        }

        private void buttonRotateY_Click(object sender, EventArgs e)
        {
            if (rotating)
            {
                if (buttonRotateY.BackColor == Color.LightSkyBlue)
                {
                    rotating = false;
                    rotationAngle = 0;
                    rotation = -1;
                    buttonRotateY.BackColor = SystemColors.ControlLight;
                    this.MouseWheel -= Form1_MouseWheelRotate;
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                rotating = true;
                rotationAngle = 0;
                rotation = 1;
                buttonRotateY.BackColor = Color.LightSkyBlue;
                this.MouseWheel += Form1_MouseWheelRotate;
            }
        }

        private void buttonRotateZ_Click(object sender, EventArgs e)
        {
            if (rotating)
            {
                if (buttonRotateZ.BackColor == Color.LightSkyBlue)
                {
                    rotating = false;
                    rotationAngle = 0;
                    rotation = -1;
                    buttonRotateZ.BackColor = SystemColors.ControlLight;
                    this.MouseWheel -= Form1_MouseWheelRotate;
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                rotating = true;
                rotationAngle = 0;
                rotation = 2;
                buttonRotateZ.BackColor = Color.LightSkyBlue;
                this.MouseWheel += Form1_MouseWheelRotate;
            }
        }

        private void buttonRotateLine_Click(object sender, EventArgs e)
        {
            if (rotating)
            {
                if (buttonRotateLine.BackColor == Color.LightSkyBlue)
                {
                    rotating = false;
                    rotationAngle = 0;
                    rotation = -1;
                    buttonRotateLine.BackColor = SystemColors.ControlLight;
                    this.MouseWheel -= Form1_MouseWheelRotate;
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                rotating = true;
                rotationAngle = 0;
                rotation = 3;
                buttonRotateLine.BackColor = Color.LightSkyBlue;
                this.MouseWheel += Form1_MouseWheelRotate;
            }
        }

        private double SinCos(double x, double y)
        {
            return Math.Sin(x) * Math.Cos(y);
        }

        private double Square(double x, double y)
        {
            return x * x + y * y;
        }

        private void fixPoints()
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (points.Count != 0 && points.Count < 3)
            {
                return;
            }

            Button button = (Button)sender;
            if (button.Text == "Задать образующую")
            {
                points.Clear();
                textBoxRotations.Visible = false;
                buttonX.Visible = false;
                buttonY.Visible = false;
                buttonZ.Visible = false;
                g.Clear(SystemColors.Control);
                MouseClick += Form1_MouseClick;
            }
            else
            {
                button.Text = "Задать образующую";
                MouseClick -= Form1_MouseClick;
                if (points.Count > 2)
                {
                    Pen pen = new Pen(Color.Black);
                    Point first = points[0];
                    Point last = points[points.Count - 1];
                    g.DrawLine(pen, first.X + W, H - first.Y, last.X + W, H - last.Y);
                }
                textBoxRotations.Visible = true;
                textBoxRotations.Text = "" + rotations;
                buttonX.Visible = true;
                buttonY.Visible = true;
                buttonZ.Visible = true;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(Color.Black);
            if (points.Count > 0)
            {
                Point prev = points[points.Count - 1];
                g.DrawLine(pen, W + prev.X, H - prev.Y, e.X, e.Y);
            }

            points.Add(new Point(e.X - W, H - e.Y));
            g.FillEllipse(brush, e.X, e.Y, 1, 1);
            button1.Text = "Готово";
        }

        private void textBoxRotations_TextChanged(object sender, EventArgs e)
        {
            String text = textBoxRotations.Text;
            if (String.IsNullOrEmpty(text))
            {
                return;
            }

            int r;
            bool success = Int32.TryParse(text, out r);
            if (success)
            {
                if (r < 3)
                {
                    return;
                }
                rotations = r;
            }
        }

        private void buttonX_Click(object sender, EventArgs e)
        {
            fixPoints();
            Point3D pFirst = new Point3D(points[1].Y, 0, 0);
            Point3D pLast = new Point3D(points[0].Y, 0, 0);

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

                Point3D p1 = new Point3D(x[0], newY[0], newZ[0]);
                Point3D p2 = new Point3D(x[0], y[0], z[0]);
                Line3D l1 = new Line3D(pFirst, p1);
                Line3D l2 = new Line3D(pFirst, p2);
                Line3D l3 = new Line3D(p1, p2);
                Polygon3D side = new Polygon3D(new List<Line3D>() { l1, l2, l3 });
                sides.Add(side);

                for (int j = 0; j < points.Count - 3; j++)
                {
                    Point3D point1 = new Point3D(x[j], newY[j], newZ[j]);
                    Point3D point2 = new Point3D(x[j + 1], newY[j + 1], newZ[j + 1]);
                    Point3D point3 = new Point3D(x[j], y[j], z[j]);
                    Point3D point4 = new Point3D(x[j + 1], y[j + 1], z[j + 1]);
                    Line3D line1 = new Line3D(point1, point2);
                    Line3D line2 = new Line3D(point1, point3);
                    Line3D line3 = new Line3D(point3, point4);
                    Line3D line4 = new Line3D(point2, point4);
                    Polygon3D side1 = new Polygon3D(new List<Line3D>() { line1, line2, line3, line4 });
                    sides.Add(side1);
                }

                p1 = new Point3D(x[x.Count - 1], newY[newY.Count - 1], newZ[newZ.Count - 1]);
                p2 = new Point3D(x[x.Count - 1], y[y.Count - 1], z[z.Count - 1]);
                l1 = new Line3D(pLast, p1);
                l2 = new Line3D(pLast, p2);
                l3 = new Line3D(p1, p2);
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

            Point3D lastPoint1 = new Point3D(points[2].Y, points[2].X, 0);
            Point3D lastPoint2 = new Point3D(x[0], y[0], z[0]);
            Line3D lastLine1 = new Line3D(pFirst, lastPoint1);
            Line3D lastLine2 = new Line3D(pFirst, lastPoint2);
            Line3D lastLine3 = new Line3D(lastPoint1, lastPoint2);
            Polygon3D lastSide = new Polygon3D(new List<Line3D>() { lastLine1, lastLine2, lastLine3 });
            sides.Add(lastSide);

            for (int j = 0; j < points.Count - 3; j++)
            {
                Point3D point1 = new Point3D(points[j + 2].Y, points[j + 2].X, 0);
                Point3D point2 = new Point3D(points[j + 3].Y, points[j + 3].X, 0);
                Point3D point3 = new Point3D(x[j], y[j], z[j]);
                Point3D point4 = new Point3D(x[j + 1], y[j + 1], z[j + 1]);
                Line3D line1 = new Line3D(point1, point2);
                Line3D line2 = new Line3D(point1, point3);
                Line3D line3 = new Line3D(point3, point4);
                Line3D line4 = new Line3D(point2, point4);
                Polygon3D side1 = new Polygon3D(new List<Line3D>() { line1, line2, line3, line4 });
                sides.Add(side1);
            }

            lastPoint1 = new Point3D(points[points.Count - 1].Y, points[points.Count - 1].X, 0);
            lastPoint2 = new Point3D(x[x.Count - 1], y[y.Count - 1], z[z.Count - 1]);
            lastLine1 = new Line3D(pLast, lastPoint1);
            lastLine2 = new Line3D(pLast, lastPoint2);
            lastLine3 = new Line3D(lastPoint1, lastPoint2);
            lastSide = new Polygon3D(new List<Line3D>() { lastLine1, lastLine2, lastLine3 });
            sides.Add(lastSide);

            current = new Polyhedron3D(sides);
            g.Clear(SystemColors.Control);
            if (currentAxonometric)
            {
                DrawAxisAxonometric();
                ShowFigure(current.Axonometric(), Color.Black);
            }
            else
            {
                DrawAxisPerspective();
                ShowFigure(current.Perspective(), Color.Black);
            }
        }

        private void buttonY_Click(object sender, EventArgs e)
        {
            fixPoints();
            Point3D pFirst = new Point3D(0, points[1].Y, 0);
            Point3D pLast = new Point3D(0, points[0].Y, 0);

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

                Point3D p1 = new Point3D(newX[0], y[0], newZ[0]);
                Point3D p2 = new Point3D(x[0], y[0], z[0]);
                Line3D l1 = new Line3D(pFirst, p1);
                Line3D l2 = new Line3D(pFirst, p2);
                Line3D l3 = new Line3D(p1, p2);
                Polygon3D side = new Polygon3D(new List<Line3D>() { l1, l2, l3 });
                sides.Add(side);

                for (int j = 0; j < points.Count - 3; j++)
                {
                    Point3D point1 = new Point3D(newX[j], y[j], newZ[j]);
                    Point3D point2 = new Point3D(newX[j + 1], y[j + 1], newZ[j + 1]);
                    Point3D point3 = new Point3D(x[j], y[j], z[j]);
                    Point3D point4 = new Point3D(x[j + 1], y[j + 1], z[j + 1]);
                    Line3D line1 = new Line3D(point1, point2);
                    Line3D line2 = new Line3D(point1, point3);
                    Line3D line3 = new Line3D(point3, point4);
                    Line3D line4 = new Line3D(point2, point4);
                    Polygon3D side1 = new Polygon3D(new List<Line3D>() { line1, line2, line3, line4 });
                    sides.Add(side1);
                }

                p1 = new Point3D(newX[newX.Count - 1], y[y.Count - 1], newZ[newZ.Count - 1]);
                p2 = new Point3D(x[x.Count - 1], y[y.Count - 1], z[z.Count - 1]);
                l1 = new Line3D(pLast, p1);
                l2 = new Line3D(pLast, p2);
                l3 = new Line3D(p1, p2);
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

            Point3D lastPoint1 = new Point3D(points[2].X, points[2].Y, 0);
            Point3D lastPoint2 = new Point3D(x[0], y[0], z[0]);
            Line3D lastLine1 = new Line3D(pFirst, lastPoint1);
            Line3D lastLine2 = new Line3D(pFirst, lastPoint2);
            Line3D lastLine3 = new Line3D(lastPoint1, lastPoint2);
            Polygon3D lastSide = new Polygon3D(new List<Line3D>() { lastLine1, lastLine2, lastLine3 });
            sides.Add(lastSide);

            for (int j = 0; j < points.Count - 3; j++)
            {
                Point3D point1 = new Point3D(points[j + 2].X, points[j + 2].Y, 0);
                Point3D point2 = new Point3D(points[j + 3].X, points[j + 3].Y, 0);
                Point3D point3 = new Point3D(x[j], y[j], z[j]);
                Point3D point4 = new Point3D(x[j + 1], y[j + 1], z[j + 1]);
                Line3D line1 = new Line3D(point1, point2);
                Line3D line2 = new Line3D(point1, point3);
                Line3D line3 = new Line3D(point3, point4);
                Line3D line4 = new Line3D(point2, point4);
                Polygon3D side1 = new Polygon3D(new List<Line3D>() { line1, line2, line3, line4 });
                sides.Add(side1);
            }

            lastPoint1 = new Point3D(points[points.Count - 1].X, points[points.Count - 1].Y, 0);
            lastPoint2 = new Point3D(x[x.Count - 1], y[y.Count - 1], z[z.Count - 1]);
            lastLine1 = new Line3D(pLast, lastPoint1);
            lastLine2 = new Line3D(pLast, lastPoint2);
            lastLine3 = new Line3D(lastPoint1, lastPoint2);
            lastSide = new Polygon3D(new List<Line3D>() { lastLine1, lastLine2, lastLine3 });
            sides.Add(lastSide);

            current = new Polyhedron3D(sides);
            g.Clear(SystemColors.Control);
            if (currentAxonometric)
            {
                DrawAxisAxonometric();
                ShowFigure(current.Axonometric(), Color.Black);
            }
            else
            {
                DrawAxisPerspective();
                ShowFigure(current.Perspective(), Color.Black);
            }
        }

        private void buttonZ_Click(object sender, EventArgs e)
        {
            fixPoints();
            Point3D pFirst = new Point3D(0, 0, points[1].Y);
            Point3D pLast = new Point3D(0, 0, points[0].Y);

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

                Point3D p1 = new Point3D(newX[0], newY[0], z[0]);
                Point3D p2 = new Point3D(x[0], y[0], z[0]);
                Line3D l1 = new Line3D(pFirst, p1);
                Line3D l2 = new Line3D(pFirst, p2);
                Line3D l3 = new Line3D(p1, p2);
                Polygon3D side = new Polygon3D(new List<Line3D>() { l1, l2, l3 });
                sides.Add(side);

                for (int j = 0; j < points.Count - 3; j++)
                {
                    Point3D point1 = new Point3D(newX[j], newY[j], z[j]);
                    Point3D point2 = new Point3D(newX[j + 1], newY[j + 1], z[j + 1]);
                    Point3D point3 = new Point3D(x[j], y[j], z[j]);
                    Point3D point4 = new Point3D(x[j + 1], y[j + 1], z[j + 1]);
                    Line3D line1 = new Line3D(point1, point2);
                    Line3D line2 = new Line3D(point1, point3);
                    Line3D line3 = new Line3D(point3, point4);
                    Line3D line4 = new Line3D(point2, point4);
                    Polygon3D side1 = new Polygon3D(new List<Line3D>() { line1, line2, line3, line4 });
                    sides.Add(side1);
                }

                p1 = new Point3D(newX[newX.Count - 1], newY[newY.Count - 1], z[z.Count - 1]);
                p2 = new Point3D(x[x.Count - 1], y[y.Count - 1], z[z.Count - 1]);
                l1 = new Line3D(pLast, p1);
                l2 = new Line3D(pLast, p2);
                l3 = new Line3D(p1, p2);
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

            Point3D lastPoint1 = new Point3D(points[2].X, 0, points[2].Y);
            Point3D lastPoint2 = new Point3D(x[0], y[0], z[0]);
            Line3D lastLine1 = new Line3D(pFirst, lastPoint1);
            Line3D lastLine2 = new Line3D(pFirst, lastPoint2);
            Line3D lastLine3 = new Line3D(lastPoint1, lastPoint2);
            Polygon3D lastSide = new Polygon3D(new List<Line3D>() { lastLine1, lastLine2, lastLine3 });
            sides.Add(lastSide);

            for (int j = 0; j < points.Count - 3; j++)
            {
                Point3D point1 = new Point3D(points[j + 2].X, 0, points[j + 2].Y);
                Point3D point2 = new Point3D(points[j + 3].X, 0, points[j + 3].Y);
                Point3D point3 = new Point3D(x[j], y[j], z[j]);
                Point3D point4 = new Point3D(x[j + 1], y[j + 1], z[j + 1]);
                Line3D line1 = new Line3D(point1, point2);
                Line3D line2 = new Line3D(point1, point3);
                Line3D line3 = new Line3D(point3, point4);
                Line3D line4 = new Line3D(point2, point4);
                Polygon3D side1 = new Polygon3D(new List<Line3D>() { line1, line2, line3, line4 });
                sides.Add(side1);
            }

            lastPoint1 = new Point3D(points[points.Count - 1].X, 0, points[points.Count - 1].Y);
            lastPoint2 = new Point3D(x[x.Count - 1], y[y.Count - 1], z[z.Count - 1]);
            lastLine1 = new Line3D(pLast, lastPoint1);
            lastLine2 = new Line3D(pLast, lastPoint2);
            lastLine3 = new Line3D(lastPoint1, lastPoint2);
            lastSide = new Polygon3D(new List<Line3D>() { lastLine1, lastLine2, lastLine3 });
            sides.Add(lastSide);

            current = new Polyhedron3D(sides);
            g.Clear(SystemColors.Control);
            if (currentAxonometric)
            {
                DrawAxisAxonometric();
                ShowFigure(current.Axonometric(), Color.Black);
            }
            else
            {
                DrawAxisPerspective();
                ShowFigure(current.Perspective(), Color.Black);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string writePath = "D:/MyPolyhedron.txt";
            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
            {
                sw.Write("");
            }
            using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
            {
                foreach (Polygon3D a in current.polygons)
                {
                    foreach (Line3D l in a.lines)
                    {
                        Point3D p1 = l.first;
                        double x = p1.x;
                        double y = p1.y;
                        double z = p1.z;

                        sw.Write(x);
                        sw.Write("! ");
                        sw.Write(y);
                        sw.Write("! ");
                        sw.Write(z);
                        sw.Write("; ");

                        Point3D p2 = l.second;
                        x = p2.x;
                        y = p2.y;
                        z = p2.z;

                        sw.Write(x);
                        sw.Write("! ");
                        sw.Write(y);
                        sw.Write("! ");
                        sw.Write(z);
                        sw.Write(";");
                        sw.WriteLine();
                    }
                    sw.WriteLine();
                    sw.WriteLine('p');
                    sw.WriteLine();
                }
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            string readPath = "D:/MyPolyhedron.txt";
            string text = File.ReadAllText(readPath);

            List<Polygon3D> sides = new List<Polygon3D>();
            List<Line3D> lines = new List<Line3D>();
            foreach (string polygon in text.Trim().Split('p'))
            {
                lines.Clear();
                string[] points = polygon.Trim().Split(';');
                for (int i = 0; i < points.Count() - 1; i += 2)
                {
                    string[] first = points[i].Trim().Split('!');
                    string[] second = points[i + 1].Trim().Split('!');

                    double x, y, z, x2, y2, z2;
                    bool xSuccess = Double.TryParse(first[0], out x);
                    bool ySuccess = Double.TryParse(first[1], out y);
                    bool zSuccess = Double.TryParse(first[2], out z);

                    bool x2Success = Double.TryParse(second[0], out x2);
                    bool y2Success = Double.TryParse(second[1], out y2);
                    bool z2Success = Double.TryParse(second[2], out z2);

                    if (xSuccess && ySuccess && zSuccess && x2Success && y2Success && z2Success)
                    {
                        Point3D p1 = new Point3D(x, y, z);
                        Point3D p2 = new Point3D(x2, y2, z2);
                        Line3D l = new Line3D(p1, p2);
                        lines.Add(l);
                    }
                }
                Polygon3D pol = new Polygon3D(lines);
                sides.Add(pol);
            }

            current = new Polyhedron3D(sides);
            g.Clear(SystemColors.Control);
            if (currentAxonometric)
            {
                DrawAxisAxonometric();
                ShowFigure(current.Axonometric(), Color.Black);
            }
            else
            {
                DrawAxisPerspective();
                ShowFigure(current.Perspective(), Color.Black);
            }
        }

        private void ShowFigure(Polyhedron3D polyhedron, Color color)
        {
            Pen pen = new Pen(color);
            foreach (Polygon3D p in polyhedron.polygons)
            {
                foreach (Line3D l in p.lines)
                {
                    g.DrawLine(pen, (int)l.first.x + W, H - (int)l.first.y, (int)l.second.x + W, H - (int)l.second.y);
                }
            }
        }
    }

    public delegate double callable(double x, double y);
    public class Point3D
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public Point3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public static Point3D operator -(Point3D leftPoint, Point3D rightPoint)
        {
            return new Point3D(leftPoint.x - rightPoint.x,
                               leftPoint.y - rightPoint.y,
                               leftPoint.z - rightPoint.z);
        }
    }

    public class Line3D
    {
        public Point3D first { get; set; }
        public Point3D second { get; set; }

        public Line3D(Point3D p1, Point3D p2)
        {
            first = new Point3D(p1.x, p1.y, p1.z);
            second = new Point3D(p2.x, p2.y, p2.z);
        }
    }

    public class Polygon3D
    {
        public List<Line3D> lines { get; set; }

        public Polygon3D(List<Line3D> lines)
        {
            this.lines = new List<Line3D>();
            this.lines.AddRange(lines);
        }
    }

    public class Polyhedron3D
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

        public int Phi = 30;
        public int Psi = 30;

        public Polyhedron3D Axonometric()
        {
            Polyhedron3D res = new Polyhedron3D();
            double phi_r = (Math.PI / 180) * Phi;
            double psi_r = (Math.PI / 180) * Psi;

            foreach (Polygon3D p in polygons)
            {
                List<Line3D> lines = new List<Line3D>();
                foreach (Line3D l in p.lines)
                {
                    Point3D p1 = l.first;
                    double x = p1.x;
                    double y = p1.y;
                    double z = p1.z;

                    double newX = (x - z) * Math.Cos(phi_r);
                    double newY = y + (x + z) * Math.Sin(phi_r);
                    Point3D first = new Point3D(newX, newY, z);

                    Point3D p2 = l.second;
                    x = p2.x;
                    y = p2.y;
                    z = p2.z;

                    newX = (x - z) * Math.Cos(phi_r);
                    newY = y + (x + z) * Math.Sin(phi_r);
                    Point3D second = new Point3D(newX, newY, z);
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
                    Point3D p1 = l.first;
                    double x = p1.x;
                    double y = p1.y;
                    double z = p1.z;

                    double r = -0.1;
                    double k = 1000;

                    double newX = (k * x) / (z + k);
                    double newY = (k * y) / (z + k);
                    Point3D first = new Point3D(newX, newY, p1.z);

                    Point3D p2 = l.second;
                    x = p2.x;
                    y = p2.y;
                    z = p2.z;

                    newX = (k * x) / (z + k);
                    newY = (k * y) / (z + k);
                    Point3D second = new Point3D(newX, newY, p2.z);
                    Line3D line = new Line3D(first, second);
                    lines.Add(line);
                }
                res.polygons.Add(new Polygon3D(lines));
            }

            return res;
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
                    var firstPoint = new Point3D(firstPointMatrix[0, 0] / firstPointMatrix[3, 0],
                                                 firstPointMatrix[1, 0] / firstPointMatrix[3, 0],
                                                 firstPointMatrix[2, 0] / firstPointMatrix[3, 0]);
                    var secondPoint = new Point3D(secondPointMatrix[0, 0] / secondPointMatrix[3, 0],
                                                 secondPointMatrix[1, 0] / secondPointMatrix[3, 0],
                                                 secondPointMatrix[2, 0] / secondPointMatrix[3, 0]);
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
            List<Point3D> points = new List<Point3D>();
            for (int i = 0; i < countStep; i++)
            {
                currentX = X0;
                for (int j = 0; j < countStep; j++)
                {
                    points.Add(new Point3D(currentX, currentY, func(currentX, currentY)));
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

    public class GraphMath3D
    {
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
    }

    public class ZBuffer
    {
        private List<int> Interpolate(int d0, int d1, int i0, int i1)
        { return null; }

    }
}
