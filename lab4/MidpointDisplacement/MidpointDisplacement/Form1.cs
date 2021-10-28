using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections;

namespace MidpointDisplacement
{
    public partial class Form1 : Form
    {
        private Graphics g;
        Random rand;
        private ArrayList points;
        
        private int R = 10;

        void MidpointDisplacement(int xb, int yb, int xe, int ye, int control = 0)
        {
            double l = Math.Sqrt((xe - xb) * (xe - xb) + (ye - yb) * (ye - yb));
            if (Width/2 > Convert.ToInt32(Math.Pow(2, control)))
            {
                control++;
                int xc = (xe + xb) / 2;
                int yc = ((Height/2 + yb) + (Height/2 + ye)) / 2 + 
                         rand.Next(Convert.ToInt32(-l / R), Convert.ToInt32(l / R) + 1);
                yc = -Height / 2 + yc;
                points.Add(new int[2] { xc, yc });
                MidpointDisplacement(xb, yb, xc, yc, control);
                MidpointDisplacement(xc, yc, xe, ye, control);
            }

        }

        public class comparePoints : IComparer
        {
            int IComparer.Compare(Object x, Object y)
            {
                int[] p1 = (int[])x;
                int[] p2 = (int[])y;
                return p1[0].CompareTo(p2[0]);
            }
        }

        private void DrawCurrentLine()
        {
            IComparer comp = new comparePoints();
            points.Sort(comp);
            Pen pen = new Pen(Color.Black);
            for (int i = 0; i < points.Count - 1; i++)
            {
                int[] first = (int[])points[i];
                int[] second = (int[])points[i + 1];
                g.DrawLine(pen, first[0] + Width/2, Height/2 - first[1], second[0] + Width/2, Height/2 - second[1]);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            g = this.CreateGraphics();
            rand = new Random();
            points = new ArrayList();
        }

        private void DrawRocks_Click(object sender, EventArgs e)
        {
            points.Clear();
            int xb = -Width / 2;
            int yb = -100;
            int xe = Width / 2;
            int ye = 150;

            points.Add(new int[2] { xb, yb });
            points.Add(new int[2] { xe, ye });
            MidpointDisplacement(xb, yb, xe, ye);

            g.Clear(SystemColors.Control);
            DrawCurrentLine();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            points.Clear();
            int xb = -Width / 2;
            int yb = -100;
            int xe = Width / 2;
            int ye = 150;

            points.Add(new int[2] { xb, yb });
            points.Add(new int[2] { xe, ye });
            MidpointDisplacementSteps(xb, yb, xe, ye);
        }

        void MidpointDisplacementSteps(int xb, int yb, int xe, int ye, int control = 0)
        {
            double l = Math.Sqrt((xe - xb) * (xe - xb) + (ye - yb) * (ye - yb));

            g.Clear(SystemColors.Control);
            DrawCurrentLine();
            Thread.Sleep(500);

            if (Width / 2 > Convert.ToInt32(Math.Pow(2, control)))
            {
                control++;
                int xc = (xe + xb) / 2;
                int yc = ((Height / 2 + yb) + (Height / 2 + ye)) / 2 +
                         rand.Next(Convert.ToInt32(-l / R), Convert.ToInt32(l / R) + 1);
                yc = -Height / 2 + yc;
                points.Add(new int[2] { xc, yc });
                MidpointDisplacementSteps(xb, yb, xc, yc, control);
                MidpointDisplacementSteps(xc, yc, xe, ye, control);
            }
        }
    }
}
