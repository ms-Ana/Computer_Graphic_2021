using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorConverting
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Bitmap currentImage;
        private bool visualizeHistogram;
        private bool currentRgb;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            g = this.CreateGraphics();
            visualizeHistogram = true;
            checkBoxHistorgam.Visible = false;
            comboBoxOptions.Visible = false;
        }

        private void DrawGrayscale()
        {
            Bitmap source = currentImage;
            //double wPercent = 300.0 / source.Width;
            //source = new Bitmap(source, new Size((int)(source.Width * wPercent), (int)(source.Height * wPercent)));

            Bitmap grayscale = new Bitmap(source.Width, source.Height);
            Bitmap grayscaleHdtv = new Bitmap(source.Width, source.Height);
            Bitmap grayscaleDifference = new Bitmap(source.Width, source.Height);

            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    Color pixel = source.GetPixel(i, j);
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;

                    byte grayscalePixel = (byte)(R * 0.299 + G * 0.587 + B * 0.114);
                    Color grayscaleColor = Color.FromArgb(grayscalePixel, grayscalePixel,
                        grayscalePixel);
                    grayscale.SetPixel(i, j, grayscaleColor);

                    byte grayscaleHdtvPixel = (byte)(R * 0.2126 + G * 0.7152 + B * 0.0722);
                    Color grayscaleHdtvColor = Color.FromArgb(grayscaleHdtvPixel,
                        grayscaleHdtvPixel, grayscaleHdtvPixel);
                    grayscaleHdtv.SetPixel(i, j, grayscaleHdtvColor);

                    int grayscaleDifferencePixel = Math.Abs(grayscaleHdtvPixel - grayscalePixel) * 10;
                    Color grayscaleDifferenceColor = Color.FromArgb(grayscaleDifferencePixel,
                        grayscaleDifferencePixel, grayscaleDifferencePixel);
                    grayscaleDifference.SetPixel(i, j, grayscaleDifferenceColor);
                }
            }

            g.DrawImage(source, 10, 53);
            g.DrawImage(grayscale, source.Width + 20, 53);
            g.DrawImage(grayscaleHdtv, source.Width * 2 + 30, 53);
            g.DrawImage(grayscaleDifference, source.Width * 3 + 40, 53);

            if (visualizeHistogram)
            {
                DrawIntensityHistogram(source, 10, source.Height + 53);
                DrawIntensityHistogram(grayscale, source.Width + 20, source.Height + 53);
                DrawIntensityHistogram(grayscaleHdtv, source.Width * 2 + 30, source.Height + 53);
                DrawIntensityHistogram(grayscaleDifference, source.Width * 3 + 40, source.Height + 53);
            }
        }

        private void DrawRgbChannels()
        {
            Bitmap source = currentImage;
            //double wPercent = 300.0 / source.Width;
            //source = new Bitmap(source, new Size((int)(source.Width * wPercent), (int)(source.Height * wPercent)));

            Bitmap red = new Bitmap(source.Width, source.Height);
            Bitmap green = new Bitmap(source.Width, source.Height);
            Bitmap blue = new Bitmap(source.Width, source.Height);

            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    Color pixel = source.GetPixel(i, j);
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;

                    Color redColor = Color.FromArgb(R, 0, 0);
                    red.SetPixel(i, j, redColor);

                    Color greenColor = Color.FromArgb(0, G, 0);
                    green.SetPixel(i, j, greenColor);

                    Color blueColor = Color.FromArgb(0, 0, B);
                    blue.SetPixel(i, j, blueColor);
                }
            }

            g.DrawImage(source, 10, 53);
            g.DrawImage(red, source.Width + 20, 53);
            g.DrawImage(green, source.Width * 2 + 30, 53);
            g.DrawImage(blue, source.Width * 3 + 40, 53);

            if (visualizeHistogram)
            {
                DrawIntensityHistogram(source, 10, source.Height + 53);
                DrawIntensityHistogram(red, source.Width + 20, source.Height + 53);
                DrawIntensityHistogram(green, source.Width * 2 + 30, source.Height + 53);
                DrawIntensityHistogram(blue, source.Width * 3 + 40, source.Height + 53);
            }
        }

        private void DrawIntensityHistogram(Bitmap source, int x, int y)
        {
            Dictionary<byte, int> RedPixels = new Dictionary<byte, int>();
            Dictionary<byte, int> GreenPixels = new Dictionary<byte, int>();
            Dictionary<byte, int> BluePixels = new Dictionary<byte, int>();

            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    Color pixel = source.GetPixel(i, j);
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;

                    if (RedPixels.ContainsKey(R))
                        RedPixels[R]++;
                    else
                        RedPixels.Add(R, 1);
                    if (GreenPixels.ContainsKey(G))
                        GreenPixels[G]++;
                    else
                        GreenPixels.Add(G, 1);
                    if (BluePixels.ContainsKey(B))
                        BluePixels[B]++;
                    else
                        BluePixels.Add(B, 1);
                }
            }

            int maxRed = RedPixels.Values.Max();
            int maxGreen = GreenPixels.Values.Max();
            int maxBlue = BluePixels.Values.Max();

            Pen pen = new Pen(Color.Black);

            pen.Width = 2; int value;
            for (byte i = 1; i < 255; i++)
            {
                if (RedPixels.ContainsKey(i))
                {
                    pen.Color = Color.FromArgb(64, 255, 0, 0);
                    value = RedPixels[i] * 100 / maxRed;
                    g.DrawLine(pen, x + i, y + 110, x + i, y + 110 - value);
                }
                if (GreenPixels.ContainsKey(i))
                {
                    pen.Color = Color.FromArgb(64, 0, 255, 0);
                    value = GreenPixels[i] * 100 / maxGreen;
                    g.DrawLine(pen, x + i, y + 220, x + i, y + 220 - value);
                }
                if (BluePixels.ContainsKey(i))
                {
                    pen.Color = Color.FromArgb(64, 0, 0, 255);
                    value = BluePixels[i] * 100 / maxBlue;
                    g.DrawLine(pen, x + i, y + 330, x + i, y + 330 - value);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                currentImage = new Bitmap(open.FileName);
                g.Clear(SystemColors.Control);
                double wPercent = 300.0 / currentImage.Width;
                currentImage = new Bitmap(currentImage, new Size((int)(currentImage.Width * wPercent), 
                    (int)(currentImage.Height * wPercent)));
                g.DrawImage(currentImage, 10, 53);
                //if (visualizeHistogram)
                    //DrawIntensityHistogram(currentImage, 10, currentImage.Height + 53);
                comboBoxOptions.Visible = true;
                checkBoxHistorgam.Visible = true;
                if (comboBoxOptions.SelectedIndex > -1)
                {
                    if (currentRgb)
                        DrawRgbChannels();
                    else
                        DrawGrayscale();
                }
                
            }
        }

        private void checkBoxHistorgam_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            visualizeHistogram = checkBox.Checked;
            if (comboBoxOptions.SelectedIndex > -1)
            {
                g.Clear(SystemColors.Control);
                if (currentRgb)
                    DrawRgbChannels();
                else
                    DrawGrayscale();
            }
        }

        private void comboBoxOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            currentRgb = cmb.SelectedIndex == 1;
            g.Clear(SystemColors.Control);
            if (currentRgb)
                DrawRgbChannels();
            else
                DrawGrayscale();
        }
    }
}
