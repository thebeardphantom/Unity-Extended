using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace BeardPhantom.UnityExtended
{
    public static class BoundsExtensions
    {
        public static Vector3 GetPositionFromNormalized(this Bounds bounds, Vector3 normalizedPosition)
        {
            return GetPositionFromNormalized(bounds, normalizedPosition.x, normalizedPosition.y, normalizedPosition.z);
        }

        public static Vector3 GetPositionFromNormalized(this Bounds bounds, float x, float y)
        {
            return GetPositionFromNormalized(bounds, x, y, 0f);
        }

        public static Vector3 GetPositionFromNormalized(this Bounds bounds, float x, float y, float z)
        {
            return new Vector3
            {
                x = Mathf.Lerp(bounds.min.x, bounds.max.x, x),
                y = Mathf.Lerp(bounds.min.y, bounds.max.y, y),
                z = Mathf.Lerp(bounds.min.z, bounds.max.z, z),
            };
        }

        public static Bounds GetTotalBounds(GameObject gameObject)
        {
            using var _ = ListPool<Renderer>.Get(out var renderers);
            gameObject.GetComponentsInChildren(renderers);
            return GetTotalBounds(renderers);
        }

        public static Bounds GetTotalBounds(IEnumerable<Renderer> renderers)
        {
            Bounds totalBounds = default;
            var hasBounds = false;
            foreach (var renderer in renderers)
            {
                var bounds = renderer.bounds;
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

        public static Bounds WorldToScreenBounds(this Bounds boundsWorld, Camera camera)
        {
            var screenBounds = new Bounds();
            var minScreen = camera.WorldToScreenPoint(boundsWorld.min);
            var maxScreen = camera.WorldToScreenPoint(boundsWorld.max);
            screenBounds.SetMinMax(minScreen, maxScreen);
            return screenBounds;
        }
    }
}