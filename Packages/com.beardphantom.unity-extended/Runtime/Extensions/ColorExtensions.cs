using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extension methods for manipulating Unity's <see cref="Color" /> structures.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Returns a new <see cref="Color" /> instance with the red component set to the specified value.
        /// </summary>
        /// <param name="color">The original color.</param>
        /// <param name="red">The value to set for the red component.</param>
        /// <returns>A new <see cref="Color" /> instance with the updated red component.</returns>
        public static Color WithRed(this Color color, float red)
        {
            color.r = red;
            return color;
        }

        /// <summary>
        /// Creates a new <see cref="Color" /> by modifying the green channel of the given color.
        /// </summary>
        /// <param name="color">The original color.</param>
        /// <param name="green">The new value for the green channel.</param>
        /// <returns>A new <see cref="Color" /> with the updated green channel.</returns>
        public static Color WithGreen(this Color color, float green)
        {
            color.g = green;
            return color;
        }

        /// <summary>
        /// Returns a new <see cref="Color" /> with the blue component set to the specified value.
        /// </summary>
        /// <param name="color">The source color to modify.</param>
        /// <param name="blue">The blue component value to set, ranging from 0 to 1.</param>
        /// <returns>A new <see cref="Color" /> with the blue component updated to the specified value.</returns>
        public static Color WithBlue(this Color color, float blue)
        {
            color.b = blue;
            return color;
        }

        /// <summary>
        /// Creates a new <see cref="Color" /> instance with a modified alpha value.
        /// </summary>
        /// <param name="color">The base color to modify.</param>
        /// <param name="alpha">The new alpha value to apply to the color.</param>
        /// <returns>A new <see cref="Color" /> instance with the specified alpha value.</returns>
        public static Color WithAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }

        /// <summary>
        /// Returns a new Color with the specified hue, while retaining the original saturation and value components.
        /// </summary>
        /// <param name="color">The original color.</param>
        /// <param name="hue">The hue value to apply, ranging from 0 to 1.</param>
        /// <param name="hdr">
        /// Indicates whether the RGB color should be created in high dynamic range (HDR) mode.
        /// If true, values may exceed the 0-1 range.
        /// </param>
        /// <returns>A new Color with the updated hue.</returns>
        public static Color WithHue(this Color color, float hue, bool hdr = false)
        {
            Color.RGBToHSV(color, out _, out float saturation, out float value);
            return Color.HSVToRGB(hue, saturation, value, hdr);
        }

        /// <summary>
        /// Returns a new Color with the specified saturation value while maintaining the original hue and value components.
        /// </summary>
        /// <param name="color">The original color to modify.</param>
        /// <param name="saturation">The saturation value to set, typically between 0 and 1.</param>
        /// <param name="hdr">Indicates whether the resulting color is in High Dynamic Range (HDR).</param>
        /// <returns>A new Color with the specified saturation applied.</returns>
        public static Color WithSaturation(this Color color, float saturation, bool hdr = false)
        {
            Color.RGBToHSV(color, out float hue, out _, out float value);
            return Color.HSVToRGB(hue, saturation, value, hdr);
        }

        /// <summary>
        /// Returns a new <see cref="Color" /> with the value (brightness) component modified, leaving other components unchanged.
        /// </summary>
        /// <param name="color">The original color.</param>
        /// <param name="value">The new value (brightness) component of the color, in the range [0, 1].</param>
        /// <param name="hdr">Indicates whether the color should be in high dynamic range mode.</param>
        /// <returns>A new <see cref="Color" /> with the specified value (brightness) component.</returns>
        public static Color WithValue(this Color color, float value, bool hdr = false)
        {
            Color.RGBToHSV(color, out float hue, out float saturation, out _);
            return Color.HSVToRGB(hue, saturation, value, hdr);
        }
    }
}