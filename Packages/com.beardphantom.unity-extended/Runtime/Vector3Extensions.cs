using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class Vector3Extensions
    {
        #region Methods

        public static Vector3 Abs(this Vector3 v3)
        {
            v3.x = Mathf.Abs(v3.x);
            v3.y = Mathf.Abs(v3.y);
            v3.z = Mathf.Abs(v3.z);
            return v3;
        }

        #endregion
    }
}