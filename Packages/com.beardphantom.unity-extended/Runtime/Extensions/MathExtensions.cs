using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extended mathematical utility methods for common operations.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Computes the modulo operation, ensuring the result is always non-negative,
        /// even when the dividend is negative.
        /// </summary>
        /// <param name="x">The dividend.</param>
        /// <param name="m">The modulus (divisor).</param>
        /// <returns>The result of (x % m + m) % m, ensuring a non-negative value.</returns>
        public static int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        /// <summary>
        /// Repeats a given integer value within the range defined by a minimum and maximum value.
        /// If the value exceeds the range, it wraps around cyclically.
        /// </summary>
        /// <param name="value">The value to repeat within the specified range.</param>
        /// <param name="min">The inclusive minimum value of the range.</param>
        /// <param name="max">The exclusive maximum value of the range.</param>
        /// <returns>The repeated value within the specified range.</returns>
        public static int Repeat(int value, int min, int max)
        {
            int range = max - min;
            return Repeat(value - min, range) + min;
        }

        /// <summary>
        /// Repeats a value t within the range [0, length).
        /// </summary>
        /// <param name="t">The value to repeat.</param>
        /// <param name="length">The length of the range.</param>
        /// <returns>The repeated value within the given range.</returns>
        public static int Repeat(int t, int length)
        {
            return Mathf.Clamp(t - Mathf.FloorToInt((float)t / length) * length, 0, length);
        }

        /// <summary>
        /// Repeats a floating-point value within a specified range by wrapping it around when it exceeds the boundaries.
        /// </summary>
        /// <param name="value">The value to be wrapped within the range.</param>
        /// <param name="min">The inclusive lower bound of the range.</param>
        /// <param name="max">The exclusive upper bound of the range.</param>
        /// <returns>The wrapped value constrained within the range [min, max).</returns>
        public static float Repeat(float value, float min, float max)
        {
            float range = max - min;
            return Mathf.Repeat(value - min, range) + min;
        }

        /// <summary>
        /// Clamps a value to the specified magnitude.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="magnitude">The maximum magnitude (absolute value) to which the value can be clamped.</param>
        /// <return>Returns the clamped value, constrained between -magnitude and magnitude.</return>
        public static float ClampMagnitude(float value, float magnitude)
        {
            magnitude = Mathf.Abs(magnitude);
            return Mathf.Clamp(value, -magnitude, magnitude);
        }

        /// <summary>
        /// Normalizes an angle to the range of -180 to 180 degrees.
        /// </summary>
        /// <param name="angle">The input angle in degrees to normalize.</param>
        /// <returns>The normalized angle within the range of -180 to 180 degrees.</returns>
        public static float NormalizeAngle180(float angle)
        {
            angle = NormalizeAngle(angle);
            if (angle > 180.0f)
            {
                angle -= 360f;
            }

            return angle;
        }

        /// <summary>
        /// Normalizes an angle to be within the [0, 360) range.
        /// </summary>
        /// <param name="angle">The angle in degrees to normalize.</param>
        /// <returns>The normalized angle within the range of [0, 360).</returns>
        public static float NormalizeAngle(float angle)
        {
            return Mathf.Repeat(angle, 360f);
        }
    }
}