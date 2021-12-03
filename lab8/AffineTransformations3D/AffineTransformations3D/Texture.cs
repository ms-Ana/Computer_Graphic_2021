using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AffineTransformations3D
{
    class Texture
    {

        public static Bitmap GetTexture(Polyhedron3D polyhedron, Bitmap texture, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            double[,] buff = new double[height, width];
            for (int i = 0; i < bitmap.Width; i++)
                for (int j = 0; j < bitmap.Height; j++)
                    bitmap.SetPixel(i, j, Color.White);
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    buff[i, j] = double.MinValue;

            var rasterizedPolyhedron = GraphMath3D.RasterizeWithTexture(polyhedron, texture);

            Point3D center = new Point3D(width / 2, height / 2, 0);
            for (int i = 0; i < rasterizedPolyhedron.Count; i++)
                foreach (var point in rasterizedPolyhedron[i])
                {
                    int x = (int)(point.Item1.x + center.x);
                    int y = (int)(point.Item1.y + center.y);
                    if (x < width && x > 0 && y < height && y > 0)
                        if (point.Item1.z > buff[x, y])
                        {
                            buff[x, y] = point.Item1.z;
                            var values = point.Item2;
                            double w = values.Item1; double h = values.Item2;
                            if (w >= texture.Width)
                                w = texture.Width - 1;
                            if (h >= texture.Height)
                                h = texture.Height - 1;
                            if (w < 0)
                                w = 0;
                            if (h < 0)
                                h = 0;

                            int red = texture.GetPixel((int)w, (int)h).R;
                            int green = texture.GetPixel((int)w, (int)h).G;
                            int blue = texture.GetPixel((int)w, (int)h).B;
                            bitmap.SetPixel(x, y, Color.FromArgb(red, green, blue));
                        }
                }
            return bitmap;
        }
    }
}
