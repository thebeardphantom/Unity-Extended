using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class Vector3IntExtensions
    {
        #region Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int To2D(this Vector3Int v3Int)
        {
            return new Vector2Int(v3Int.x, v3Int.y);
        }

        #endregion
    }
}