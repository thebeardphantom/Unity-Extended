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

        public static Vector3 GetPositionFromNormalized(this Bounds bounds, float x, float y, float z = 0f)
        {
            return new Vector3
            {
                x = Mathf.Lerp(bounds.min.x, bounds.max.x, x),
                y = Mathf.Lerp(bounds.min.y, bounds.max.y, y),
                z = Mathf.Lerp(bounds.min.z, bounds.max.z, z),
            };
        }

        public static Vector3 GetNormalizedPosition(this Bounds bounds, Vector3 position)
        {
            return new Vector3
            {
                x = Mathf.InverseLerp(bounds.min.x, bounds.max.x, position.x),
                y = Mathf.InverseLerp(bounds.min.y, bounds.max.y, position.y),
                z = Mathf.InverseLerp(bounds.min.z, bounds.max.z, position.z),
            };
        }

        public static Bounds GetTotalRendererBounds(GameObject gameObject)
        {
            using PooledObject<List<Renderer>> _ = ListPool<Renderer>.Get(out List<Renderer> renderers);
            gameObject.GetComponentsInChildren(renderers);
            return GetTotalRendererBounds(renderers);
        }

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