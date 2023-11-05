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
        private List<PointF> controlPoints;

        public BezierCurve(params PointF[] controlPoints)
        {
            this.controlPoints = new List<PointF>(controlPoints);
        }

        public void DrawBezierCurve(Graphics g)
        {
            for (int i = 0; i < controlPoints.Count - 3; i += 3)
            {
                PointF p0 = controlPoints[i];
                PointF p1 = controlPoints[i + 1];
                PointF p2 = controlPoints[i + 2];
                PointF p3 = controlPoints[i + 3];

                // Pokud se rozhodeneme využít naší metodu CalculateBezierPoint
                // int numberOfPoints = 1000 - určuje hladkost a viditelnost křivky (čím vyšší integer, tím hladší a viditelnější křivka)
                //for (int j = 0; j <= numberOfPoints; j++)
                //{
                //    float t = (float)j / numberOfPoints;
                //    PointF p = CalculateBezierPoint(t, p0, p1, p2, p3);
                //    g.FillEllipse(Brushes.Black, p.X, p.Y, 4, 4);
                //}
                Pen pen = new Pen(Color.Black) { Width = 5 };

                g.DrawBezier(pen, p0, p1, p2, p3);
            }
        }

        // Nepoužitá metoda k vypočtení bodů beziérovy křivky
        private PointF CalculateBezierPoint(float t, PointF p0, PointF p1, PointF p2, PointF p3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            PointF p = PointF.Empty;

            p.X = (uuu * p0.X) + (3 * uu * t * p1.X) + (3 * u * tt * p2.X) + (ttt * p3.X);
            p.Y = (uuu * p0.Y) + (3 * uu * t * p1.Y) + (3 * u * tt * p2.Y) + (ttt * p3.Y);

            return p;
        }
    }
}
