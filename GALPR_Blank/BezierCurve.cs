using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALPR_Blank
{
    public class BezierCurve
    {
        public Point p0;
        public Point p1;
        public Point p2;
        public Point p3;

        private double qx0;
        private double qx1;
        private double qx2;
        private double qx3;

        private double qy0;
        private double qy1;
        private double qy2;
        private double qy3;

        public BezierCurve(Point p0, Point p1, Point p2, Point p3) {

            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }

        public BezierCurve(List<Point> points, int startIndex) {

            p0 = points[startIndex % points.Count];
            p1 = points[(startIndex + 1) % points.Count];
            p2 = points[(startIndex + 2) % points.Count];
            p3 = points[(startIndex + 3) % points.Count];
        }

        private void recomputeQValues()
        {
            qx0 = p0.X;
            qx1 = 3 * (p1.X - p0.X);
            qx2 = 3 * (p2.X - 2 * p1.X + p0.X);
            qx3 = p3.X - 3 * p2.X + 3 * p1.X - p0.X;

            qy0 = p0.Y;
            qy1 = 3 * (p1.Y - p0.Y);
            qy2 = 3 * (p2.Y - 2 * p1.Y + p0.Y);
            qy3 = p3.Y - 3 * p2.Y + 3 * p1.Y - p0.Y;
        }

        private Point GetPointForT(double t)
        {
            double tt = t * t;
            double ttt = tt * t;

            double x = qx0 + qx1 * t + qx2 * tt + qx3 * ttt;
            double y = qy0 + qy1 * t + qy2 * tt + qy3 * ttt;

            return new Point((int)Math.Round(x), (int)Math.Round(y));
        }

        public void DrawToVRAM(VRAM vram, Color color, int steps) {

            recomputeQValues();

            double dt = 1.0 / steps;
            double t = dt;

            Point oldPoint = p0; // GetPointForT(0);
            vram.SetPixel(p0.X, p0.Y, Color.Red);
            for (int i = 1; i < steps; i++)
            {
               Point cp = GetPointForT(t);
               vram.SetPixel(cp.X, cp.Y, color);


        //              DDALine.DrawLine(vram, oldPoint, nextPoint, color);

        //oldPoint = nextPoint;
                t += dt;
            }
            vram.SetPixel((int)(p3.X), (int)p3.Y, Color.Red);

    }
  }
}
