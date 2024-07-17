using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class Vector3IntExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int To2D(this Vector3Int v3Int)
        {
            return new Vector2Int(v3Int.x, v3Int.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Abs(this Vector3Int v3Int)
        {
            return new Vector3Int(Mathf.Abs(v3Int.x), Mathf.Abs(v3Int.y), Mathf.Abs(v3Int.z));
        }
    }
}