using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;

namespace BeardPhantom.UnityExtended
{
    public static class Rigidbody2DExtensions
    {
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
            float angle = rigidbody2D.GetLookAtAngle2D(position, forward2D);
            rigidbody2D.SetRotation(angle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetLookAtAngle2D(this Rigidbody2D rigidbody2D, Vector2 position)
        {
            return GetLookAtAngle2D(rigidbody2D, position, Transform2DExtensions.Forward2D);
        }

        public static float GetLookAtAngle2D(this Rigidbody2D rigidbody2D, Vector2 position, Vector2 forward2D)
        {
            Vector2 directionTo = (position - rigidbody2D.position).normalized;
            float angle = Vector2.SignedAngle(forward2D, directionTo);
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
            Quaternion rotation = rigidbody2D.GetRotationAsQuaternion();
            return rotation * Transform2DExtensions.Forward2DDefault;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceTo(this Rigidbody2D rigidbody2D, Rigidbody2D other)
        {
            return DistanceTo(rigidbody2D, other.position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceTo(this Rigidbody2D rigidbody2D, Transform transform)
        {
            return DistanceTo(rigidbody2D, transform.GetPosition2D());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceTo(this Rigidbody2D rigidbody2D, Vector2 position)
        {
            return Vector2.Distance(rigidbody2D.position, position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceToSqr(this Rigidbody2D rigidbody2D, Rigidbody2D other)
        {
            return DistanceToSqr(rigidbody2D, other.position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceToSqr(this Rigidbody2D rigidbody2D, Transform transform)
        {
            return DistanceToSqr(rigidbody2D, transform.GetPosition2D());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceToSqr(this Rigidbody2D rigidbody2D, Vector2 position)
        {
            return Vector2.SqrMagnitude(position - rigidbody2D.position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion GetRotationAsQuaternion(this Rigidbody2D rigidbody2D)
        {
            return Quaternion.Euler(0f, 0f, rigidbody2D.rotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetRotationAsEuler(this Rigidbody2D rigidbody2D)
        {
            return new Vector3(0f, 0f, rigidbody2D.rotation);
        }

        public static Bounds GetBounds(this Rigidbody2D rigidbody2D)
        {
            using (ListPool<Collider2D>.Get(out List<Collider2D> colliders))
            {
                rigidbody2D.GetAttachedColliders(colliders);
                Bounds bounds = default;
                for (var i = 0; i < colliders.Count; i++)
                {
                    Collider2D collider = colliders[i];
                    if (i == 0)
                    {
                        bounds = collider.bounds;
                    }
                    else
                    {
                        bounds.Encapsulate(collider.bounds);
                    }
                }

                return bounds;
            }
        }
    }
}