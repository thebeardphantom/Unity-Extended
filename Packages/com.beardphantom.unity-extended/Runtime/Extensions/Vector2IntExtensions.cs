using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class Vector2IntExtensions
    {
        #region Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int To3D(this Vector2Int v2Int)
        {
            return new Vector3Int(v2Int.x, v2Int.y);
        }

        #endregion
    }
}