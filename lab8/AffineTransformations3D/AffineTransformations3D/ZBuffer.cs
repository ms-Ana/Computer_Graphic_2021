using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

namespace AffineTransformations3D
{
    class ZBuffer
    {
        public static Bitmap ZBufferAlgorithm(int width, int height, List<Polyhedron3D> polyhedron3Ds)
        {
            Bitmap result = new Bitmap(width, height);
            double[,] buff = new double[height, width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    result.SetPixel(i, j, Color.White);
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    buff[i, j] = double.MinValue;
            List<List<List<Point3D>>> rasterizedPolyhedrons = new List<List<List<Point3D>>>();
            foreach (var polyhedron in polyhedron3Ds)
                rasterizedPolyhedrons.Add(GraphMath3D.Rasterize(polyhedron, width, height));

            List<List<Color>> colors = new List<List<Color>>();
            foreach (var polyhedron in polyhedron3Ds)
                colors.Add(Colorize(polyhedron));

            Point3D center = new Point3D(width / 2, height / 2, 0);

            for (int i = 0; i < rasterizedPolyhedrons.Count; i++)
            {
                for (int j = 0; j < rasterizedPolyhedrons[i].Count; j++)
                    foreach (var point in rasterizedPolyhedrons[i][j])
                    {
                        int x = (int)(point.x + center.x);
                        int y = (int)(point.y + center.y);
                        if (x < width && x > 0 && y < height && y > 0)
                            if (point.z > buff[x, y])
                            {
                                buff[x, y] = point.z;
                                result.SetPixel(x, y, colors[i][j]);
                            }
                    }

            }

            return result;
        }
        public static List<Color> Colorize(Polyhedron3D polyhedron)
        {
            List<Color> colors = new List<Color>();
            Random randomGen = new Random();
            for (int i = 0; i < polyhedron.polygons.Count(); i++)
                colors.Add(Color.FromArgb(randomGen.Next(255), randomGen.Next(255), randomGen.Next(255)));
            return colors;
        }
       
      
    }
}
