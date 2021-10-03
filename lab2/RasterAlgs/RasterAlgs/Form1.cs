using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace RasterAlgs
{
    public partial class Form1 : Form
    {
        private Graphics g;
        Color newColor;
        Color oldColor;
        Bitmap originalImage;
        Bitmap currentImage;
        Bitmap imageToFill;
        bool drawSlow;
        int currentAlg;

        HashSet<Point> filled;
        List<Point> p;
        int startX, startY;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            g = this.CreateGraphics();
            newColor = Color.Red;
            buttonFillImage.Visible = false;
            drawSlow = false;
            filled = new HashSet<Point>();
            p = new List<Point>();
            this.MouseClick += Form1_MouseClick;
            buttonBorder.Visible = false;
        }

        void DrawBorderFromList()
        {
            int[] addX = { 0, -1, -1, -1, 0, 1, 1, 1 };
            int[] addY = { 1, 1, 0, -1, -1, -1, 0, 1 };
            foreach (Point point in p)
            {
                for (int i = 0; i < 8; i++)
                {
                    currentImage.SetPixel(point.X + addX[i], point.Y + addY[i], Color.Black);
                }
            }
            g.DrawImage(currentImage, 200, 50);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentAlg == 0)
            {
                oldColor = currentImage.GetPixel(e.X - 200, e.Y - 50);
                FillLines(e.X - 200, e.Y - 50);
                g.DrawImage(currentImage, 200, 50);
            }
            else if (currentAlg == 1)
            {
                oldColor = currentImage.GetPixel(e.X - 200, e.Y - 50);
                startX = e.X - 200;
                startY = e.Y - 50;
                FillLinesImage(e.X - 200, e.Y - 50);
                g.DrawImage(currentImage, 200, 50);
            }
            else
            {
                oldColor = currentImage.GetPixel(e.X - 200, e.Y - 50);
                FindBorderOfImage(e.X - 200, e.Y - 50);
                g.DrawImage(currentImage, 200, 50);
            }
        }

        private void FillLines(int x, int y)
        {
            if (x < 0 || x >= currentImage.Width || y < 0 || y >= currentImage.Height)
            {
                return;
            }
            Color c = currentImage.GetPixel(x, y);
            if ((c.R == newColor.R && c.G == newColor.G && c.B == newColor.B) 
                || (c.R != oldColor.R && c.G != oldColor.G && c.B != oldColor.B))
            {
                return;
            }

            int left, right;
            int i = x;
            while (true)
            {
                if (i == 0)
                {
                    left = 0;
                    break;
                }
                Color col = currentImage.GetPixel(i, y);
                if (col.R != oldColor.R && col.G != oldColor.G && col.B != oldColor.B)
                {
                    left = i + 1;
                    break;
                }
                currentImage.SetPixel(i, y, newColor);
                i--;
            }
            if (drawSlow)
            {
                g.DrawImage(currentImage, 200, 50);
            }

            i = x;
            while (true)
            {
                if (i == currentImage.Width)
                {
                    right = currentImage.Width;
                    break;
                }
                Color col = currentImage.GetPixel(i, y);
                if (col.R != oldColor.R && col.G != oldColor.G && col.B != oldColor.B)
                {
                    right = i - 1;
                    break;
                }
                currentImage.SetPixel(i, y, newColor);
                i++;
            }
            if (drawSlow)
            {
                g.DrawImage(currentImage, 200, 50);
            }

            for (int j = left; j <= right; j++)
            {
                FillLines(j, y - 1);
            }
            for (int j = left; j <= right; j++)
            {
                FillLines(j, y + 1);
            }
        }

        private void FillLinesImage(int x, int y)
        {
            if (x < 0 || x >= currentImage.Width || y < 0 || y >= currentImage.Height)
            {
                return;
            }
            if (filled.Contains(new Point(x, y)))
            {
                return;
            }
            Color c = currentImage.GetPixel(x, y);
            if (c.R != oldColor.R && c.G != oldColor.G && c.B != oldColor.B)
            {
                return;
            }

            int left, right;
            int i = x;
            int fillX = imageToFill.Width / 2 - 1;
            int fillY = imageToFill.Height / 2 - 1;

            while (true)
            {
                if (filled.Contains(new Point(i, y)))
                {
                    left = i;
                    break;
                }
                if (i == 0)
                {
                    left = 0;
                    break;
                }
                Color col = currentImage.GetPixel(i, y);
                if (col.R != oldColor.R && col.G != oldColor.G && col.B != oldColor.B)
                {
                    left = i + 1;
                    break;
                }
                int colorToFillX, colorToFillY;
                if (fillX + i - startX < 0)
                {
                    colorToFillX = imageToFill.Width + (fillX + i - startX) % imageToFill.Width - 1;
                }
                else if (fillX + i - startX >= imageToFill.Width)
                {
                    colorToFillX = (fillX + i - startX) % imageToFill.Width;
                }
                else
                {
                    colorToFillX = fillX + i - startX;
                }

                if (fillY + startY - y < 0)
                {
                    colorToFillY = imageToFill.Height + (fillY + startY - y) % imageToFill.Height - 1;
                }
                else if (fillY + startY - y >= imageToFill.Height)
                {
                    colorToFillY = (fillY + startY - y) % imageToFill.Height;
                }
                else
                {
                    colorToFillY = fillY + startY - y;
                }
                
                currentImage.SetPixel(i, y, imageToFill.GetPixel(colorToFillX, colorToFillY));
                filled.Add(new Point(i, y));
                i--;
            }
            if (drawSlow)
            {
                g.DrawImage(currentImage, 200, 50);
            }

            i = x + 1;
            while (true)
            {
                if (filled.Contains(new Point(i, y)))
                {
                    right = i;
                    break;
                }
                if (i == currentImage.Width)
                {
                    right = currentImage.Width;
                    break;
                }
                Color col = currentImage.GetPixel(i, y);
                if (col.R != oldColor.R && col.G != oldColor.G && col.B != oldColor.B)
                {
                    right = i - 1;
                    break;
                }
                int colorToFillX, colorToFillY;
                if (fillX + i - startX < 0)
                {
                    colorToFillX = imageToFill.Width + (fillX + i - startX) % imageToFill.Width - 1;
                }
                else if (fillX + i - startX >= imageToFill.Width)
                {
                    colorToFillX = (fillX + i - startX) % imageToFill.Width;
                }
                else
                {
                    colorToFillX = fillX + i - startX;
                }

                if (fillY + startY - y < 0)
                {
                    colorToFillY = imageToFill.Height + (fillY + startY - y) % imageToFill.Height - 1;
                }
                else if (fillY + startY - y >= imageToFill.Height)
                {
                    colorToFillY = (fillY + startY - y) % imageToFill.Height;
                }
                else
                {
                    colorToFillY = fillY + startY - y;
                }

                currentImage.SetPixel(i, y, imageToFill.GetPixel(colorToFillX, colorToFillY));
                filled.Add(new Point(i, y));
                i++;
            }
            if (drawSlow)
            {
                g.DrawImage(currentImage, 200, 50);
            }

            for (int j = left; j <= right; j++)
            {
                FillLinesImage(j, y - 1);
            }
            for (int j = left; j <= right; j++)
            {
                FillLinesImage(j, y + 1);
            }
        }

        private void FindBorderOfImage(int x, int y)
        {
            int right = x;
            while (true)
            {
                if (right == currentImage.Width)
                {
                    break;
                }
                Color col = currentImage.GetPixel(right, y);
                if (col.R != oldColor.R && col.G != oldColor.G && col.B != oldColor.B)
                {
                    break;
                }
                right++;
            }

            List<Point> points = new List<Point>();
            points.Add(new Point(right, y));
            currentImage.SetPixel(right, y, Color.Black);
            if (drawSlow)
            {
                g.DrawImage(currentImage, 200, 50);
            }
            int currX = right; int currY = y;
            Color c; bool flag; bool endFlag = false;
            int[] addX = { 0, -1, -1, -1, 0, 1, 1, 1 };
            int[] addY = { 1, 1, 0, -1, -1, -1, 0, 1 };
            int dir = 0;
            while (true)
            {
                if (endFlag)
                {
                    break;
                }
                flag = false;
                if (dir == -2)
                {
                    dir = 6;
                }
                else if (dir == -1)
                {
                    dir = 7;
                }
                for (int i = 0; i < 8; i++)
                {
                    dir %= 8;
                    if (endFlag)
                    {
                        break;
                    }
                    if (points.Contains(new Point(currX + addX[dir], currY + addY[dir])))
                    {
                        if (currX + addX[dir] == right && currY + addY[dir] == y)
                        {
                            endFlag = true;
                        }
                        continue;
                    }
                    c = currentImage.GetPixel(currX + addX[dir], currY + addY[dir]);
                    if (c.R != oldColor.R && c.G != oldColor.G && c.B != oldColor.B)
                    {
                        for (int j = 0; j < 7; j+= 2)
                        {
                            c = currentImage.GetPixel(currX + addX[dir] + addX[j], currY + addY[dir] + addY[j]);
                            if (c.R == oldColor.R && c.G == oldColor.G && c.B == oldColor.B)
                            {
                                currX += addX[dir]; currY += addY[dir];
                                points.Add(new Point(currX, currY));
                                currentImage.SetPixel(currX, currY, Color.Black);
                                points.Add(new Point(currX, currY));
                                if (drawSlow)
                                {
                                    g.DrawImage(currentImage, 200, 50);
                                }
                                flag = true;
                                dir -= 2;
                                break;
                            }
                        }
                        if (flag)
                        {
                            break;
                        }
                        dir++;
                    }
                }
                
            }
            p.AddRange(points);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            if (cmb.SelectedIndex == 0)
            {
                buttonFillImage.Visible = false;
                currentAlg = 0;
                buttonBorder.Visible = false;
            }
            else if (cmb.SelectedIndex == 1)
            {
                buttonFillImage.Visible = true;
                currentAlg = 1;
                buttonBorder.Visible = false;
            }
            else
            {
                buttonFillImage.Visible = false;
                currentAlg = 2;
                buttonBorder.Visible = true;
            }
        }

        private void buttonChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                currentImage = new Bitmap(open.FileName);
                originalImage = new Bitmap(open.FileName);
                g.Clear(SystemColors.Control);
                p.Clear();
                g.DrawImage(currentImage, 200, 50);

            }
        }

        private void buttonFillImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                imageToFill = new Bitmap(open.FileName);

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            drawSlow = checkBox.Checked;
        }

        private void buttonBorder_Click(object sender, EventArgs e)
        {
            DrawBorderFromList();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            g.Clear(SystemColors.Control);
            p.Clear();
            currentImage = new Bitmap(originalImage);
            g.DrawImage(currentImage, 200, 50);
        }
    }
}
