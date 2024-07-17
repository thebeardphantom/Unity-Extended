using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class Vector3Extensions
    {
        public static Vector3 WithX(this Vector3 v3, in float x)
        {
            v3.x = x;
            return v3;
        }

        public static Vector3 WithY(this Vector3 v3, in float y)
        {
            v3.y = y;
            return v3;
        }

        public static Vector3 WithZ(this Vector3 v3, in float z)
        {
            v3.z = z;
            return v3;
        }

        public static Vector3 Abs(this Vector3 v3)
        {
            v3.x = Mathf.Abs(v3.x);
            v3.y = Mathf.Abs(v3.y);
            v3.z = Mathf.Abs(v3.z);
            return v3;
        }

        public static Vector2 RemapXZToXY(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.z);
        }

        public static Vector3 RemapToX0Z(this Vector3 v3)
        {
            return new Vector3(v3.x, 0, v3.y);
        }
    }
}