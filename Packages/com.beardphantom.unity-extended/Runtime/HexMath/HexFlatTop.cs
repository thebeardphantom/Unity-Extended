using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Unity uses odd-r for pointy-top hex grids and odd-q for flat top hex grids.
    /// This class is only designed to work with flat-top.
    /// Math from https://www.redblobgames.com/grids/hexagons/#neighbors-offset
    /// </summary>
    public static partial class HexFlatTop
    {
        #region Methods

        public static Vector2Int CubeToOffsetFlat(this CubeCoord cubeCoord)
        {
            var col = cubeCoord.Q;
            var row = cubeCoord.R + (cubeCoord.Q - (cubeCoord.Q & 1)) / 2;
            return new Vector2Int(row, col);
        }

        public static CubeCoord OffsetToCubeFlat(this Vector2Int offset)
        {
            var swizzled = new Vector2Int(offset.y, offset.x);
            var q = swizzled.x;
            var r = swizzled.y - (swizzled.x - (swizzled.x & 1)) / 2;
            return new CubeCoord(q, r, -q - r);
        }

        #endregion
    }
}