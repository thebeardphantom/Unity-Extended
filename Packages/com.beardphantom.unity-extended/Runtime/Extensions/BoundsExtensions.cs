using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extension methods for working with <see cref="Bounds" /> objects.
    /// </summary>
    public static class BoundsExtensions
    {
        /// <summary>
        /// Calculates the position within the bounds based on a normalized position value.
        /// </summary>
        /// <param name="bounds">
        /// The bounds within which the position is to be calculated.
        /// </param>
        /// <param name="normalizedPosition">
        /// A Vector3 representing a normalized position. Each component should be between 0 and 1,
        /// where 0 corresponds to the minimum bounds and 1 corresponds to the maximum bounds.
        /// </param>
        /// <returns>
        /// A Vector3 representing the position within the bounds corresponding to the provided normalized position.
        /// </returns>
        public static Vector3 GetPositionFromNormalized(this Bounds bounds, Vector3 normalizedPosition)
        {
            return bounds.GetPositionFromNormalized(normalizedPosition.x, normalizedPosition.y, normalizedPosition.z);
        }

        /// <summary>
        /// Calculates a position within the bounds corresponding to a given normalized position.
        /// </summary>
        /// <param name="bounds">The bounds within which to calculate the position.</param>
        /// <param name="x">The normalized x value to convert to an x position within the bounds</param>
        /// <param name="y">The normalized y value to convert to a y position within the bounds</param>
        /// <param name="z">The normalized z value to convert to a z position within the bounds</param>
        /// <returns>
        /// A Vector3 representing the calculated position within the bounds based on the normalized input.
        /// </returns>
        public static Vector3 GetPositionFromNormalized(this Bounds bounds, float x, float y, float z = 0f)
        {
            return new Vector3
            {
                x = Mathf.Lerp(bounds.min.x, bounds.max.x, x),
                y = Mathf.Lerp(bounds.min.y, bounds.max.y, y),
                z = Mathf.Lerp(bounds.min.z, bounds.max.z, z),
            };
        }

        /// <summary>
        /// Calculates the normalized position of a point within the bounds.
        /// </summary>
        /// <param name="bounds">
        /// The bounds within which the point is located.
        /// </param>
        /// <param name="position">
        /// The position in space to normalize relative to the bounds.
        /// </param>
        /// <returns>
        /// A Vector3 representing the normalized position of the given point within the bounds,
        /// where each component ranges from 0 to 1.
        /// </returns>
        public static Vector3 GetNormalizedPosition(this Bounds bounds, Vector3 position)
        {
            return new Vector3
            {
                x = Mathf.InverseLerp(bounds.min.x, bounds.max.x, position.x),
                y = Mathf.InverseLerp(bounds.min.y, bounds.max.y, position.y),
                z = Mathf.InverseLerp(bounds.min.z, bounds.max.z, position.z),
            };
        }

        /// <summary>
        /// Calculates the total bounds encapsulating all the renderers attached to the given GameObject and its children.
        /// </summary>
        /// <param name="gameObject">The GameObject whose renderers' bounds are to be aggregated.</param>
        /// <returns>
        /// A <see cref="Bounds" /> object representing the encapsulated bounds of all renderers in the hierarchy of the
        /// provided GameObject.
        /// </returns>
        public static Bounds GetTotalRendererBounds(GameObject gameObject)
        {
            using PooledObject<List<Renderer>> _ = ListPool<Renderer>.Get(out List<Renderer> renderers);
            gameObject.GetComponentsInChildren(renderers);
            return GetTotalRendererBounds(renderers);
        }

        /// <summary>
        /// Calculates the total bounding volume that encompasses all renderers from the provided collection.
        /// If no renderers are provided, an empty Bounds object is returned.
        /// </summary>
        /// <param name="renderers">
        /// A collection of renderers used to calculate the total bounds.
        /// </param>
        /// <return>
        /// A Bounds object that encapsulates all the renderers in the collection.
        /// </return>
        public static Bounds GetTotalRendererBounds(IEnumerable<Renderer> renderers)
        {
            Bounds totalBounds = default;
            var hasBounds = false;
            foreach (Renderer renderer in renderers)
            {
                Bounds bounds = renderer.bounds;
                if (hasBounds)
                {
                    totalBounds.Encapsulate(bounds);
                }
                else
                {
                    hasBounds = true;
                    totalBounds = bounds;
                }
            }

            return totalBounds;
        }

        /// <summary>
        /// Converts world-space bounding box to screen-space bounding box based on the specified camera.
        /// </summary>
        /// <param name="boundsWorld">
        /// The world-space bounds to be converted.
        /// </param>
        /// <param name="camera">
        /// The camera used for the world-to-screen transformation.
        /// </param>
        /// <returns>
        /// A new <see cref="Bounds" /> representing the screen-space bounds.
        /// </returns>
        public static Bounds WorldToScreenBounds(this Bounds boundsWorld, Camera camera)
        {
            var screenBounds = new Bounds();
            Vector3 minScreen = camera.WorldToScreenPoint(boundsWorld.min);
            Vector3 maxScreen = camera.WorldToScreenPoint(boundsWorld.max);
            screenBounds.SetMinMax(minScreen, maxScreen);
            return screenBounds;
        }
    }
}