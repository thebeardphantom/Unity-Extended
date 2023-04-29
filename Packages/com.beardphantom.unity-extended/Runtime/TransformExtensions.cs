using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class TransformExtensions
    {
        #region Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 DirectionTo(this Transform transform, Transform other)
        {
            return DirectionTo(transform, other.position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 DirectionTo(this Transform transform, Vector3 position)
        {
            return (position - transform.position).normalized;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceTo(this Transform transform, Transform other)
        {
            return DistanceTo(transform, other.position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceTo(this Transform transform, Vector3 position)
        {
            return Vector3.Distance(transform.position, position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceToSqr(this Transform transform, Transform other)
        {
            return DistanceToSqr(transform, other.position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceToSqr(this Transform transform, Vector3 position)
        {
            return Vector3.SqrMagnitude(position - transform.position);
        }

        #endregion
    }
}