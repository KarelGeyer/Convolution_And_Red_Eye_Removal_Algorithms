using GALP_Algoritmy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALPR_Blank
{
    public class Convol
    {
        public void Convolution(VRAM vram, PictureBoxCustom pictureBox1)
        {
            VRAM image = vram.Copy();
            ConvertToGrayscale(image);
            pictureBox1.Image = image.GetBitmap();
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
        }

        private Color Color2Gray(Color color)
        {
            double R2g = 0.299,
                G2g = 0.587,
                B2g = 0.114;

            byte g = (byte)(color.R * R2g + color.G * G2g + color.B * B2g);
            return Color.FromArgb(g, g, g);
        }

        private void ConvertToGrayscale(VRAM vram)
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
