using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class BoundsExtensions
    {
        #region Methods

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
                z = Mathf.Lerp(bounds.min.z, bounds.max.z, z)
            };
        }

        #endregion
    }
}