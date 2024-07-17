﻿using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class UnityObjectExtensions
    {
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
                return unityObj == null;
            }

            // Uses regular C# null check
            return obj == null;
        }

        public static bool IsNotNull<T>(this T obj) where T : class
        {
            if (obj is Object unityObj)
            {
                /* Invokes Unity's custom == check for nulls.
                 * This case technically only works because obj is
                 * a "fake null" on the C# side by this point.
                 * If its a true null it'd hit the else case instead,
                 * which fortunately still returns what we want.
                 */
                return unityObj != null;
            }

            // Uses regular C# null check
            return obj != null;
        }
    }
}