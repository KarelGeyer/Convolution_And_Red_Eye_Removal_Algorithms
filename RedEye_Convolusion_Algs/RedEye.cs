using GALP_Algoritmy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALPR_Blank
{
    public class RedEye
    {
        public void RemoveRedEye(VRAM vram, PictureBoxCustom pictureBox1)
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

            pictureBox1.Image = vram.GetBitmap();
            pictureBox1.Image = imageToAdjust.GetBitmap();
        }
    }
}
