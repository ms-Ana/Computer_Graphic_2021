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

namespace SolidsOfRevolution
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private int W, H;
        private int scale = 100;
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

            textBoxRotations.Visible = false;
            buttonX.Visible = false;
            buttonY.Visible = false;
            buttonZ.Visible = false;
            points = new List<Point>();
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
            buttonAddPoints.Text = "Готово";
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

            Polyhedron3D res = new Polyhedron3D(sides);
            Polyhedron3D axon = res.Axonometric();
            g.Clear(SystemColors.Control);
            ShowFigure(axon);
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

            Polyhedron3D res = new Polyhedron3D(sides);
            Polyhedron3D axon = res.Axonometric();
            g.Clear(SystemColors.Control);
            ShowFigure(axon);
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

            Polyhedron3D res = new Polyhedron3D(sides);
            Polyhedron3D axon = res.Axonometric();
            g.Clear(SystemColors.Control);
            ShowFigure(axon);
        }

        private void ShowFigure(Polyhedron3D polyhedron)
        {
            Pen pen = new Pen(Color.Black);
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

                    p1.x = (x - z) * Math.Cos(phi_r);
                    p1.y = y + (x + z) * Math.Sin(phi_r);

                    Point3D p2 = l.second;
                    x = p2.x;
                    y = p2.y;
                    z = p2.z;

                    p2.x = (x - z) * Math.Cos(phi_r);
                    p2.y = y + (x + z) * Math.Sin(phi_r);
                }
            }

            return res;
        }
    }
}
