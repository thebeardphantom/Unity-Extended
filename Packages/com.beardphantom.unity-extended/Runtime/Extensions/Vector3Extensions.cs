using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extension methods for the UnityEngine.Vector3 struct, enabling enhanced functionality for Vector3
    /// manipulation.
    /// </summary>
    public static class Vector3Extensions
    {
        /// <summary>
        /// Returns a new <see cref="Vector3" /> with the x-component replaced by the specified value.
        /// </summary>
        /// <param name="v3">The source vector to modify.</param>
        /// <param name="x">The new x-component value.</param>
        /// <returns>A new <see cref="Vector3" /> with the updated x-component.</returns>
        public static Vector3 WithX(this Vector3 v3, in float x)
        {
            v3.x = x;
            return v3;
        }

        /// <summary>
        /// Returns a new <see cref="Vector3" /> with the Y component set to the specified value.
        /// </summary>
        /// <param name="v3">The original <see cref="Vector3" />.</param>
        /// <param name="y">The new value for the Y component.</param>
        /// <returns>A new <see cref="Vector3" /> with the updated Y component.</returns>
        public static Vector3 WithY(this Vector3 v3, in float y)
        {
            v3.y = y;
            return v3;
        }

        /// <summary>
        /// Returns a new <see cref="Vector3" /> with the Z component set to the specified value.
        /// </summary>
        /// <param name="v3">The original <see cref="Vector3" />.</param>
        /// <param name="z">The new value for the Z component.</param>
        /// <returns>A new <see cref="Vector3" /> with the updated Z component.</returns>
        public static Vector3 WithZ(this Vector3 v3, in float z)
        {
            v3.z = z;
            return v3;
        }

        /// <summary>
        /// Returns a copy of the given Vector3 with each component set to its absolute value.
        /// </summary>
        /// <param name="v3">The source Vector3.</param>
        /// <returns>A new Vector3 where all components are non-negative.</returns>
        public static Vector3 Abs(this Vector3 v3)
        {
            v3.x = Mathf.Abs(v3.x);
            v3.y = Mathf.Abs(v3.y);
            v3.z = Mathf.Abs(v3.z);
            return v3;
        }

        /// <summary>
        /// Normalizes the vector and calculates its magnitude.
        /// </summary>
        /// <param name="v3">The input vector to be normalized.</param>
        /// <param name="magnitude">Outputs the magnitude of the input vector.</param>
        /// <returns>A normalized version of the input vector.</returns>
        public static Vector3 NormalizeAndGetMagnitude(this Vector3 v3, out float magnitude)
        {
            magnitude = v3.magnitude;
            Vector3 normalized = v3 / magnitude;
            return normalized;
        }

        /// <summary>
        /// Creates a new Vector2 by remapping the X and Z components of the given Vector3 to the X and Y components, respectively.
        /// </summary>
        /// <param name="v3">The original Vector3.</param>
        /// <returns>A Vector2 where X and Y are mapped from the X and Z components of the original Vector3, respectively.</returns>
        public static Vector2 RemapXZToXY(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.z);
        }

        /// <summary>
        /// Remaps the Y component of the given Vector3 to the Z axis while setting the Y component to 0.
        /// </summary>
        /// <param name="v3">The Vector3 to remap.</param>
        /// <returns>A new Vector3 where the Y component is set to 0 and the original Y value is moved to the Z axis.</returns>
        public static Vector3 RemapToX0Z(this Vector3 v3)
        {
            return new Vector3(v3.x, 0, v3.y);
        }
    }
}