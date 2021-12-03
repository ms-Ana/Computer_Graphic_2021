using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

namespace AffineTransformations3D
{
    class FloatingHorizon
    {
        public static void FloatingHorizonAlgorithm(callable func, int countStep, double X0, double X1, double Z0, double Z1,
             Graphics g, int W, int H, int scale)
        {
            int wScreen = W * 2 - 1;
            int hScreen = H * 2 - 1;
            double stepX = (X1 - X0) / countStep, stepZ = (Z1 - Z0) / countStep,
                currentX = X0, currentZ = Z0;
            double location = -100;
            double Xmin = X0, Xmax = X1, Zmin = Z0, Zmax = Z1;
            int tFlag = 0, pFlag = 0;
            Pen pen = new Pen(Color.Black);
            double xi = 0, yi = 0;

            double xLeft = -1, yLeft = -1, xRight = -1, yRight = -1;
            Dictionary<double, double> Up = new Dictionary<double, double>(), Down = new Dictionary<double, double>();
            for (int i = 0; i < countStep; i++)
            {
                Up.Add(currentX, 0);
                Down.Add(currentX, hScreen);
                currentX += stepX;
            }

            List<double> pointsZ = new List<double>();
            for (int i = 0; i < countStep; i++)
            {
                pointsZ.Add(currentZ);
                currentZ += stepZ;
            }
            pointsZ = pointsZ.OrderBy(point => Math.Abs(location - point)).ToList();

            for (int i = pointsZ.Count - 1; i >= 0; i--)
            {
                currentZ = pointsZ[i];
                double xPrev = Xmin;
                double yPrev = func(Xmin, currentZ);
                Edge(xPrev, yPrev, ref xLeft, ref yLeft, ref Up, ref Down);
                Visibility(xPrev, yPrev, 0, hScreen, Up, Down, ref tFlag);
                for (currentX = Xmin; currentX <= Xmax; currentX += stepX)
                {
                    double y = func(currentX, currentZ);
                    Visibility(currentX, y, 0, hScreen, Up, Down, ref tFlag);
                    if (tFlag == pFlag)
                    {
                        if (tFlag == 1 || tFlag == -1)
                        {
                            g.DrawLine(pen, W + (int)xPrev * scale, H - (int)yPrev * scale, W + (int)currentX * scale, H - (int)y * scale);
                            CalculateHorizon(xPrev, yPrev, currentX, y, ref Up, ref Down);
                        }
                    }
                    else
                    {
                        if (tFlag == 0)
                        {
                            if (pFlag == 1)
                            {
                                Intersection(xPrev, yPrev, currentX, y, 0, Up, ref xi, ref yi);
                            }
                            else
                            {
                                Intersection(xPrev, yPrev, currentX, y, hScreen, Down, ref xi, ref yi);
                            }
                            g.DrawLine(pen, W + (int)xPrev * scale, H - (int)yPrev * scale, W + (int)xi * scale, H - (int)yi * scale);
                            CalculateHorizon(xPrev, yPrev, xi, yi, ref Up, ref Down);
                        }
                        else if (tFlag == 1)
                        {
                            if (pFlag == 0)
                            {
                                Intersection(xPrev, yPrev, currentX, y, 0, Up, ref xi, ref yi);
                                //if (Math.Abs(yi - y) < 1)
                                    //g.DrawLine(pen, W + (int)xi * scale, H - (int)yi * scale, W + (int)currentX * scale, H - (int)y * scale);
                                CalculateHorizon(xi, yi, currentX, y, ref Up, ref Down);
                            }
                            else
                            {
                                Intersection(xPrev, yPrev, currentX, y, hScreen, Down, ref xi, ref yi);
                                //if (Math.Abs(yPrev - yi) < 1)
                                    //g.DrawLine(pen, W + (int)xPrev * scale, H - (int)yPrev * scale, W + (int)xi * scale, H - (int)yi * scale);
                                CalculateHorizon(xPrev, yPrev, xi, yi, ref Up, ref Down);
                                Intersection(xPrev, yPrev, currentX, y, 0, Up, ref xi, ref yi);
                                //if (Math.Abs(yi - y) < 1)
                                    //g.DrawLine(pen, W + (int)xi * scale, H - (int)yi * scale, W + (int)currentX * scale, H - (int)y * scale);
                                CalculateHorizon(xi, yi, currentX, y, ref Up, ref Down);
                            }
                        }
                        else
                        {
                            if (pFlag == 0)
                            {
                                Intersection(xPrev, yPrev, currentX, y, 0, Up, ref xi, ref yi);
                                //if (Math.Abs(yi - y) < 1)
                                    //g.DrawLine(pen, W + (int)xi * scale, H - (int)yi * scale, W + (int)currentX * scale, H - (int)y * scale);
                                CalculateHorizon(xi, yi, currentX, y, ref Up, ref Down);
                            }
                            else
                            {
                                Intersection(xPrev, yPrev, currentX, y, 0, Up, ref xi, ref yi);
                                //if (Math.Abs(yi - yPrev) < 1)
                                    //g.DrawLine(pen, W + (int)xPrev * scale, H - (int)yPrev * scale, W + (int)xi * scale, H - (int)yi * scale);
                                CalculateHorizon(xPrev, yPrev, xi, yi, ref Up, ref Down);
                                Intersection(xPrev, yPrev, currentX, y, hScreen, Down, ref xi, ref yi);
                                //if (Math.Abs(yi - y) < 1)
                                    //g.DrawLine(pen, W + (int)xi * scale, H - (int)yi * scale, W + (int)currentX * scale, H - (int)y * scale);
                                CalculateHorizon(xi, yi, currentX, y, ref Up, ref Down);
                            }
                        }
                    }
                    pFlag = tFlag;
                    xPrev = currentX;
                    yPrev = y;
                }
                Edge(currentX, yPrev, ref xRight, ref yRight, ref Up, ref Down);
            }
        }

