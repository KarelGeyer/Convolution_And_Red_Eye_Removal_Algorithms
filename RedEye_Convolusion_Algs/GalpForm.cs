using GALPR_Blank;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GALP_Algoritmy
{
    public enum MethodsToUse
    {
        RED_EYE,
        CONVOLUTION,
        BEZIER,
    }
    public partial class GalpForm : Form
    {
        const string IMAGE_PATH = @"E:\Projects\Learn\Grafické algoritmy\images\{0}";
        string[] imageNames =
        {
            //Redeye
            "BoldRedEye.jpeg",
            "redeye2.jpg",
            "redeye3.jpg",
            //Convolution
            "convol1.jpg",
            "convol2.png",
            "convol3.jpg",
            "convol4.jpg"
        };

        public GalpForm()
        {
            InitializeComponent();
            MethodsToUse methodToUse = MethodsToUse.BEZIER;

            if (!methodToUse.Equals(MethodsToUse.BEZIER))
            {
                int imageIndex = 3;
                string path = string.Format(IMAGE_PATH, imageNames[imageIndex]);
                VRAM vram = VRAM.CreateFromBitmap(new Bitmap(path));
                pictureBoxCustom1.Image = vram.GetBitmap();

                if (methodToUse.Equals(MethodsToUse.RED_EYE))
                    new RedEye().RemoveRedEye(vram, pictureBoxCustom1);

                if (methodToUse.Equals(MethodsToUse.CONVOLUTION))
                    new Convol().Convolution(vram, pictureBoxCustom1);
            }

            if (methodToUse.Equals(MethodsToUse.BEZIER))
            {
                VRAM vram = new VRAM(2000, 2000);

                Point p0 = new Point(100, 800);
                Point p1 = new Point(400, 200);
                Point p2 = new Point(700, 200);
                Point p3 = new Point(1000, 800);
                BezierCurve curve = new BezierCurve(p0, p1, p2, p3);

                Point p02 = new Point(1000, 800);
                Point p12 = new Point(1400, 200);
                Point p22 = new Point(1700, 200);
                Point p32 = new Point(2000, 800);
                BezierCurve curve2 = new BezierCurve(p02, p12, p22, p32);
                Bitmap bitmap = new Bitmap(vram.Width, vram.Height);

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    curve.DrawBezierCurve(g);
                    curve2.DrawBezierCurve(g);
                }

                pictureBoxCustom1.Image = bitmap;
            }
        }
    }
}
