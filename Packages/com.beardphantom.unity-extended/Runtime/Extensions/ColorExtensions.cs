using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class ColorExtensions
    {
        public static Color WithRed(this Color color, float red)
        {
            color.r = red;
            return color;
        }

        public static Color WithGreen(this Color color, float green)
        {
            color.g = green;
            return color;
        }

        public static Color WithBlue(this Color color, float blue)
        {
            color.b = blue;
            return color;
        }

        public static Color WithAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }

        public static Color WithHue(this Color color, float hue, bool hdr = false)
        {
            Color.RGBToHSV(color, out _, out var saturation, out var value);
            return Color.HSVToRGB(hue, saturation, value, hdr);
        }

        public static Color WithSaturation(this Color color, float saturation, bool hdr = false)
        {
            Color.RGBToHSV(color, out var hue, out _, out var value);
            return Color.HSVToRGB(hue, saturation, value, hdr);
        }

        public static Color WithValue(this Color color, float value, bool hdr = false)
        {
            Color.RGBToHSV(color, out var hue, out var saturation, out _);
            return Color.HSVToRGB(hue, saturation, value, hdr);
        }
    }
}