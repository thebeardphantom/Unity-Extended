using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extension methods for the UnityEngine.Vector2 struct.
    /// </summary>
    public static class Vector2Extensions
    {
        /// <summary>
        /// Returns a new <see cref="Vector2" /> with the X component replaced by the specified value.
        /// </summary>
        /// <param name="v2">The original <see cref="Vector2" /> instance.</param>
        /// <param name="x">The new value for the X component.</param>
        /// <returns>A new <see cref="Vector2" /> with the X component set to the specified value.</returns>
        public static Vector2 WithX(this Vector2 v2, in float x)
        {
            v2.x = x;
            return v2;
        }

        /// <summary>
        /// Creates a new Vector2 with the specified y value while keeping the x value unchanged.
        /// </summary>
        /// <param name="v2">The original Vector2 instance.</param>
        /// <param name="y">The new y value to be assigned.</param>
        /// <returns>A new Vector2 instance with the specified y value and the original x value.</returns>
        public static Vector2 WithY(this Vector2 v2, in float y)
        {
            v2.y = y;
            return v2;
        }

        /// <summary>
        /// Converts a 2D vector to a 3D vector with the X and Y components of the vector
        /// mapped to the X and Z components of the resulting vector, respectively.
        /// The Y component of the resulting vector is set to 0.
        /// </summary>
        /// <param name="v2">The 2D vector to remap.</param>
        /// <returns>A 3D vector with the X and Z components mapped from the input vector and the Y component set to 0.</returns>
        public static Vector3 RemapToX0Z(this Vector2 v2)
        {
            return new Vector3(v2.x, 0f, v2.y);
        }

        /// <summary>
        /// Rotates the given <see cref="Vector2" /> instance by the specified angle in degrees.
        /// </summary>
        /// <param name="v2">The vector to rotate.</param>
        /// <param name="degrees">The angle in degrees by which to rotate the vector.</param>
        /// <returns>A new <see cref="Vector2" /> that is the result of rotating the input vector.</returns>
        public static Vector2 Rotate(this Vector2 v2, float degrees)
        {
            float rads = degrees * Mathf.Deg2Rad;
            float sin = Mathf.Sin(rads);
            float cos = Mathf.Cos(rads);

            float tx = v2.x;
            float ty = v2.y;
            v2.x = cos * tx - sin * ty;
            v2.y = sin * tx + cos * ty;
            return v2;
        }
    }
}