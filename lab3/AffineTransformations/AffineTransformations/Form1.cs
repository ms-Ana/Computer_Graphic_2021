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

namespace AffineTransformations
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private int W, H;
        private ArrayList currentFigure;
        private Point rotationPoint;
        private int rotationAngle;
        private Point dilationPoint;
        private ArrayList secondLine;

        private double[,] translationMatrix = { { 1, 0, 0 }, 
                                                { 0, 1, 0 }, 
                                                { 0, 0, 1 } };

        private double[,] rotationMatrix = { { 0, 0, 0 },
                                             { 0, 0, 0 },
                                             { 0, 0, 1 } };

        private double[,] dilationMatrix = { { 0, 0, 0 },
                                             { 0, 0, 0 },
                                             { 0, 0, 1 } };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            g = this.CreateGraphics();
            W = Width / 2; H = Height / 2;

            DrawAxis();
            currentFigure = new ArrayList();
            comboBoxtransformation.Visible = false;
            textBoxDx.Visible = false;
            textBoxDy.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            secondLine = new ArrayList();
            label1.Visible = false;
        }

        private void DrawAxis()
        {
            Pen pen = new Pen(Color.Black, 1);
            g.DrawLine(pen, 0, H, Width, H);
            g.DrawLine(pen, W, 0, W, Height);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Pen pen = new Pen(Color.Gray, 1);
            if (currentFigure.Count > 0)
            {
                double[] prev = (double[])currentFigure[currentFigure.Count - 1];
                g.DrawLine(pen, W + (int)prev[0], H - (int)prev[1], e.X, e.Y);
            }

            currentFigure.Add(new double [3] { e.X - W, H - e.Y, 1});
            g.DrawRectangle(pen, e.X, e.Y, 1, 1);
            buttonCurrent.Text = "Примитив задан";
        }

        private void Form1_MouseWheelRotate(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                rotationAngle += 10;
            }
            else
            {
                rotationAngle -= 10;
            }
            AffineRotation();
        }

        private void Form1_MouseSelectPoint(object sender, MouseEventArgs e)
        {
            rotationPoint = new Point(e.X - W, H - e.Y);
            g.Clear(SystemColors.Control);
            DrawAxis();
            DrawFigure(currentFigure, Color.FromArgb(255, 20, 20, 20));
            SolidBrush brush = new SolidBrush(Color.Aqua);
            g.FillRectangle(brush, W + rotationPoint.X - 1, H - rotationPoint.Y - 1, 3, 3);
        }

        private void Form1_MouseSelectPointDilation(object sender, MouseEventArgs e)
        {
            dilationPoint = new Point(e.X - W, H - e.Y);
            g.Clear(SystemColors.Control);
            DrawAxis();
            DrawFigure(currentFigure, Color.FromArgb(255, 20, 20, 20));
            SolidBrush brush = new SolidBrush(Color.Aqua);
            g.FillRectangle(brush, W + dilationPoint.X - 1, H - dilationPoint.Y - 1, 3, 3);
            AffineDilation();
        }

        private void Form1_MouseFindIntersection(object sender, MouseEventArgs e)
        {
            label1.Visible = false;
            Pen pen = new Pen(Color.Aqua, 1);
            if (secondLine.Count == 0)
            {
                secondLine.Add(new double[3] { e.X - W, H - e.Y, 1 });
                g.DrawRectangle(pen, e.X, e.Y, 1, 1);
            }
            else if (secondLine.Count == 1)
            {
                secondLine.Add(new double[3] { e.X - W, H - e.Y, 1 });
                double[] first = (double[])secondLine[0];
                g.DrawLine(pen, W + (int)first[0], H - (int)first[1], e.X, e.Y);
                Intersection();
            }
            else
            {
                secondLine.Clear();
                g.Clear(SystemColors.Control);
                DrawAxis();
                DrawFigure(currentFigure, Color.FromArgb(255, 20, 20, 20));
                secondLine.Add(new double[3] { e.X - W, H - e.Y, 1 });
                g.DrawRectangle(pen, e.X, e.Y, 1, 1);
            }
        }

        private void buttonCurrent_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "Задать текущий примитив")
            {
                currentFigure.Clear();
                g.Clear(SystemColors.Control);
                DrawAxis();
                MouseClick += Form1_MouseClick;
            }
            else
            {
                button.Text = "Задать текущий примитив";
                MouseClick -= Form1_MouseClick;
                if (currentFigure.Count > 2)
                {
                    Pen pen = new Pen(Color.Gray, 1);
                    double[] first = (double[])currentFigure[0];
                    double[] last = (double[])currentFigure[currentFigure.Count - 1];
                    g.DrawLine(pen, (int)first[0] + W, H - (int)first[1], (int)last[0] + W, H - (int)last[1]);
                }
                comboBoxtransformation.Visible = true;
            }
        }

        private void AffineTranslation()
        {
            if (!String.IsNullOrEmpty(textBoxDx.Text) && !String.IsNullOrEmpty(textBoxDy.Text))
            {
                int dx, dy;
                bool dxSuccess = Int32.TryParse(textBoxDx.Text, out dx);
                bool dySuccess = Int32.TryParse(textBoxDy.Text, out dy);
                if (dxSuccess && dySuccess)
                {
                    translationMatrix[2, 0] = dx;
                    translationMatrix[2, 1] = dy;
                    ArrayList newFigure = Transform(currentFigure, translationMatrix);

                    g.Clear(SystemColors.Control);
                    DrawAxis();
                    DrawFigure(currentFigure, Color.FromArgb(255, 20, 20, 20));
                    DrawFigure(newFigure, Color.Red);
                }
            }
        }

        private void AffineRotation()
        {
            if (rotationPoint != null)
            {
                int dx = rotationPoint.X;
                int dy = rotationPoint.Y;
                translationMatrix[2, 0] = -dx;
                translationMatrix[2, 1] = -dy;
                ArrayList newFigure = Transform(currentFigure, translationMatrix);

                double rad = (Math.PI / 180) * rotationAngle;
                double cosFi = Math.Cos(rad);
                double sinFi = Math.Sin(rad);
                rotationMatrix[0, 0] = cosFi;
                rotationMatrix[0, 1] = sinFi;
                rotationMatrix[1, 0] = -sinFi;
                rotationMatrix[1, 1] = cosFi;
                newFigure = Transform(newFigure, rotationMatrix);

                translationMatrix[2, 0] = dx;
                translationMatrix[2, 1] = dy;
                newFigure = Transform(newFigure, translationMatrix);

                g.Clear(SystemColors.Control);
                DrawAxis();
                DrawFigure(currentFigure, Color.FromArgb(255, 20, 20, 20));
                DrawFigure(newFigure, Color.Red);
                SolidBrush brush = new SolidBrush(Color.Aqua);
                g.FillRectangle(brush, W + rotationPoint.X - 1, H - rotationPoint.Y - 1, 3, 3);
            }
        }

        private void AffineDilation()
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text))
            {
                int dx = dilationPoint.X;
                int dy = dilationPoint.Y;
                translationMatrix[2, 0] = -dx;
                translationMatrix[2, 1] = -dy;
                ArrayList newFigure = Transform(currentFigure, translationMatrix);

                double alpha, beta;
                bool alphaSuccess = Double.TryParse(textBox1.Text, out alpha);
                bool betaSuccess = Double.TryParse(textBox2.Text, out beta);
                if (alphaSuccess && betaSuccess)
                {
                    dilationMatrix[0, 0] = alpha;
                    dilationMatrix[1, 1] = beta;
                    newFigure = Transform(newFigure, dilationMatrix);

                    translationMatrix[2, 0] = dx;
                    translationMatrix[2, 1] = dy;
                    newFigure = Transform(newFigure, translationMatrix);

                    g.Clear(SystemColors.Control);
                    DrawAxis();
                    DrawFigure(currentFigure, Color.FromArgb(255, 20, 20, 20));
                    DrawFigure(newFigure, Color.Red);
                    SolidBrush brush = new SolidBrush(Color.Aqua);
                    g.FillRectangle(brush, W + dilationPoint.X - 1, H - dilationPoint.Y - 1, 3, 3);
                }
            }
        }

        private void Intersection()
        {
            if (currentFigure.Count != 2)
            {
                return;
            }
            double[] line1First =  (double[])currentFigure[0];   // x1, y1
            double[] line1Second = (double[])currentFigure[1];   // x2, y2
            double[] line2First =  (double[])secondLine[0];      // x3, y3
            double[] line2Second = (double[])secondLine[1];      // x4, y4

            double x1 = line1First[0];  double y1 = line1First[1];
            double x2 = line1Second[0]; double y2 = line1Second[1];
            double x3 = line2First[0];  double y3 = line2First[1];
            double x4 = line2Second[0]; double y4 = line2Second[1];

            double den = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
            double num1 = (x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3);
            double num2 = (x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3);

            if (den == 0)
            {
                if (num1 == 0 || num2 == 0)
                {
                    label1.Visible = true;
                    label1.Text = "Отрезки совпадают";
                    return;
                }
                else
                {
                    label1.Visible = true;
                    label1.Text = "Отрезки параллельны";
                    return;
                }
            }

            double t1 = num1 / den;
            double t2 = num2 / den;
            if (t1 < 0 || t1 > 1 || t2 < 0 || t2 > 1)
            {
                label1.Visible = true;
                label1.Text = "Отрезки не пересекаются";
                return;
            }

            double x = x1 + t1 * (x2 - x1);
            double y = y1 + t1 * (y2 - y1);
            SolidBrush brush = new SolidBrush(Color.Red);
            g.FillRectangle(brush, W + (int)x - 1, H - (int)y - 1, 3, 3);
        }

        private ArrayList Transform(ArrayList figure, double[,] matrix)
        {
            ArrayList newFigure = new ArrayList();
            for (int i = 0; i < figure.Count; i++)
            {
                double[] curr = (double[])figure[i];
                double x = curr[0] * matrix[0, 0] + curr[1] * matrix[1, 0] + curr[2] * matrix[2, 0];
                double y = curr[0] * matrix[0, 1] + curr[1] * matrix[1, 1] + curr[2] * matrix[2, 1];
                newFigure.Add(new double[3] { x, y, 1 });
            }
            return newFigure;
        }

        private void DrawFigure(ArrayList figure, Color color)
        {
            double[] prev = (double[])figure[0];
            Pen pen = new Pen(color, 1);
            if (figure.Count == 1)
            {
                g.DrawRectangle(pen, (int)prev[0] + W, H - (int)prev[1], 1, 1);
                return;
            }
            for (int i = 1; i < figure.Count; i++)
            {
                double[] curr = (double[])figure[i];
                g.DrawLine(pen, (int)prev[0] + W, H - (int)prev[1], (int)curr[0] + W, H - (int)curr[1]);
                prev = curr;
            }
            if (figure.Count > 2)
            {
                double[] first = (double[])figure[0];
                double[] last = (double[])figure[figure.Count - 1];
                g.DrawLine(pen, (int)first[0] + W, H - (int)first[1], (int)last[0] + W, H - (int)last[1]);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            g.Clear(SystemColors.Control);
            currentFigure.Clear();
            comboBoxtransformation.Visible = false;
            textBoxDx.Visible = false;
            textBoxDx.Text = "";
            textBoxDy.Visible = false;
            textBoxDy.Text = "";
            textBox1.Visible = false;
            textBox1.Text = "";
            textBox2.Visible = false;
            textBox2.Text = "";
            label1.Visible = false;
            MouseClick -= Form1_MouseSelectPoint;
            MouseWheel -= Form1_MouseWheelRotate;
            MouseClick -= Form1_MouseSelectPointDilation;
            MouseClick -= Form1_MouseFindIntersection;
            DrawAxis();
        }

        private void comboBoxtransformation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            ComboBox cmb = (ComboBox)sender;
            if (cmb.SelectedIndex == 0)
            {
                textBoxDx.Visible = true;
                textBoxDy.Visible = true;
                label1.Visible = false;
                MouseClick -= Form1_MouseSelectPoint;
                MouseWheel -= Form1_MouseWheelRotate;
                MouseClick -= Form1_MouseSelectPointDilation;
                MouseClick -= Form1_MouseFindIntersection;
            }
            else if (cmb.SelectedIndex == 1)
            {
                rotationPoint = new Point();
                label1.Visible = false;
                rotationAngle = 0;
                MouseClick += Form1_MouseSelectPoint;
                MouseWheel += Form1_MouseWheelRotate;
                MouseClick -= Form1_MouseSelectPointDilation;
                MouseClick -= Form1_MouseFindIntersection;
            }
            else if (cmb.SelectedIndex == 2)
            {
                dilationPoint = new Point();
                textBox1.Visible = true;
                textBox2.Visible = true;
                label1.Visible = false;
                MouseClick -= Form1_MouseSelectPoint;
                MouseWheel -= Form1_MouseWheelRotate;
                MouseClick += Form1_MouseSelectPointDilation;
                MouseClick -= Form1_MouseFindIntersection;
            }
            else
            {
                secondLine.Clear();
                label1.Visible = false;
                MouseClick -= Form1_MouseSelectPoint;
                MouseWheel -= Form1_MouseWheelRotate;
                MouseClick -= Form1_MouseSelectPointDilation;
                MouseClick += Form1_MouseFindIntersection;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AffineTranslation();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            AffineTranslation();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            AffineDilation();
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            AffineDilation();
        }

        private void Clear()
        {
            g.Clear(SystemColors.Control);
            textBoxDx.Visible = false;
            textBoxDx.Text = "";
            textBoxDy.Visible = false;
            textBoxDy.Text = "";
            textBox1.Visible = false;
            textBox1.Text = "";
            textBox2.Visible = false;
            textBox2.Text = "";
            label1.Visible = false;
            MouseClick -= Form1_MouseSelectPoint;
            MouseWheel -= Form1_MouseWheelRotate;
            MouseClick -= Form1_MouseSelectPointDilation;
            MouseClick -= Form1_MouseFindIntersection;
            DrawAxis();
            DrawFigure(currentFigure, Color.FromArgb(255, 20, 20, 20));
        }
    }
}
