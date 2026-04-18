using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides utility methods for working with Unity objects.
    /// </summary>
    public static class UnityObjectUtility
    {
        /// <summary>
        /// Finds and returns an object of the specified type in the scene. Throws an exception if no object of the specified type
        /// is found.
        /// </summary>
        /// <typeparam name="T">The type of the object to find.</typeparam>
        /// <param name="findObjectsInactive">Specifies whether inactive objects should be included in the search.</param>
        /// <param name="customExceptionMessage">
        /// The exception message to use if no object of type <typeparamref name="T" /> is found.
        /// </param>
        /// <returns>The found object of the specified type.</returns>
        /// <exception cref="System.Exception">Thrown if no object of type <typeparamref name="T" /> is found.</exception>
        public static T FindAnyRequiredObjectOfType<T>(
            FindObjectsInactive findObjectsInactive = FindObjectsInactive.Exclude,
            string customExceptionMessage = null)
            where T : Object
        {
            var result = Object.FindAnyObjectByType<T>(findObjectsInactive);
            if (result.IsNotNull())
            {
                return result;
            }

            string exceptionMessage = customExceptionMessage ?? $"Object of type {typeof(T).Name} not found.";
            throw new Exception(exceptionMessage);
        }
    }
}