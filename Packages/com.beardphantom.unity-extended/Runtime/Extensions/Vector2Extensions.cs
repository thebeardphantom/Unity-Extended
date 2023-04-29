using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class Vector2Extensions
    {
        #region Methods

        public static Vector2 WithX(this Vector2 v2, in float x)
        {
            v2.x = x;
            return v2;
        }

        public static Vector2 WithY(this Vector2 v2, in float y)
        {
            v2.y = y;
            return v2;
        }

        public static Vector3 RemapToX0Z(this Vector2 v2)
        {
            return new Vector3(v2.x, 0f, v2.y);
        }

        #endregion
    }
}