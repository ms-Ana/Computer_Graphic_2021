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


namespace AffineTransformations3D
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private int W, H;
        private int scale = 100;

        private int current;

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

        private void buttonHexahedron_Click(object sender, EventArgs e)
        {
            Polyhedron3D p = Hexahedron(scale);
            switch (current)
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
                default:
                    break;
            }

            Polyhedron3D axon = p.Axonometric();
            g.Clear(SystemColors.Control);
            DrawAxisAxonometric();
            ShowFigure(axon, Color.Black);
        }

        private void buttonPerspective_Click(object sender, EventArgs e)
        {
            Polyhedron3D p = Hexahedron(scale);
            switch (current)
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
                default:
                    break;
            }

            Polyhedron3D perspective = p.Perspective();
            g.Clear(SystemColors.Control);
            DrawAxisPerspective();
            ShowFigure(perspective, Color.Black);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            current = cmb.SelectedIndex;

            buttonAxonometric.Visible = true;
            buttonPerspective.Visible = true;
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

        public int Phi = 30;
        public int Psi = 30;

        public Polyhedron3D Axonometric()
        {
            Polyhedron3D res = new Polyhedron3D(this.polygons);
            double phi_r = (Math.PI / 180) * Phi;
            double psi_r = (Math.PI / 180) * Psi;

            foreach (Polygon3D p in polygons)
            {
                foreach (Line3D l in p.lines)
                {
                    Point3D p1 = l.first;
                    double x = p1.x;
                    double y = p1.y;
                    double z = p1.z;

                    /*p1.x = Math.Cos(psi_r) * x + Math.Sin(phi_r) * Math.Sin(psi_r) * y;
                    p1.y = Math.Cos(phi_r) * y;
                    p1.z = Math.Sin(psi_r) * x - Math.Sin(phi_r) * Math.Cos(psi_r) * y;*/

                    p1.x = (x - z) * Math.Cos(phi_r);
                    p1.y = y + (x + z) * Math.Sin(phi_r);

                    Point3D p2 = l.second;
                    x = p2.x;
                    y = p2.y;
                    z = p2.z;

                    /*p2.x = Math.Cos(psi_r) * x2 + Math.Sin(phi_r) * Math.Sin(psi_r) * y2;
                    p2.y = Math.Cos(phi_r) * y2;
                    p2.z = Math.Sin(psi_r) * x2 - Math.Sin(phi_r) * Math.Cos(psi_r) * y2;*/

                    p2.x = (x - z) * Math.Cos(phi_r);
                    p2.y = y + (x + z) * Math.Sin(phi_r);
                }
            }

            return res;
        }

        public Polyhedron3D Perspective()
        {
            Polyhedron3D res = new Polyhedron3D(this.polygons);

            foreach (Polygon3D p in polygons)
            {
                foreach (Line3D l in p.lines)
                {
                    Point3D p1 = l.first;
                    double x = p1.x;
                    double y = p1.y;
                    double z = p1.z;

                    double r = -0.1;
                    double k = 1000;

                    p1.x = (k * x) / (z + k);
                    p1.y = (k * y) / (z + k);

                    /*p1.x = x / (r * z + 1);
                    p1.y = y / (r * z + 1);*/

                    Point3D p2 = l.second;
                    x = p2.x;
                    y = p2.y;
                    z = p2.z;

                    p2.x = (k * x) / (z + k);
                    p2.y = (k * y) / (z + k);

                    /*p2.x = x / (r * z + 1);
                    p2.y = y / (r * z + 1);*/
                }
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
            Scale(sz, sy, sz);
            ApplyTransformation(GraphMath3D.TranslationMatrix(center.x, center.y, center.z));
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

            for (int i = 0; i < countStep; i++)
                for (int j = 0; j < countStep; j++)
                {
                    Line3D l1 = new Line3D(points[i * (countStep + 1) + j], points[i * (countStep + 1) + j + 1]);
                    Line3D l2 = new Line3D(points[i * (countStep + 1) + j + 1], points[(i + 1) * (countStep + 1) + (j + 1)]);
                    Line3D l3 = new Line3D(points[(i + 1) * (countStep + 1) + (j + 1)], points[(i + 1) * (countStep + 1) + j]);
                    Line3D l4 = new Line3D(points[(i + 1) * (countStep + 1) + j], points[i * (countStep + 1) + j]);
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
                                   {  0, 0,   0, 0}};
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
