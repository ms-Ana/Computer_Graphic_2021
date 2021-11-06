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
    }
}
