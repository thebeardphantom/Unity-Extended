using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extension methods for working with UnityEngine.Object and other objects,
    /// offering additional utility for null-checking in Unity-specific and general contexts.
    /// </summary>
    public static class UnityObjectExtensions
    {
        /// <summary>
        /// Checks whether the specified object is null. This method supports both C# null checks and Unity's null check logic for
        /// UnityEngine.Object-derived objects.
        /// </summary>
        /// <typeparam name="T">The type of the object to check. Must be a reference type.</typeparam>
        /// <param name="obj">The object to check for null.</param>
        /// <returns>True if the object is null or a Unity "fake null"; otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNull<T>(this T obj) where T : class
        {
            if (obj is Object unityObj)
            {
                /* Invokes Unity's custom == check for nulls.
                 * This case technically only works because obj is
                 * a "fake null" on the C# side by this point.
                 * If its a true null it'd hit the else case instead,
                 * which fortunately still returns what we want.
                 */
                return !unityObj;
            }

            // Uses regular C# null check
            return obj == null;
        }

        /// <summary>
        /// Checks if the specified object is not null.
        /// </summary>
        /// <typeparam name="T">The type of the object being checked.</typeparam>
        /// <param name="obj">The object to check for non-nullity.</param>
        /// <returns>True if the object is not null; otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNotNull<T>(this T obj) where T : class
        {
            return !obj.IsNull();
        }
    }
}