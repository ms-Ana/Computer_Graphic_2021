using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bresenham
{
    public partial class Form1 : Form
    {
        private Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        //Метод, устанавливающий пиксел на форме с заданными цветом и прозрачностью
        private static void PutPixel(Graphics g, Color col, int x, int y, int alpha)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, col)), x, y, 1, 1);
        }

        static public void Bresenham4Line(Graphics g, Color clr, int x0, int y0,
                                                                 int x1, int y1)
        {
            int dy = y1 - y0;
            int dx = x1 - x0;
            int x = x0;
            int y = y0;
            if (dy > dx)
            {
                int d = 2 * dx - dy;
                PutPixel(g, Color.Black, x, y, 255);
                for (int i = 0; i <= dy; i++)
                {
                    if (d < 0)
                    {
                        d += 2 * dx;
                    }
                    else
                    {
                        x++;
                        d += 2 * (dx - dy);
                    }
                    y++;
                    PutPixel(g, Color.Black, x, y, 255);
                }
            }
            else
            {
                int d = 2 * dx - dy;
                PutPixel(g, Color.Black, x, y, 255);
                for (int i = 0; i <= dx; i++)
                {
                    if (d < 0)
                    {
                        d += 2 * dy;
                    }
                    else
                    {
                        y++;
                        d += 2 * (dy - dx);
                    }
                    x++;
                    PutPixel(g, Color.Black, x, y, 255);
                }
            }
        }

        static public void Wu(Graphics g, Color clr, int x1, int y1, int x0, int y0)
        {
            PutPixel(g, Color.Black, x1, y1, 255);
            float dx = x1 - x0; float dy = y1 - y0;
            float gradient = dy / dx;
            float y = y0 + gradient;
            for (var x = x0 + 1; x <= x1 - 1; x++)
            {
                PutPixel(g, Color.Black, x, (int)y, 1 - ((int)y - (int)y));
                PutPixel(g, Color.Black, x, (int)y + 1, (int)y - (int)y);
                y += gradient;
            }
        }

        static public void Bresenham4LineWu(Graphics g, Color clr, int x0, int y0,
                                                                 int x1, int y1)
        {
            int dy = y1 - y0;
            int dx = x1 - x0;
            int x = x0;
            int y = y0;
            if (dy > dx)
            {
                int d = 2 * dx - dy;
                PutPixel(g, Color.Black, x, y, 255);
                for (int i = 0; i <= dy; i++)
                {
                    int xx = x;
                    int yy = y;
                    if (d < 0)
                    {
                        d += 2 * dx;
                    }
                    else
                    {
                        x++;
                        d += 2 * (dx - dy);
                    }
                    y++;
                    PutPixel(g, Color.Black, x, y, 255);
                    Wu(g, Color.Black, xx, yy, x, y);
                }
            }
            else
            {
                int d = 2 * dx - dy;
                PutPixel(g, Color.Black, x, y, 255);
                for (int i = 0; i <= dx; i++)
                {
                    int xx = x;
                    int yy = y;
                    if (d < 0)
                    {
                        d += 2 * dy;
                    }
                    else
                    {
                        y++;
                        d += 2 * (dy - dx);
                    }
                    x++;
                    PutPixel(g, Color.Black, x, y, 255);
                    Wu(g, Color.Black, xx, yy, x, y);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(SystemColors.Control);
            int x1 = 100;
            int y1 = 120;
            int x2 = 400;
            int y2 = 190;
            Brush brush = new SolidBrush(Color.Red);
            g.FillRectangle(brush, x1 - 1, y1 - 1, 3, 3);
            g.FillRectangle(brush, x2 - 1, y2 - 1, 3, 3);
            Bresenham4Line(g, Color.Black, x1, y1, x2, y2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = this.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(SystemColors.Control);
            int x1 = 100;
            int y1 = 120;
            int x2 = 400;
            int y2 = 190;
            Brush brush = new SolidBrush(Color.Red);
            g.FillRectangle(brush, x1 - 1, y1 - 1, 3, 3);
            g.FillRectangle(brush, x2 - 1, y2 - 1, 3, 3);
            Bresenham4LineWu(g, Color.Black, x1, y1, x2, y2);
        }
    }
}
