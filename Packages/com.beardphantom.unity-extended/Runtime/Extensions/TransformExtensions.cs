using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extension methods for Unity's Transform class, enabling
    /// additional functionalities such as calculating directions and distances
    /// between transforms or positions in a performant manner.
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Calculates the normalized direction vector from the position of the current transform
        /// to the position of another transform.
        /// </summary>
        /// <param name="transform">The source transform from which the direction is calculated.</param>
        /// <param name="other">The target transform to which the direction is calculated.</param>
        /// <returns>The normalized direction vector pointing from the source transform to the target transform.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 DirectionTo(this Transform transform, Transform other)
        {
            return transform.DirectionTo(other.position);
        }

        /// <summary>
        /// Calculates the normalized direction vector from the transform's position to the specified position.
        /// </summary>
        /// <param name="transform">The transform from which the direction is calculated.</param>
        /// <param name="position">The target position to calculate the direction towards.</param>
        /// <returns>The normalized direction vector pointing from the transform to the specified position.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 DirectionTo(this Transform transform, Vector3 position)
        {
            return (position - transform.position).normalized;
        }

        /// <summary>
        /// Calculates the distance between the transform's position and another transform's position.
        /// </summary>
        /// <param name="transform">The transform from which the distance is calculated.</param>
        /// <param name="other">The other transform to which the distance is measured.</param>
        /// <returns>The distance between the two transforms.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceTo(this Transform transform, Transform other)
        {
            return transform.DistanceTo(other.position);
        }

        /// <summary>
        /// Calculates the distance between the Transform's position and the specified position.
        /// </summary>
        /// <param name="transform">The Transform from which the distance is measured.</param>
        /// <param name="position">The target position to measure the distance to.</param>
        /// <returns>The distance between the Transform's position and the specified position.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceTo(this Transform transform, Vector3 position)
        {
            return Vector3.Distance(transform.position, position);
        }

        /// <summary>
        /// Calculates the squared distance between the position of the current transform and another transform.
        /// </summary>
        /// <param name="transform">The current transform.</param>
        /// <param name="other">The target transform to calculate the squared distance to.</param>
        /// <returns>The squared distance between the current transform's position and the target transform's position.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceToSqr(this Transform transform, Transform other)
        {
            return transform.DistanceToSqr(other.position);
        }

        /// <summary>
        /// Calculates the squared distance from the current transform's position to the specified position.
        /// Squared distance is more efficient for computations when exact distance is not required.
        /// </summary>
        /// <param name="transform">The transform from which the squared distance is calculated.</param>
        /// <param name="position">The target position to which the squared distance is calculated.</param>
        /// <returns>The squared distance from the transform's position to the target position.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceToSqr(this Transform transform, Vector3 position)
        {
            return Vector3.SqrMagnitude(position - transform.position);
        }
    }
}