        public static void HorizonView(callable func, int countStep, double X0, double X1, double Z0, double Z1,
            Graphics g, int W, int H)
        {
            int wScreen = W * 2 - 1;
            int hScreen = H * 2 - 1;
            double stepX = (X1 - X0) / countStep, stepZ = (Z1 - Z0) / countStep,
                currentX = X0, currentZ = Z0;
            double Xmin = X0, Xmax = X1, Zmin = Z0, Zmax = Z1;
            int tFlag = 0, pFlag = 0;
            Pen pen = new Pen(Color.Black);
            double xi = 0, yi = 0;

            double xLeft = -1, yLeft = -1, xRight = -1, yRight = -1;
            Dictionary<double, double> Up = new Dictionary<double, double>(), Down = new Dictionary<double, double>();
            List<List<double>> pointsZ = new List<List<double>>();
            List<List<double>> pointsX = new List<List<double>>();

            int index = 0;
            for (int h = (int)X0; h <= X1; h += (int)stepX)
            {
                double middleX = h / 2;
                pointsX.Add(new List<double>());
                pointsZ.Add(new List<double>());
                for (double x = middleX - countStep / 2; x <= middleX + countStep / 2; x++)
                {
                    double z = -x + h;
                    pointsX[index].Add(x);
                    pointsZ[index].Add(z);
                    if (!Up.ContainsKey(x))
                        Up.Add(x, 0);
                    if (!Down.ContainsKey(x))
                        Down.Add(x, hScreen);
                }
                index++;
            }

            for (int i = 0; i <= countStep; i++)
            {
                Xmin = pointsX[i][0];
                currentZ = pointsZ[i][0];
                double xPrev = Xmin;
                double yPrev = func(Xmin, currentZ);
                Edge(xPrev, yPrev, ref xLeft, ref yLeft, ref Up, ref Down);
                Visibility(xPrev, yPrev, 0, hScreen, Up, Down, ref tFlag);
                for (int j = 0; j <= countStep; j++)
                {
                    currentX = pointsX[i][j];
                    currentZ = pointsZ[i][j];
                    double y = func(currentX, currentZ);
                    Visibility(currentX, y, 0, hScreen, Up, Down, ref tFlag);
                    if (tFlag == pFlag)
                    {
                        if (tFlag == 1 || tFlag == -1)
                        {
                            g.DrawLine(pen, W + (int)xPrev, H - (int)yPrev, W + (int)currentX, H - (int)y);
                            CalculateHorizon(xPrev, yPrev, currentX, y, ref Up, ref Down);
                        }
                    }
                    else
                    {
                        if (tFlag == 0)
                        {
                            if (pFlag == 1)
                            {
                                Intersection(xPrev, yPrev, currentX, y, 0, Up, ref xi, ref yi);
                            }
                            else
                            {
                                Intersection(xPrev, yPrev, currentX, y, hScreen, Down, ref xi, ref yi);
                            }
                            g.DrawLine(pen, W + (int)xPrev, H - (int)yPrev, W + (int)xi, H - (int)yi);
                            CalculateHorizon(xPrev, yPrev, xi, yi, ref Up, ref Down);
                        }
                        else if (tFlag == 1)
                        {
                            if (pFlag == 0)
                            {
                                if (Intersection(xPrev, yPrev, currentX, y, 0, Up, ref xi, ref yi) && xi != currentX)
                                    g.DrawLine(pen, W + (int)xi, H - (int)yi, W + (int)currentX, H - (int)y);
                                CalculateHorizon(xi, yi, currentX, y, ref Up, ref Down);
                            }
                            else
                            {
                                if (Intersection(xPrev, yPrev, currentX, y, hScreen, Down, ref xi, ref yi) && xPrev != currentX)
                                    g.DrawLine(pen, W + (int)xPrev, H - (int)yPrev, W + (int)xi, H - (int)yi);
                                CalculateHorizon(xPrev, yPrev, xi, yi, ref Up, ref Down);
                                if (Intersection(xPrev, yPrev, currentX, y, 0, Up, ref xi, ref yi) && xi != currentX)
                                    g.DrawLine(pen, W + (int)xi, H - (int)yi, W + (int)currentX, H - (int)y);
                                CalculateHorizon(xi, yi, currentX, y, ref Up, ref Down);
                            }
                        }
                        else
                        {
                            if (pFlag == 0)
                            {
                                if (Intersection(xPrev, yPrev, currentX, y, 0, Up, ref xi, ref yi) && xi != currentX)
                                    g.DrawLine(pen, W + (int)xi, H - (int)yi, W + (int)currentX, H - (int)y);
                                CalculateHorizon(xi, yi, currentX, y, ref Up, ref Down);
                            }
                            else
                            {
                                if (Intersection(xPrev, yPrev, currentX, y, 0, Up, ref xi, ref yi) && xPrev != currentX)
                                    g.DrawLine(pen, W + (int)xPrev, H - (int)yPrev, W + (int)xi, H - (int)yi);
                                CalculateHorizon(xPrev, yPrev, xi, yi, ref Up, ref Down);
                                if (Intersection(xPrev, yPrev, currentX, y, hScreen, Down, ref xi, ref yi) && xi != currentX)
                                    g.DrawLine(pen, W + (int)xi, H - (int)yi, W + (int)currentX, H - (int)y);
                                CalculateHorizon(xi, yi, currentX, y, ref Up, ref Down);
                            }
                        }
                    }
                    pFlag = tFlag;
                    xPrev = currentX;
                    yPrev = y;
                }
                Edge(currentX, yPrev, ref xRight, ref yRight, ref Up, ref Down);
            }
        }

