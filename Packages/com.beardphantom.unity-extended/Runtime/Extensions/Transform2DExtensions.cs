﻿using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class Transform2DExtensions
    {
        public static Vector2 Forward2D = Forward2DDefault;

        public static Vector2 Forward2DDefault => Vector2.right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 TransformPoint2D(this Transform transform, Vector2 point)
        {
            return transform.TransformPoint(point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 TransformDirection2D(this Transform transform, Vector2 direction)
        {
            return transform.TransformDirection(direction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 TransformVector2D(this Transform transform, Vector2 vector)
        {
            return transform.TransformVector(vector);
        }

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
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.z = angle;
            transform.eulerAngles = eulerAngles;
        }

        public static void SetLocalAngle2D(this Transform transform, float angle)
        {
            Vector3 localEulerAngles = transform.localEulerAngles;
            localEulerAngles.z = angle;
            transform.localEulerAngles = localEulerAngles;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LookAt2D(this Transform transform, Transform other)
        {
            LookAt2D(transform, other.GetPosition2D(), Forward2D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LookAt2D(this Transform transform, Transform other, Vector2 forward2D)
        {
            LookAt2D(transform, other.GetPosition2D(), forward2D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LookAt2D(this Transform transform, Vector2 position)
        {
            LookAt2D(transform, position, Forward2D);
        }

        public static void LookAt2D(this Transform transform, Vector2 position, Vector2 forward2D)
        {
            float angle = transform.GetLookAtAngle2D(position, forward2D);
            transform.SetAngle2D(angle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetLookAtAngle2D(this Transform transform, Vector2 position)
        {
            return GetLookAtAngle2D(transform, position, Forward2D);
        }

        public static float GetLookAtAngle2D(this Transform transform, Vector2 position, Vector2 forward2D)
        {
            Vector2 directionTo = (position - transform.GetPosition2D()).normalized;
            float angle = Vector2.SignedAngle(forward2D, directionTo);
            return angle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 DirectionTo2D(this Transform transform, Transform other)
        {
            return DirectionTo2D(transform, other.GetPosition2D());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 DirectionTo2D(this Transform transform, Vector2 position)
        {
            return (position - transform.GetPosition2D()).normalized;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetForward2D(this Transform transform)
        {
            return transform.TransformDirection(Forward2D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceTo2D(this Transform transform, Transform other)
        {
            return DistanceTo2D(transform, other.GetPosition2D());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceTo2D(this Transform transform, Vector2 position)
        {
            return Vector2.Distance(transform.GetPosition2D(), position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceToSqr2D(this Transform transform, Transform other)
        {
            return DistanceToSqr2D(transform, other.GetPosition2D());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceToSqr2D(this Transform transform, Vector2 position)
        {
            return Vector2.SqrMagnitude(position - transform.GetPosition2D());
        }
    }
}