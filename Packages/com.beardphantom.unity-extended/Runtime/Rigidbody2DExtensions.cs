using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class Rigidbody2DExtensions
    {
        #region Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LookAt2D(this Rigidbody2D rigidbody2D, Transform other)
        {
            LookAt2D(rigidbody2D, other.GetPosition2D(), Transform2DExtensions.Forward2D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LookAt2D(this Rigidbody2D rigidbody2D, Transform other, Vector2 forward2D)
        {
            LookAt2D(rigidbody2D, other.GetPosition2D(), forward2D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LookAt2D(this Rigidbody2D rigidbody2D, Vector2 position)
        {
            LookAt2D(rigidbody2D, position, Transform2DExtensions.Forward2D);
        }

        public static void LookAt2D(this Rigidbody2D rigidbody2D, Vector2 position, Vector2 forward2D)
        {
            var angle = rigidbody2D.GetLookAtAngle2D(position, forward2D);
            rigidbody2D.SetRotation(angle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetLookAtAngle2D(this Rigidbody2D rigidbody2D, Vector2 position)
        {
            return GetLookAtAngle2D(rigidbody2D, position, Transform2DExtensions.Forward2D);
        }

        public static float GetLookAtAngle2D(this Rigidbody2D rigidbody2D, Vector2 position, Vector2 forward2D)
        {
            var directionTo = (position - rigidbody2D.position).normalized;
            var angle = Vector2.SignedAngle(forward2D, directionTo);
            return angle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 DirectionTo(this Rigidbody2D rigidbody2D, Transform other)
        {
            return DirectionTo(rigidbody2D, other.GetPosition2D());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 DirectionTo(this Rigidbody2D rigidbody2D, Rigidbody2D other)
        {
            return DirectionTo(rigidbody2D, other.position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 DirectionTo(this Rigidbody2D rigidbody2D, Vector2 position)
        {
            return (position - rigidbody2D.position).normalized;
        }

        public static Vector2 GetForward2D(this Rigidbody2D rigidbody2D)
        {
            var rotation = rigidbody2D.GetRotationAsQuaternion();
            return rotation * Transform2DExtensions.Forward2DDefault;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Quaternion GetRotationAsQuaternion(this Rigidbody2D rigidbody2D)
        {
            return Quaternion.Euler(0f, 0f, rigidbody2D.rotation);
        }

        #endregion
    }
}