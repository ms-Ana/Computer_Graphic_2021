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

        private bool currentAxonometric;
        private Polyhedron3D current;
        private List<Polyhedron3D> currentFigures;
        private bool add;
        private List<int> removed;
        private bool remove = true;

        private double rotationAngle;
        private int rotation;
        private bool rotating;

        private int distance;
        private Polyhedron3D currentCamera;
        private int cameraAngle;
        private PictureBox picture;

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
            SolidsOfRevolution.points = new List<Point>();
            removed = new List<int>();
            distance = 100;
            currentFigures = new List<Polyhedron3D>();
            add = false;

            picture = new PictureBox
            {
                Name = "pictureBox",
                Size = new Size(400, 400),
                Location = new Point(this.Width - 420, this.Height - 440)

            };
            this.Controls.Add(picture);
            picture.Visible = false;
        }


        // Projections
        private void buttonAxonometric_Click(object sender, EventArgs e)
        {
            currentAxonometric = true;
            ShowFigureWithAxis();
        }

        private void buttonPerspective_Click(object sender, EventArgs e)
        {
            currentAxonometric = false;
            ShowFigureWithAxis();
        }


        // Choose figure
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            int currentPolyhedron = cmb.SelectedIndex;
            Polyhedron3D p = Figures.Hexahedron(scale);
            HideButtons(false);
            switch (currentPolyhedron)
            {
                case 1:
                    p = Figures.Tetrahedron(scale);
                    break;
                case 2:
                    p = Figures.Octahedron(scale);
                    break;
                case 3:
                    p = Figures.Icosahedron(scale);
                    break;
                case 4:
                    p = Figures.Dodecahedron(scale);
                    break;
                case 5:
                    p = Polyhedron3D.Graphic(GraphMath3D.SinCos, 30, -100, 100, -100, 100);
                    g.Clear(SystemColors.Control);
                    FloatingHorizon.FloatingHorizonAlgorithm(GraphMath3D.SinCos, 30, -100, 100, -100, 100, g, W, H, 3);
                    break;
                case 6:
                    p = Polyhedron3D.Graphic(GraphMath3D.Square, 30, -100, 100, -100, 100);
                    g.Clear(SystemColors.Control);
                    FloatingHorizon.FloatingHorizonAlgorithm(GraphMath3D.Square, 30, -100, 100, -100, 100, g, W, H, 1);
                    break;
                case 7:
                    HideButtons(true);
                    break;
                default:
                    break;
            }
            if (add)
            {
                currentFigures.RemoveAt(currentFigures.Count() - 1);
                currentFigures.Add(current.Copy());
                currentFigures.Add(p);
                add = false;
            }
            else
            {
                currentFigures.Clear();
            }
            if (currentFigures.Count() == 0)
            {
                currentFigures.Add(p);
            }
            current = p;

            buttonAxonometric.Visible = true;
            buttonPerspective.Visible = true;
        }


        // Draw figure on screen
        private void ShowFigure(Polyhedron3D polyhedron, Polyhedron3D projection, Color color, bool remove)
        {
            removed.Clear();
            if (remove)
            {
                DeleteNotFrontFacingSides(polyhedron, false);
            }
            Pen pen = new Pen(color);
            for (int i = 0; i < projection.polygons.Count(); i++)
            {
                if (remove && removed.Contains(i))
                {
                    continue;
                }
                foreach (Line3D l in projection.polygons[i].lines)
                {
                    g.DrawLine(pen, (int)l.first.x + W, H - (int)l.first.y, (int)l.second.x + W, H - (int)l.second.y);
                }
            }
        }

        private void ShowFigureCamera(Polyhedron3D polyhedron, Polyhedron3D projection, Color color, bool remove)
        {
            removed.Clear();
            if (remove)
            {
                DeleteNotFrontFacingSides(polyhedron, true);
            }
            Pen pen = new Pen(color);
            for (int i = 0; i < projection.polygons.Count(); i++)
            {
                if (remove && removed.Contains(i))
                {
                    continue;
                }
                foreach (Line3D l in projection.polygons[i].lines)
                {
                    g.DrawLine(pen, (int)l.first.x + W, H - (int)l.first.y, (int)l.second.x + W, H - (int)l.second.y);
                }
            }
        }

        private void ShowFigureWithAxis()
        {
            g.Clear(SystemColors.Control);
            if (currentAxonometric)
            {
                DrawAxisAxonometric();
                foreach (Polyhedron3D polyhedron in currentFigures)
                {
                    ShowFigure(polyhedron, polyhedron.Axonometric(), Color.Black, remove);
                }
            }
            else
            {
                DrawAxisPerspective();
                foreach (Polyhedron3D polyhedron in currentFigures)
                {
                    ShowFigure(polyhedron, polyhedron.Perspective(), Color.Black, remove);
                }
            }
        }


        // Draw Axis
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
            ShowFigure(p1, p1.Axonometric(), Color.Red, false);

            Line3D yAxis = new Line3D(startY, y);
            Polygon3D s2 = new Polygon3D(new List<Line3D> { yAxis });
            Polyhedron3D p2 = new Polyhedron3D(new List<Polygon3D> { s2 });
            ShowFigure(p2, p2.Axonometric(), Color.Blue, false);

            Line3D zAxis = new Line3D(startZ, z);
            Polygon3D s3 = new Polygon3D(new List<Line3D> { zAxis });
            Polyhedron3D p3 = new Polyhedron3D(new List<Polygon3D> { s3 });
            ShowFigure(p2, p3.Axonometric(), Color.Green, false);
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
            ShowFigure(p1, p1.Perspective(), Color.Red, false);

            Line3D yAxis = new Line3D(startY, y);
            Polygon3D s2 = new Polygon3D(new List<Line3D> { yAxis });
            Polyhedron3D p2 = new Polyhedron3D(new List<Polygon3D> { s2 });
            ShowFigure(p2, p2.Perspective(), Color.Blue, false);

            Line3D zAxis = new Line3D(startZ, z);
            Polygon3D s3 = new Polygon3D(new List<Line3D> { zAxis });
            Polyhedron3D p3 = new Polyhedron3D(new List<Polygon3D> { s3 });
            ShowFigure(p3, p3.Perspective(), Color.Green, false);
        }


        // Helper functions for affine transformations
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
                    ShowFigureWithAxis();
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
                    ShowFigureWithAxis();
                }
            }
        }

        private void ReflectFigure(String axis)
        {
            current.Reflect(axis);
            ShowFigureWithAxis();
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

            ShowFigureWithAxis();
        }


        // Affine transformations buttons
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

        
        // Solids of revolution helper functions and buttons
        private void HideButtons(bool hide)
        {
            button1.Visible = hide;
            buttonX.Visible = hide;
            buttonY.Visible = hide;
            buttonZ.Visible = hide;
            textBoxRotations.Visible = hide;
            label13.Visible = hide;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SolidsOfRevolution.points.Count != 0 && SolidsOfRevolution.points.Count < 3)
            {
                return;
            }

            Button button = (Button)sender;
            if (button.Text == "Задать образующую")
            {
                SolidsOfRevolution.points.Clear();
                textBoxRotations.Visible = false;
                buttonX.Visible = false;
                buttonY.Visible = false;
                buttonZ.Visible = false;
                label13.Visible = false;
                g.Clear(SystemColors.Control);
                MouseClick += Form1_MouseClick;
            }
            else
            {
                button.Text = "Задать образующую";
                MouseClick -= Form1_MouseClick;
                if (SolidsOfRevolution.points.Count > 2)
                {
                    Pen pen = new Pen(Color.Black);
                    Point first = SolidsOfRevolution.points[0];
                    Point last = SolidsOfRevolution.points[SolidsOfRevolution.points.Count - 1];
                    g.DrawLine(pen, first.X + W, H - first.Y, last.X + W, H - last.Y);
                }
                textBoxRotations.Visible = true;
                textBoxRotations.Text = "" + SolidsOfRevolution.rotations;
                buttonX.Visible = true;
                buttonY.Visible = true;
                buttonZ.Visible = true;
                label13.Visible = true;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(Color.Black);
            if (SolidsOfRevolution.points.Count > 0)
            {
                Point prev = SolidsOfRevolution.points[SolidsOfRevolution.points.Count - 1];
                g.DrawLine(pen, W + prev.X, H - prev.Y, e.X, e.Y);
            }

            SolidsOfRevolution.points.Add(new Point(e.X - W, H - e.Y));
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
                SolidsOfRevolution.rotations = r;
            }
        }

        private void buttonX_Click(object sender, EventArgs e)
        {
            Polyhedron3D p = SolidsOfRevolution.CreateParallelToXAxis();
            if (add)
            {
                currentFigures.RemoveAt(currentFigures.Count() - 1);
                currentFigures.Add(current.Copy());
                currentFigures.Add(p);
                add = false;
            }
            else
            {
                currentFigures.Clear();
            }
            if (currentFigures.Count() == 0)
            {
                currentFigures.Add(p);
            }
            current = p;
            ShowFigureWithAxis();
        }

        private void buttonY_Click(object sender, EventArgs e)
        {
            Polyhedron3D p = SolidsOfRevolution.CreateParallelToYAxis();
            if (add)
            {
                currentFigures.RemoveAt(currentFigures.Count() - 1);
                currentFigures.Add(current.Copy());
                currentFigures.Add(p);
                add = false;
            }
            else
            {
                currentFigures.Clear();
            }
            if (currentFigures.Count() == 0)
            {
                currentFigures.Add(p);
            }
            current = p;
            ShowFigureWithAxis();
        }

        private void buttonZ_Click(object sender, EventArgs e)
        {
            Polyhedron3D p = SolidsOfRevolution.CreateParallelToZAxis();
            if (add)
            {
                currentFigures.RemoveAt(currentFigures.Count() - 1);
                currentFigures.Add(current.Copy());
                currentFigures.Add(p);
                add = false;
            }
            else
            {
                currentFigures.Clear();
            }
            if (currentFigures.Count() == 0)
            {
                currentFigures.Add(p);
            }
            current = p;
            ShowFigureWithAxis();
        }


        // Deleting not front-facing sides
        private void DeleteNotFrontFacingSides(Polyhedron3D p, bool camera)
        {
            removed.Clear();
            double projX, projY, projZ;
            if (camera)
            {
                double a = (Math.PI / 180) * cameraAngle;
                double x = -Math.Sqrt(8);
                projX = x * Math.Cos(a) - x * Math.Sin(a);
                projY = 3;
                projZ = x * Math.Sin(a) + x * Math.Cos(a);
            }
            else if (currentAxonometric)
            {
                projX = -Math.Sqrt(8);
                projY = 3;
                projZ = -Math.Sqrt(8);
            }
            else
            {
                projX = 0;
                projY = 0;
                projZ = 1;
            }

            for (int i = 0; i < p.polygons.Count(); i++)
            {
                Polygon3D polygon = p.polygons[i];
                Point3D normal = GraphMath3D.CalculateNormal(polygon);

                double cos = (projX * normal.x + projY * normal.y + projZ * normal.z) /
                    (Math.Sqrt(projX * projX + projY * projY + projZ * projZ) *
                    Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z));
                double arccos = Math.Acos(cos);
                double angle = (180 / Math.PI) * arccos;
                if (angle > 90 && angle < 180)
                {
                    removed.Add(i);
                }
            }
        }

        private void checkBoxRemove_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            remove = checkBox.Checked;
        }


        // Camera
        private void buttonCamera_Click(object sender, EventArgs e)
        {
            if (buttonCamera.BackColor == Color.LightSkyBlue)
            {
                buttonCamera.BackColor = SystemColors.ControlLight;
                KeyDown -= Form1_KeyDown;
                ShowFigureWithAxis();
                return;
            }
            else
            {
                distance = 100;
                currentCamera = current.Copy();
                cameraAngle = 0;

                KeyDown += Form1_KeyDown;
                buttonCamera.BackColor = Color.LightSkyBlue;
                ShowFigureWithAxis();
                return;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (distance - 10 == 50)
                {
                    return;
                }
                distance -= 10;
                currentCamera.Scale(2, 2, 2);
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (distance + 10 == 170)
                {
                    return;
                }
                distance += 10;
                currentCamera.Scale(0.5, 0.5, 0.5);
            }
            else if (e.KeyCode == Keys.Right)
            {
                currentCamera.RotateCenter(0, 10, 0);
                cameraAngle += 10;
            }
            else if (e.KeyCode == Keys.Left)
            {
                currentCamera.RotateCenter(0, -10, 0);
                cameraAngle -= 10;
            }
            else
            {
                return;
            }

            e.Handled = true;
            g.Clear(SystemColors.Control);
            ShowFigureCamera(currentCamera, currentCamera.Axonometric(), Color.Black, true);
        }

        
        // ZBuffer
        private void buttonZBuffer_Click(object sender, EventArgs e)
        {
            List<Polyhedron3D> polyhedrons = new List<Polyhedron3D>();
            foreach (Polyhedron3D p in FixPointsForZBuffer())
            {
                polyhedrons.Add(p.Copy().Axonometric());
            }

            Bitmap bitmap = ZBuffer.ZBufferAlgorithm(400, 400, polyhedrons);
            picture.Image = bitmap;
            picture.Visible = true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            add = true;
        }

        private List<Polyhedron3D> FixPointsForZBuffer()
        {
            List<Polyhedron3D> res = new List<Polyhedron3D>();
            foreach (Polyhedron3D polyhedron in currentFigures)
            {
                Polyhedron3D newPolyhedron = polyhedron.Copy();
                newPolyhedron.FixPointsForBitmap();
                res.Add(newPolyhedron);
            }
            return res;
        }


        // Gourand

        private void buttonGourand_Click(object sender, EventArgs e)
        {
            //Point3D light = new Point3D(-Math.Sqrt(8), 3, -Math.Sqrt(8));
            Point3D light = new Point3D(0, 400, 0);
            Polyhedron3D polyhedron = current.Copy();
            polyhedron.FixPointsForBitmap();
            Bitmap bitmap = GouraudShading.Gourand(polyhedron.Axonometric(), 400, 400, light, Color.Gray);
            picture.Image = bitmap;
            picture.Visible = true;
        }


        // Texture

        private void buttonTexture_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Bitmap texture = new Bitmap(open.FileName);
                Polyhedron3D polyhedron = current.Copy();
                polyhedron.FixPointsForBitmap();
                Bitmap bitmap = Texture.GetTexture(polyhedron.Axonometric(), texture, 400, 400);
                picture.Image = bitmap;
                picture.Visible = true;
            }
        }

    }

    public delegate double callable(double x, double y);

}