        private static void Edge(double x, double y, ref double xEdge, ref double yEdge,
            ref Dictionary<double, double> Up, ref Dictionary<double, double> Down)
        {
            if (xEdge != -1)
                CalculateHorizon(xEdge, yEdge, x, y, ref Up, ref Down);
            xEdge = x;
            yEdge = y;
        }

        private static void Visibility(double x, double y, int defUp, int defDown,
            Dictionary<double, double> Up, Dictionary<double, double> Down, ref int tFlag)
        {
            double u, d;
            if (!Up.ContainsKey(x))
                u = defUp;
            else
                u = Up[x];
            if (!Down.ContainsKey(x))
                d = defDown;
            else
                d = Down[x];
            if (y < u && y < d)
                tFlag = 0;
            if (y >= u)
                tFlag = 1;
            if (y <= d)
                tFlag = -1;
        }

        private static void CalculateHorizon(double x1, double y1, double x2, double y2,
            ref Dictionary<double, double> Up, ref Dictionary<double, double> Down)
        {
            if (x2 - x1 == 0)
            {
                if (Up.ContainsKey(x2))
                    Up[x2] = Math.Max(Up[x2], y2);
                else
                    Up.Add(x2, y2);
                if (Down.ContainsKey(x2))
                    Down[x2] = Math.Min(Down[x2], y2);
                else
                    Down.Add(x2, y2);
            }
            else
            {
                double d = (y2 - y1) / (x2 - x1);
                for (double x = x1; x <= x2; x++)
                {
                    double y = d * (x - x1) + y1;
                    if (Up.ContainsKey(x))
                        Up[x] = Math.Max(Up[x], y);
                    else
                        Up.Add(x, y);
                    if (Down.ContainsKey(x))
                        Down[x] = Math.Min(Down[x], y);
                    else
                        Down.Add(x, y);
                }
            }
        }

        private static bool Intersection(double x1, double y1, double x2, double y2, int def,
            Dictionary<double, double> Arr, ref double xi, ref double yi)
        {
            if (x2 - x1 == 0)
            {
                xi = x2;
                yi = Arr[x2];
            }
            else
            {
                double d = (y2 - y1) / (x2 - x1);
                double a;
                if (!Arr.ContainsKey(x1 + 1))
                    a = def;
                else
                    a = Arr[x1 + 1];
                int ySign = Math.Sign(y1 + d - a);
                int cSign = ySign;
                yi = y1 + d;
                xi = x1 + 1;
                while (cSign == ySign)
                {
                    yi += d;
                    xi++;
                    if (!Arr.ContainsKey(xi))
                    {
                        bool canContinue = false;
                        foreach (double key in Arr.Keys)
                        {
                            if (key > xi)
                            {
                                canContinue = true;
                                break;
                            }
                        }
                        if (!canContinue)
                            return false;
                        cSign = Math.Sign(yi - def);
                    }
                    else
                        cSign = Math.Sign(yi - Arr[xi]);
                }
                double val;
                if (!Arr.ContainsKey(xi))
                    val = def;
                else
                    val = Arr[xi];
                double val2;
                if (!Arr.ContainsKey(xi - 1))
                    val2 = def;
                else
                    val2 = Arr[xi - 1];
                if (Math.Abs(yi - d - val2) <= Math.Abs(yi - val))
                {
                    yi -= d;
                    xi--;
                }
            }
            return true;
        }
    }
}
