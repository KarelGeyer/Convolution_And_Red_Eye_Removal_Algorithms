using System;
using System.Drawing;
using System.Windows.Forms;

namespace GALP_Algoritmy
{
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
            int methodToUse = 1;
            int imageIndex = 3;
            string path = string.Format(IMAGE_PATH, imageNames[imageIndex]);
            VRAM vram = VRAM.CreateFromBitmap(new Bitmap(path));
            pictureBoxCustom1.Image = vram.GetBitmap();

            if (methodToUse.Equals(0))
                RemoveRedEye(vram);
            else
                Convolution(vram);
        }

        private void RemoveRedEye(VRAM vram)
        {
            VRAM imageToAdjust = vram.Copy();
            for (int y = 0; y < imageToAdjust.Height; y++)
            {
                for (int x = 0; x < imageToAdjust.Width; x++)
                {
                    Color c = imageToAdjust.GetPixel(x, y);
                    HSL hsl = new HSL(c);

                    //(hsl.Hue > 250 || hsl.Hue < 5) && hsl.Saturation > 0.2 && hsl.Luminance > 0.2 && hsl.Luminance < 0.5 - BoldRedEye.jpeg
                    //(hsl.Hue > 350 || hsl.Hue < 15) && hsl.Saturation > 0.45 && hsl.Luminance > 0.3 && hsl.Luminance < 0.85 - redeye2.jpg
                    //(hsl.Hue > 210 || hsl.Hue < 20) && hsl.Saturation > 0.4 && hsl.Luminance > 0.25 && hsl.Luminance < 0.45 - redeye3.jpg
                    if (
                        (hsl.Hue > 210 || hsl.Hue < 20)
                        && hsl.Saturation > 0.4
                        && hsl.Luminance > 0.25
                        && hsl.Luminance < 0.45
                    )
                        imageToAdjust.SetPixel(x, y, Color.FromArgb(0, 20, 20, 20));
                }
            }

            pictureBoxCustom1.Image = vram.GetBitmap();
            pictureBoxCustom2.Image = imageToAdjust.GetBitmap();
        }

        private void Convolution(VRAM vram)
        {
            VRAM image = vram.Copy();
            ConvertToGrayscale(image);
            pictureBoxCustom1.Image = image.GetBitmap();
            VRAM tmp = image.Copy();
            int n = 3;
            int n1 = n / 2;
            int H = 80;
            int g;
            double s = 0;

            float[,] k = new float[,]
            {
                { 1, 2, 1 },
                { 2, 4, 2 },
                { 1, 2, 1 }
            };

            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    s += k[y, x];
                }
            }

            s = 1 / s;

            for (int y = n1; y < tmp.Height - n1; y++)
                for (int x = n1; x < tmp.Width - n1; x++)
                {
                    int f = tmp.GetPixel(x, y).R;
                    double sum = 0;
                    for (int jy = 0; jy < n; jy++)
                    {
                        for (int jx = 0; jx < n; jx++)
                        {
                            sum += tmp.GetPixel(x - n1 + jx, y - n1 + jy).R * k[jy, jx];
                        }
                    }

                    g = (int)(sum * s + 0.5);
                    if (Math.Abs(g - f) < H)
                        image.SetPixel(x, y, Color.FromArgb(g, g, g));
                }
            pictureBoxCustom2.Image = image.GetBitmap();
        }

        public static Color Color2Gray(Color color)
        {
            double R2g = 0.299,
                G2g = 0.587,
                B2g = 0.114;

            byte g = (byte)(color.R * R2g + color.G * G2g + color.B * B2g);
            return Color.FromArgb(g, g, g);
        }

        public static void ConvertToGrayscale(VRAM vram)
        {
            for (int y = 0; y < vram.Height; y++)
            {
                for (int x = 0; x < vram.Width; x++)
                {
                    vram.SetPixel(x, y, Color2Gray(vram.GetPixel(x, y)));
                }
            }
        }
    }
}
