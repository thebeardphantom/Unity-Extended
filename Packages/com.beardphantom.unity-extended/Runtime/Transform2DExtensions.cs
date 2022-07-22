﻿using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class Transform2DExtensions
    {
        #region Properties

        public static Vector2 Forward2DDefault => Vector2.right;

        #endregion

        #region Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetPosition2D(this Transform transform)
        {
            return transform.position;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetLocalPosition2D(this Transform transform)
        {
            return transform.localPosition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition2D(this Transform transform, Vector2 position)
        {
            transform.position = position;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalPosition2D(this Transform transform, Vector2 localPosition)
        {
            transform.localPosition = localPosition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetAngle2D(this Transform transform)
        {
            return transform.eulerAngles.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetLocalAngle2D(this Transform transform)
        {
            return transform.localRotation.z;
        }

        public static void SetAngle2D(this Transform transform, float angle)
        {
            var eulerAngles = transform.eulerAngles;
            eulerAngles.z = angle;
            transform.eulerAngles = eulerAngles;
        }

        public static void SetLocalAngle2D(this Transform transform, float angle)
        {
            var localEulerAngles = transform.localEulerAngles;
            localEulerAngles.z = angle;
            transform.localEulerAngles = localEulerAngles;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LookAt2D(this Transform transform, Transform other)
        {
            LookAt2D(transform, other.GetPosition2D(), Forward2DDefault);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LookAt2D(this Transform transform, Transform other, Vector2 forward2D)
        {
            LookAt2D(transform, other.GetPosition2D(), forward2D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LookAt2D(this Transform transform, Vector2 position)
        {
            LookAt2D(transform, position, Forward2DDefault);
        }

        public static void LookAt2D(this Transform transform, Vector2 position, Vector2 forward2D)
        {
            var directionTo = (position - transform.GetPosition2D()).normalized;
            var angle = Vector2.SignedAngle(forward2D, directionTo);
            transform.SetAngle2D(angle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 DirectionTo(this Transform transform, Transform other)
        {
            return DirectionTo(transform, other.GetPosition2D());
        }

        public static Vector2 DirectionTo(this Transform transform, Vector2 position)
        {
            return (position - transform.GetPosition2D()).normalized;
        }

        #endregion
    }
}