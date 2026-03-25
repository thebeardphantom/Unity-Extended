using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extension methods for the <see cref="Vector2Int"/> struct, adding additional functionality
    /// to manipulate and convert instances of this type.
    /// </summary>
    public static class Vector2IntExtensions
    {
        /// <summary>
        /// Converts a 2D integer vector to a 3D integer vector by adding a z-coordinate.
        /// </summary>
        /// <param name="v2Int">The 2D vector to convert.</param>
        /// <param name="z">The z-coordinate to assign to the resulting 3D vector. Default is 0.</param>
        /// <returns>A 3D vector with the x and y components from the 2D vector and the specified z-coordinate.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int To3D(this Vector2Int v2Int, int z = 0)
        {
            return new Vector3Int(v2Int.x, v2Int.y, z);
        }

        /// <summary>
        /// Returns a new <see cref="Vector2Int"/> with the absolute values of the x and y components of the given vector.
        /// </summary>
        /// <param name="v2Int">The input <see cref="Vector2Int"/> vector.</param>
        /// <returns>A new <see cref="Vector2Int"/> where both x and y components are converted to their absolute values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Abs(this Vector2Int v2Int)
        {
            return new Vector2Int(Mathf.Abs(v2Int.x), Mathf.Abs(v2Int.y));
        }
    }
}