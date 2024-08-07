using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class Vector2IntExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int To3D(this Vector2Int v2Int, int z = 0)
        {
            return new Vector3Int(v2Int.x, v2Int.y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Abs(this Vector2Int v2Int)
        {
            return new Vector2Int(Mathf.Abs(v2Int.x), Mathf.Abs(v2Int.y));
        }
    }
}