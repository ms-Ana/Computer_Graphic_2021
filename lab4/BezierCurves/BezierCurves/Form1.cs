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

namespace BezierCurves
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private int W, H;
        private const double rate = 0.05;
        private ArrayList points;
        private double[] currentPoint;

        private bool hasCurrentPoint;
        private bool canChangePoint;
        private bool changingPoint;
        private int changingIndex;
        private bool canMovePoint;
        private bool movingPoint;
        private bool deletingPoints;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            g = this.CreateGraphics();
            W = Width / 2; H = Height / 2;

            points = new ArrayList();
            currentPoint = new double[2];
            hasCurrentPoint = false;
            changingPoint = false;
            canChangePoint = false;
            changingIndex = -1;
            canMovePoint = false;
            movingPoint = false;
            deletingPoints = false;
            
            MouseDown += Form1_MouseDown;
            MouseMove += Form1_MouseMove;
            MouseUp += Form1_MouseUp;
            buttonDelete.Visible = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (deletingPoints)
            {
                int currX = e.X - W;
                int currY = H - e.Y;
                for (int i = 0; i < points.Count - 1; i += 2)
                {
                    double[] first = (double[])points[i];
                    double[] second = (double[])points[i + 1];
                    double midX = 0.5 * first[0] + 0.5 * second[0];
                    double midY = 0.5 * first[1] + 0.5 * second[1];
                    if (Math.Abs(midX - currX) <= 10 && Math.Abs(midY - currY) <= 10)
                    {
                        points.RemoveAt(i);
                        points.RemoveAt(i);
                        g.Clear(SystemColors.Control);
                        DrawCurrentFigure();
                        break;
                    }
                }
                return;
            }

            if (canMovePoint)
            {
                movingPoint = true;
                return;
            }

            if (canChangePoint)
            {
                changingPoint = true;
                return;
            }

            if (hasCurrentPoint)
            {
                return;
            }
            hasCurrentPoint = true;
            SolidBrush brush = new SolidBrush(Color.Black);
            g.FillEllipse(brush, e.X - 1, e.Y - 1, 3, 3);
            points.Add(new double[2] { e.X - W, H - e.Y });
            points.Add(new double[2] { e.X - W, H - e.Y });
            currentPoint[0] = e.X - W;
            currentPoint[1] = H - e.Y;

            if (points.Count >= 4)
            {
                double[] p1 = (double[])points[points.Count - 4];
                double[] p2 = (double[])points[points.Count - 3];
                double[] p3 = (double[])points[points.Count - 2];
                double[] p4 = (double[])points[points.Count - 1];
                DrawBezierCurve(p1, p2, p3, p4);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (movingPoint && changingIndex >= 0)
            {
                double[] first = (double[])points[changingIndex];
                double[] second = (double[])points[changingIndex + 1];
                double midX = 0.5 * first[0] + 0.5 * second[0];
                double midY = 0.5 * first[1] + 0.5 * second[1];
                double dX = midX - (e.X - W);
                double dY = midY - (H - e.Y);
                points.RemoveAt(changingIndex);
                points.RemoveAt(changingIndex);
                points.Insert(changingIndex, new double[2] { second[0] - dX, second[1] - dY });
                points.Insert(changingIndex, new double[2] { first[0] - dX, first[1] - dY });
                g.Clear(SystemColors.Control);
                DrawCurrentFigure();
                return;
            }

            if (changingPoint && changingIndex >= 0)
            {
                double[] first = (double[])points[changingIndex];
                double[] second;
                if (changingIndex % 2 == 0)
                {
                    second = (double[])points[changingIndex + 1];
                }
                else
                {
                    second = (double[])points[changingIndex - 1];
                }
                double midX = 0.5 * first[0] + 0.5 * second[0];
                double midY = 0.5 * first[1] + 0.5 * second[1];
                double dX = midX - (e.X - W);
                double dY = midY - (H - e.Y);
                int secondX = (int)(midX + dX) + W;
                int secondY = H - (int)(midY + dY);
                if (changingIndex % 2 == 0)
                {
                    points.RemoveAt(changingIndex);
                    points.RemoveAt(changingIndex);
                    points.Insert(changingIndex, new double[2] { secondX - W, H - secondY });
                    points.Insert(changingIndex, new double[2] { e.X - W, H - e.Y });
                }
                else
                {
                    points.RemoveAt(changingIndex - 1);
                    points.RemoveAt(changingIndex - 1);
                    points.Insert(changingIndex - 1, new double[2] { e.X - W, H - e.Y });
                    points.Insert(changingIndex - 1, new double[2] { secondX - W, H - secondY });
                }
                g.Clear(SystemColors.Control);
                DrawCurrentFigure();
                return;
            }

            int currX = e.X - W;
            int currY = H - e.Y;
            for (int i = 0; i < points.Count - 1; i += 2)
            {
                double[] first = (double[])points[i];
                double[] second = (double[])points[i + 1];
                double midX = 0.5 * first[0] + 0.5 * second[0];
                double midY = 0.5 * first[1] + 0.5 * second[1];
                if (Math.Abs(midX - currX) <= 10 && Math.Abs(midY - currY) <= 10)
                {
                    Cursor.Current = Cursors.Hand;
                    canMovePoint = true;
                    changingIndex = i;
                    break;
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    canMovePoint = false;
                    changingIndex = -1;
                }
            }

            if (!canMovePoint)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    double[] p = (double[])points[i];
                    if (Math.Abs(p[0] - currX) <= 10 && Math.Abs(p[1] - currY) <= 10)
                    {
                        Cursor.Current = Cursors.Hand;
                        canChangePoint = true;
                        changingIndex = i;
                        break;
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        canChangePoint = false;
                        changingIndex = -1;
                    }
                }
            }

            if (!hasCurrentPoint)
            {
                return;
            }

            g.Clear(SystemColors.Control);
            double distanceX = currentPoint[0] - (e.X - W);
            double distanceY = currentPoint[1] - (H - e.Y);
            int nextX = (int)(currentPoint[0] + distanceX) + W;
            int nextY = H - (int)(currentPoint[1] + distanceY);
            points.RemoveAt(points.Count - 1);
            points.RemoveAt(points.Count - 1);
            points.Add(new double[2] { nextX - W, H - nextY });
            points.Add(new double[2] { e.X - W, H - e.Y });
            DrawCurrentFigure();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            hasCurrentPoint = false;
            changingPoint = false;
            movingPoint = false;

            if (points.Count > 0)
            {
                buttonDelete.Visible = true;
            }
            else
            {
                currentPoint = new double[2];
                hasCurrentPoint = false;
                canChangePoint = false;
                changingPoint = false;
                changingIndex = -1;
                canMovePoint = false;
                movingPoint = false;
                deletingPoints = false;
                buttonDelete.Text = "Удалить точку";
                buttonDelete.Visible = false;
            }
        }

        private void DrawBezierCurve(double[] p1, double[] p2, double[] p3, double[] p4)
        {
            double newX = 0.5 * p1[0] + 0.5 * p2[0];
            double newY = 0.5 * p1[1] + 0.5 * p2[1];
            double[] newP1 = new double[2];
            newP1[0] = newX; newP1[1] = newY;
            newX = 0.5 * p3[0] + 0.5 * p4[0];
            newY = 0.5 * p3[1] + 0.5 * p4[1];
            double[] newP4 = new double[2];
            newP4[0] = newX; newP4[1] = newY;

            Pen pen = new Pen(Color.Red);
            double lastX = newP1[0];
            double lastY = newP1[1];
            double currX, currY;
            for (double t = 0; t <= 1 + rate; t += rate)
            {
                currX = (1 - t) * (1 - t) * (1 - t) * newP1[0] + 
                        3 * (1 - t) * (1 - t) * t * p2[0] + 
                        3 * (1 - t) * t * t * p3[0] + t * t * t * newP4[0];
                currY = (1 - t) * (1 - t) * (1 - t) * newP1[1] + 
                        3 * (1 - t) * (1 - t) * t * p2[1] + 
                        3 * (1 - t) * t * t * p3[1] + t * t * t * newP4[1];
                g.DrawLine(pen, (int)lastX + W, H - (int)lastY, (int)currX + W, H - (int)currY);
                lastX = currX; lastY = currY;
            }
        }

        private void DrawCurrentFigure()
        {
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush grayBrush = new SolidBrush(Color.FromArgb(255, 200, 200, 200));
            Pen pen = new Pen(Color.FromArgb(255, 200, 200, 200));
            for (int i = 0; i < points.Count - 1; i += 2)
            {
                double[] first = (double[])points[i];
                double[] second = (double[])points[i + 1];
                g.FillEllipse(grayBrush, (int)first[0] + W - 1, H - (int)first[1] - 1, 3, 3);
                g.FillEllipse(grayBrush, (int)second[0] + W - 1, H - (int)second[1] - 1, 3, 3);
                g.DrawLine(pen, (int)first[0] + W, H - (int)first[1], (int)second[0] + W, H - (int)second[1]);
                double newX = 0.5 * first[0] + 0.5 * second[0];
                double newY = 0.5 * first[1] + 0.5 * second[1];
                g.FillEllipse(blackBrush, (int)newX + W - 1, H - (int)newY - 1, 3, 3);
            }

            if (points.Count >= 4)
            {
                for (int i = 0; i < points.Count - 2; i += 2)
                {
                    double[] p1 = (double[])points[i];
                    double[] p2 = (double[])points[i + 1];
                    double[] p3 = (double[])points[i + 2];
                    double[] p4 = (double[])points[i + 3];
                    DrawBezierCurve(p1, p2, p3, p4);
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "Удалить точку")
            {
                deletingPoints = true;
                button.Text = "Завершить удаление";
            }
            else
            {
                deletingPoints = false;
                button.Text = "Удалить точку";
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            g.Clear(SystemColors.Control);
            points.Clear();
            currentPoint = new double[2];
            hasCurrentPoint = false;
            canChangePoint = false;
            changingPoint = false;
            changingIndex = -1;
            canMovePoint = false;
            movingPoint = false;
            deletingPoints = false;
            buttonDelete.Text = "Удалить точку";
            buttonDelete.Visible = false;
        }
    }
}
