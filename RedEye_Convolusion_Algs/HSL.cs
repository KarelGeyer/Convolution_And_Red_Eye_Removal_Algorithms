using System;
using System.Drawing;

namespace GALP_Algoritmy
{
    public class HSL
    {
        public int Hue;
        public float Saturation;
        public float Luminance;

        /// <summary>
        /// Transform Color to the HSL color model
        /// </summary>
        /// <param name="color"></param>
        public HSL(Color color)
        {
            float red = (color.R / 255.0f);
            float green = (color.G / 255.0f);
            float blue = (color.B / 255.0f);
            float min = Math.Min(Math.Min(red, green), blue);
            float max = Math.Max(Math.Max(red, green), blue);
            float delta = max - min;

            Luminance = (max + min) / 2;
            if (delta == 0)
            {
                Hue = 0;
                Saturation = 0.0f;
            }
            else
            {
                Saturation = (Luminance <= 0.5) ? (delta / (max + min)) : (delta / (2 - max - min));
                float hue;

                if (red == max)
                    hue = ((green - blue) / 6) / delta;
                else if (green == max)
                    hue = (1.0f / 3) + ((blue - red) / 6) / delta;
                else
                    hue = (2.0f / 3) + ((red - green) / 6) / delta;

                if (hue < 0)
                    hue += 1;
                if (hue > 1)
                    hue -= 1;

                Hue = (int)(hue * 360);
            }
        }
    }
}
