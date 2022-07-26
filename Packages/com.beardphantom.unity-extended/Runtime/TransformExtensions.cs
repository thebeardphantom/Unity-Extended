﻿using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class TransformExtensions
    {
        #region Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 DirectionTo(this Transform transform, Transform other)
        {
            return DirectionTo(transform, other.position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 DirectionTo(this Transform transform, Vector3 position)
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
            return Vector2.Distance(transform.GetPosition2D(), position);
        }

        #endregion
    }
}