using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extension methods for the <see cref="Vector3Int"/> structure.
    /// </summary>
    public static class Vector3IntExtensions
    {
        /// <summary>
        /// Converts a <see cref="Vector3Int"/> to a <see cref="Vector2Int"/> by discarding the z-component.
        /// </summary>
        /// <param name="v3Int">The <see cref="Vector3Int"/> to convert.</param>
        /// <returns>A <see cref="Vector2Int"/> containing the x and y components of the input <see cref="Vector3Int"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int To2D(this Vector3Int v3Int)
        {
            return new Vector2Int(v3Int.x, v3Int.y);
        }

        /// <summary>
        /// Returns a new <see cref="Vector3Int"/> with each component replaced by its absolute value.
        /// </summary>
        /// <param name="v3Int">The input <see cref="Vector3Int"/> whose components will be made absolute.</param>
        /// <returns>A new <see cref="Vector3Int"/> with absolute values of the input components.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Abs(this Vector3Int v3Int)
        {
            return new Vector3Int(Mathf.Abs(v3Int.x), Mathf.Abs(v3Int.y), Mathf.Abs(v3Int.z));
        }
    }
}