using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extension methods for performing common 2D operations on Unity's <see cref="Transform" /> class.
    /// These methods are optimized for 2D transformations by utilizing the X and Y components of the <see cref="Vector2" />
    /// structure.
    /// </summary>
    public static class Transform2DExtensions
    {
        /// <summary>
        /// Represents the default forward direction in 2D space for Transform-related extensions.
        /// The value is statically defined and defaults to the Unity Engine's <see cref="Vector2.right" />.
        /// It can be used to determine or transform "forward" in the context of 2D operations
        /// within the <see cref="Transform2DExtensions" /> utility class.
        /// </summary>
        public static Vector2 Forward2D = Forward2DDefault;

        /// <summary>
        /// Gets the default forward direction in 2D space as a <see cref="Vector2" />.
        /// This value is assigned as <see cref="Vector2.right" />, representing the positive X-axis in 2D space.
        /// </summary>
        public static Vector2 Forward2DDefault => Vector2.right;

        /// <summary>
        /// Transforms the specified 2D point from local space to world space using the given Transform.
        /// </summary>
        /// <param name="transform">The Transform used for the transformation.</param>
        /// <param name="point">The 2D point in local space to be transformed.</param>
        /// <return>The transformed 2D point in world space.</return>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 TransformPoint2D(this Transform transform, Vector2 point)
        {
            return transform.TransformPoint(point);
        }

        /// <summary>
        /// Transforms a direction vector from local space to world space with its orientation
        /// relative to the Transform's space considered. This only applies to the direction and
        /// does not account for translation or position.
        /// </summary>
        /// <param name="transform">The Transform to apply the directional transformation.</param>
        /// <param name="direction">The direction vector in local space to be transformed into world space.</param>
        /// <returns>Returns the transformed direction vector in world space.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 TransformDirection2D(this Transform transform, Vector2 direction)
        {
            return transform.TransformDirection(direction);
        }

        /// <summary>
        /// Transforms a 2D vector from the local space of the transform into world space.
        /// </summary>
        /// <param name="transform">The transform used to perform the transformation.</param>
        /// <param name="vector">The 2D vector to transform.</param>
        /// <return>Returns the transformed 2D vector in world space.</return>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 TransformVector2D(this Transform transform, Vector2 vector)
        {
            return transform.TransformVector(vector);
        }

        /// <summary>
        /// Retrieves the 2D position of the transform, discarding the z-axis component of its position.
        /// </summary>
        /// <param name="transform">The Transform object to retrieve the 2D position from.</param>
        /// <returns>A Vector2 representing the 2D position of the transform.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetPosition2D(this Transform transform)
        {
            return transform.position;
        }

        /// <summary>
        /// Retrieves the 2D local position of the specified <see cref="Transform" />.
        /// </summary>
        /// <param name="transform">The transform from which to retrieve the local position.</param>
        /// <returns>The 2D local position of the transform as a <see cref="Vector2" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetLocalPosition2D(this Transform transform)
        {
            return transform.localPosition;
        }

        /// <summary>
        /// Sets the 2D position of the transform in world space.
        /// </summary>
        /// <param name="transform">The transform whose position will be modified.</param>
        /// <param name="position">The new 2D position to set in world space.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition2D(this Transform transform, Vector2 position)
        {
            transform.position = position;
        }

        /// <summary>
        /// Sets the local position of the transform in 2D space using a specified <see cref="Vector2" />.
        /// </summary>
        /// <param name="transform">The transform whose local position will be set.</param>
        /// <param name="localPosition">The new local position for the transform in 2D space.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalPosition2D(this Transform transform, Vector2 localPosition)
        {
            transform.localPosition = localPosition;
        }

        /// <summary>
        /// Retrieves the 2D angle of the transform, using the Z-axis rotation component.
        /// </summary>
        /// <param name="transform">The transform from which to retrieve the 2D angle.</param>
        /// <returns>The angle around the Z-axis in degrees.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetAngle2D(this Transform transform)
        {
            return transform.eulerAngles.z;
        }

        /// <summary>
        /// Retrieves the local rotation angle of a Transform in 2D space.
        /// </summary>
        /// <param name="transform">The Transform to retrieve the local angle from.</param>
        /// <returns>The local rotation angle around the Z-axis in 2D space.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetLocalAngle2D(this Transform transform)
        {
            return transform.localRotation.z;
        }

        /// <summary>
        /// Sets the 2D rotation angle of the Transform around the Z-axis.
        /// </summary>
        /// <param name="transform">The Transform whose rotation angle is being set.</param>
        /// <param name="angle">The desired rotation angle in degrees.</param>
        public static void SetAngle2D(this Transform transform, float angle)
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.z = angle;
            transform.eulerAngles = eulerAngles;
        }

        /// <summary>
        /// Sets the local rotation angle around the Z-axis for a 2D transform.
        /// </summary>
        /// <param name="transform">The transform whose local rotation is being modified.</param>
        /// <param name="angle">The angle in degrees to set for the local rotation around the Z-axis.</param>
        public static void SetLocalAngle2D(this Transform transform, float angle)
        {
            Vector3 localEulerAngles = transform.localEulerAngles;
            localEulerAngles.z = angle;
            transform.localEulerAngles = localEulerAngles;
        }

        /// <summary>
        /// Rotates the transform to face the target transform in 2D space,
        /// aligning the forward direction with the specified default 2D forward vector.
        /// </summary>
        /// <param name="transform">The transform that will be rotated to face the target.</param>
        /// <param name="other">The target transform to face.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LookAt2D(this Transform transform, Transform other)
        {
            transform.LookAt2D(other.GetPosition2D(), Forward2D);
        }

        /// <summary>
        /// Rotates the transform to face a specified target position in 2D space using a specified forward direction.
        /// </summary>
        /// <param name="transform">The transform to be rotated.</param>
        /// <param name="position">The target position to look at in 2D space.</param>
        /// <param name="forward2D">The forward direction in 2D space, used as the reference direction for rotation.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LookAt2D(this Transform transform, Transform other, Vector2 forward2D)
        {
            transform.LookAt2D(other.GetPosition2D(), forward2D);
        }

        /// <summary>
        /// Rotates the given transform to face a specific position in 2D space, using the default forward direction.
        /// </summary>
        /// <param name="transform">The transform to rotate.</param>
        /// <param name="position">The target position to look at in 2D space.</>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LookAt2D(this Transform transform, Vector2 position)
        {
            transform.LookAt2D(position, Forward2D);
        }

        /// <summary>
        /// Rotates the transform to face a specified position in 2D space, using a given forward direction vector.
        /// The method calculates the angle required for the transform to face the target position and sets the transform's
        /// rotation accordingly in 2D space, while ignoring the Z-axis.
        /// </summary>
        /// <param name="transform">The Transform instance to manipulate.</param>
        /// <param name="position">The target position in world space to look at.</param>
        /// <param name="forward2D">
        /// The forward direction vector of the transform in 2D space. This vector is used as
        /// the reference for determining the rotation angle.
        /// </param>
        public static void LookAt2D(this Transform transform, Vector2 position, Vector2 forward2D)
        {
            float angle = transform.GetLookAtAngle2D(position, forward2D);
            transform.SetAngle2D(angle);
        }

        /// <summary>
        /// Calculates the angle in degrees between the forward vector of the transform
        /// and the specified position in 2D space, using the default forward vector (Vector2.right).
        /// </summary>
        /// <param name="transform">The transform from which the angle is calculated.</param>
        /// <param name="position">The target position to look at in 2D space.</param>
        /// <returns>The angle in degrees between the forward vector of the transform and the target position.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetLookAtAngle2D(this Transform transform, Vector2 position)
        {
            return transform.GetLookAtAngle2D(position, Forward2D);
        }

        /// <summary>
        /// Calculates the angle required to make the transform face a specified position in 2D space,
        /// considering a custom forward direction.
        /// </summary>
        /// <param name="transform">The transform that is calculating the angle.</param>
        /// <param name="position">The target position to look at in 2D space.</param>
        /// <param name="forward2D">The custom forward direction of the transform in 2D space.</param>
        /// <return>
        /// The signed angle in degrees that the transform needs to rotate to face the specified position.
        /// A positive value rotates counterclockwise, and a negative value rotates clockwise.
        /// </return>
        public static float GetLookAtAngle2D(this Transform transform, Vector2 position, Vector2 forward2D)
        {
            Vector2 directionTo = (position - transform.GetPosition2D()).normalized;
            float angle = Vector2.SignedAngle(forward2D, directionTo);
            return angle;
        }

        /// <summary>
        /// Calculates the normalized direction vector from the current transform to a target transform in 2D space.
        /// </summary>
        /// <param name="transform">The reference transform.</param>
        /// <param name="other">The target transform.</param>
        /// <return>A normalized <c>Vector2</c> pointing towards the target transform.</return>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 DirectionTo2D(this Transform transform, Transform other)
        {
            return transform.DirectionTo2D(other.GetPosition2D());
        }

        /// <summary>
        /// Calculates the normalized direction vector from the transform's position to a target position in 2D space.
        /// </summary>
        /// <param name="transform">The reference transform from which the direction is calculated.</param>
        /// <param name="position">The target position in 2D space.</param>
        /// <returns>The normalized direction vector pointing towards the target position in 2D space.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 DirectionTo2D(this Transform transform, Vector2 position)
        {
            return (position - transform.GetPosition2D()).normalized;
        }

        /// <summary>
        /// Returns the forward direction vector in 2D space for the given Transform,
        /// based on the predefined 2D forward vector.
        /// </summary>
        /// <param name="transform">The Transform for which the forward direction is determined.</param>
        /// <returns>A Vector2 representing the forward direction in 2D space.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetForward2D(this Transform transform)
        {
            return transform.TransformDirection(Forward2D);
        }

        /// <summary>
        /// Calculates the 2D distance between the transform's current position and another transform's position.
        /// </summary>
        /// <param name="transform">The transform from which the distance is calculated.</param>
        /// <param name="other">The target transform to calculate the distance to.</param>
        /// <returns>The distance between the two transforms in 2D space.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceTo2D(this Transform transform, Transform other)
        {
            return transform.DistanceTo2D(other.GetPosition2D());
        }

        /// <summary>
        /// Calculates the 2D distance between the transform's position and the specified position.
        /// </summary>
        /// <param name="transform">The transform whose position is used as the origin.</param>
        /// <param name="position">The target position to calculate the distance to.</param>
        /// <returns>The 2D distance between the transform's position and the specified position.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceTo2D(this Transform transform, Vector2 position)
        {
            return Vector2.Distance(transform.GetPosition2D(), position);
        }

        /// <summary>
        /// Calculates the square of the distance between the current transform and another transform in 2D space.
        /// </summary>
        /// <param name="transform">The current transform.</param>
        /// <param name="other">The target transform whose 2D position is used to calculate the squared distance.</param>
        /// <returns>The square of the distance between the two transforms in 2D space.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceToSqr2D(this Transform transform, Transform other)
        {
            return transform.DistanceToSqr2D(other.GetPosition2D());
        }

        /// <summary>
        /// Calculates the squared distance between the Transform's 2D position and a specified position.
        /// </summary>
        /// <param name="transform">The Transform whose 2D position is being compared.</param>
        /// <param name="position">The target 2D position to calculate the squared distance to.</param>
        /// <returns>The squared distance between the Transform's 2D position and the specified position.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceToSqr2D(this Transform transform, Vector2 position)
        {
            return Vector2.SqrMagnitude(position - transform.GetPosition2D());
        }
    }
}